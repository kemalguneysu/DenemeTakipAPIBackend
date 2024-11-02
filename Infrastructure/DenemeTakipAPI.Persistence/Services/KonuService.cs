using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs.Konu;
using DenemeTakipAPI.Application.Repositories.DersRepository;
using DenemeTakipAPI.Application.Repositories.KonuRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Azure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Policy;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonularPaginated;
using DenemeTakipAPI.Persistence.Repositories.DersRepository;
using DenemeTakipAPI.Application.Abstraction.Hubs;

namespace DenemeTakipAPI.Persistence.Services
{
    public class KonuService:IKonuService
    {
        readonly IKonuReadRepository _konuReadRepository;
        readonly IKonuWriteRepository _konuWriteRepository;
        readonly IDersReadRepository _dersReadRepository;
        readonly IKonuHubService _konuHubService;
        public KonuService(IKonuReadRepository konuReadRepository, IKonuWriteRepository konuWriteRepository, IDersReadRepository dersReadRepository, IKonuHubService konuHubService)
        {
            _konuReadRepository = konuReadRepository;
            _konuWriteRepository = konuWriteRepository;
            _dersReadRepository = dersReadRepository;
            _konuHubService = konuHubService;
        }

        public async Task CreateKonuAsync(CreateKonu createKonu)
        {
            var ders = await _dersReadRepository.GetByIdAsync(createKonu.DersId);
            await _konuWriteRepository.AddAsync(new()
            {
                KonuAdi=createKonu.KonuAdi,
                Ders=ders,
                IsTyt=createKonu.IsTyt,
                
            });
            var signalRMessage = $"Yeni bir konu eklendi:\n";
            signalRMessage += $"Konu Adı: {createKonu.KonuAdi}\n";
            signalRMessage += $"Ders: {ders.DersAdi}\n";
            signalRMessage += $"Konu Türü: {(createKonu.IsTyt ? "TYT" : "AYT")}";

            await _konuHubService.KonuAddedMessage(signalRMessage);
            await _konuWriteRepository.SaveAsync();
        }

        public async Task<DeleteKonuResponse> DeleteKonu(List<string> ids)
        {
            var konular = await _konuReadRepository.GetWhere(p => ids.Contains(p.Id.ToString())).ToListAsync();
            var signalRMessage = $"Konular silindi:\n";
            if (konular != null && konular.Any())
            {
                foreach (var konu in konular)
                {
                    signalRMessage += $"{konu.KonuAdi}\n"; // Konu adını ekliyoruz
                }
                _konuWriteRepository.RemoveRange(konular);
                await _konuWriteRepository.SaveAsync();
                await _konuHubService.KonuDeletedMessage(signalRMessage);
                return new()
                {
                    Succeeded = true,
                    Message = "Seçilen konular başarıyla silinmiştir."
                };
            }
            else
            {
                return new()
                {
                    Succeeded = false,
                    Message = "Silinecek konu bulunamadı."
                };
            }
        }

        public async Task<ListKonu> Get(int page, int size)
        {
            var totalCount = await _konuReadRepository.GetAll(false).CountAsync();
            var konular = await _konuReadRepository.GetAll(false).Include(o=>o.Ders).Skip((page - 1) * size).Take(size).ToListAsync();
            return new()
            {
                TotalKonu = totalCount,
                Konular = konular
            };
        }

        public async Task<ListAytKonular> GetAllAytKonular(int page, int size, List<string>? dersIds, string? konuAdi)
        {
            var query = _konuReadRepository.GetAll(false).Where(u => u.IsTyt == false).AsQueryable();
            if (!string.IsNullOrEmpty(konuAdi))
            {
                query = query.Where(u => u.KonuAdi.Contains(konuAdi));
            }
            if (dersIds != null && dersIds.Any())
            {
                query = query.Where(k => dersIds.Contains(k.Ders.Id.ToString()));
            }
            var totalCount = await query.CountAsync();
            var aytKonular = await query.Skip((page- 1) * size).Take(size).Select(u => new
            {
                Id=u.Id.ToString(),
                KonuAdi=u.KonuAdi,
                IsTyt=u.IsTyt,
                DersAdi=u.Ders.DersAdi
            }).ToListAsync();
            return new()
            {
                TotalCount = totalCount,
                AytKonular = aytKonular,
            };
        }

        public async Task<ListAllKonuPaginated> GetAllKonularPaginated(int page, int size, List<string>? dersIds, string? konuAdi, bool IsTyt = true)
        {
            var query = _konuReadRepository.GetAll(false).Where(u => u.IsTyt == IsTyt).AsQueryable();
            if (!string.IsNullOrEmpty(konuAdi)) 
            {
                query = query.Where(u => u.KonuAdi.ToLower().Contains(konuAdi.ToLower()));
            }
            if (dersIds != null && dersIds.Any())
            {
                query = query.Where(k => dersIds.Contains(k.Ders.Id.ToString()));
            }

            var totalCount = await query.CountAsync();
            var konular = await query.Skip((page - 1) * size).Take(size).Select(u => new
            {
                Id = u.Id.ToString(),
                KonuAdi = u.KonuAdi,
                IsTyt = u.IsTyt,
                DersAdi = u.Ders.DersAdi
            }).ToListAsync();
            return new()
            {
                TotalCount = totalCount,
                Konular = konular,
            };
        }

        public async Task<ListTytKonular> GetAllTytKonular(int page,int size, List<string>? dersIds, string? konuAdi)
        {
            var query = _konuReadRepository.GetAll(false).Where(u => u.IsTyt == true).AsQueryable();
            if (!string.IsNullOrEmpty(konuAdi))
            {
                query = query.Where(u => u.KonuAdi.Contains(konuAdi));
            }
            if (dersIds != null && dersIds.Any())
            {
                query = query.Where(k => dersIds.Contains(k.Ders.Id.ToString()));
            }
            var totalCount = await query.CountAsync();
            var tytKonular = await query.Skip((page - 1) * size).Take(size).Select(u => new
            {
                Id = u.Id.ToString(),
                KonuAdi = u.KonuAdi,
                IsTyt = u.IsTyt,
                DersAdi = u.Ders.DersAdi
            }).ToListAsync();
            return new()
            {
                TotalCount = totalCount,
                TytKonular = tytKonular,
            };
        }

        public async Task<ListAllKonu> GetKonuById(string id)
        {
            var konu=await _konuReadRepository.GetAll(false).Where(u=>u.Id.ToString()==id).Select(u => new ListAllKonu
            {
                Id = u.Id.ToString(),
                KonuAdi = u.KonuAdi,
                IsTyt = u.IsTyt,
                DersAdi = u.Ders.DersAdi,
                DersId=u.Ders.Id.ToString()
            }).FirstOrDefaultAsync();
            if (konu == null)
                throw new Exception("Konu bulunamadı.");
            return konu;
        }

        public async Task UpdateKonu(UpdateKonu updateKonu)
        {
            var konu = await _konuReadRepository.GetWhere(u=>u.Id.ToString()==updateKonu.KonuId).Include(u=>u.Ders).FirstOrDefaultAsync();
            if (konu == null)
                throw new Exception("Konu bulunamadı");

            var yeniDers = await _dersReadRepository.GetByIdAsync(updateKonu.DersId);

            var signalRMessage = $"{konu.KonuAdi} konusu üzerinde değişiklikler:\n";

            if (konu.KonuAdi != updateKonu.KonuAdi)
            {
                signalRMessage += $"Konu Adı: {konu.KonuAdi} => {updateKonu.KonuAdi}\n";
            }

            if (konu.Ders.Id != yeniDers.Id)
            {
                signalRMessage += $"Ders: {konu.Ders.DersAdi} => {yeniDers.DersAdi}\n";
            }

            if (string.IsNullOrWhiteSpace(signalRMessage))
            {
                signalRMessage = $"{konu.KonuAdi} üzerinde değişiklik yapılmadı.";
            }
            konu.IsTyt = yeniDers.IsTyt == true;
            konu.Ders = yeniDers;
            konu.KonuAdi = updateKonu.KonuAdi;

            await _konuHubService.KonuUpdatedMessage(signalRMessage);
            await _konuWriteRepository.SaveAsync();
        }

        public async Task<GetAllKonularPaginatedQueryResponse> GetAllKonular(int? page, int? size, List<string>? dersIds, string? konuOrDersAdi, bool? isTyt)
        {
            var query = _konuReadRepository.GetAll().Include(u => u.Ders).AsQueryable();
            if (isTyt!=null)
                query=query.Where(u=>u.IsTyt== isTyt);
            if (!string.IsNullOrEmpty(konuOrDersAdi))
            {
                string loweredKonuOrDersAdi = konuOrDersAdi.ToLower();
                query = query.Where(u => u.KonuAdi.ToLower().Contains(loweredKonuOrDersAdi)|| u.Ders.DersAdi.ToLower().Contains(loweredKonuOrDersAdi));
            }
            if (dersIds!=null && dersIds.Any())
            {
                query = query.Where(u => dersIds.Contains(u.Ders.Id.ToString()));
            }
            var totalCount= await query.CountAsync();
            if(page == null && size == null)
            {
                var data = await query.Select(u => new
                {
                    Id=u.Id.ToString(),
                    KonuAdi=u.KonuAdi,
                    IsTyt=u.IsTyt,
                    DersAdi=u.Ders.DersAdi,
                    DersId=u.Ders.Id
                }).ToListAsync();
                return new()
                {
                    TotalCount = totalCount,
                    Konular = data
                };
            }
            var konular = await query.Skip((page.Value - 1) * size.Value).Take(size.Value).Select(u => new
            {
                Id = u.Id.ToString(),
                KonuAdi = u.KonuAdi,
                DersAdi = u.Ders.DersAdi,
                IsTyt = u.IsTyt,
                DersId = u.Ders.Id
            }).ToListAsync();
            return new()
            {
                TotalCount = totalCount,
                Konular = konular
            };
        }
        
    }
}
