using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.DTOs.Ders;
using DenemeTakipAPI.Application.Repositories.DersRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DenemeTakipAPI.Application.DTOs.Konu;
using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;

namespace DenemeTakipAPI.Persistence.Services
{
    public class DersService:IDersService
    {
        readonly IDersReadRepository _dersReadRepository;
        readonly IDersWriteRepository _dersWriteRepository;
        readonly IDersHubService _dersHubService;
        public DersService(IDersReadRepository dersReadRepository, IDersWriteRepository dersWriteRepository, IDersHubService dersHubService)
        {
            _dersReadRepository = dersReadRepository;
            _dersWriteRepository = dersWriteRepository;
            _dersHubService = dersHubService;
        }

        public async Task CreateDersAsync(CreateDers createDers)
        {
            await _dersWriteRepository.AddAsync(new()
            {
                DersAdi = createDers.DersAdi,
                IsTyt= createDers.IsTyt,
            });
            var signalRMessage = $"Yeni bir ders eklendi:\n";
            signalRMessage += $"Ders Adı: {createDers.DersAdi}\n";
            signalRMessage += $"Ders Türü: {(createDers.IsTyt ? "TYT" : "AYT")}";
            await _dersHubService.DersAddedMessage(signalRMessage);
            await _dersWriteRepository.SaveAsync();
        }

        public async Task<DeleteDersResponse> DeleteDers(List<string> ids)
        {
            var dersler=await _dersReadRepository.GetWhere(p=>ids.Contains(p.Id.ToString())).ToListAsync();
            var signalRMessage = $"Dersler silindi:\n";
            if (dersler!=null && dersler.Any())
            {
                foreach (var ders in dersler)
                {
                    signalRMessage += $"{ders.DersAdi}\n"; // Konu adını ekliyoruz
                }
                _dersWriteRepository.RemoveRange(dersler);
                await _dersWriteRepository.SaveAsync();
                await _dersHubService.DersDeletedMessage(signalRMessage);
                return new()
                {
                    Succeeded = true,
                    Message = "Seçilen dersler başarıyla silinmiştir."
                };
            }
            else
            {
                return new()
                {
                    Succeeded = false,
                    Message = "Silinecek ders bulunamadı."
                };
            }
        }

        public async Task<ListDers> Get(int page, int size)
        {
            var totalCount = await _dersReadRepository.GetAll(false).CountAsync();
            var dersler= await _dersReadRepository.GetAll().Skip((page - 1) * size).Take(size).Select(s => new
            {
                Id= s.Id,
                DersAdi= s.DersAdi,
                IsTyt= s.IsTyt,
            }).ToListAsync();
            return new()
            {
                TotalDers = totalCount,
                Dersler= dersler
            };
        }

        //public async Task<List<ListAllDers>> GetAllDersler(bool? isTyt, string? dersAdi, int? page, int? size)
        //{
        //    var query = _dersReadRepository.GetAll(false).AsQueryable();
        //    if (isTyt != null)
        //    {
        //        query = query.Where(i => i.IsTyt == isTyt);
        //    }

        //    if (!string.IsNullOrEmpty(dersAdi))
        //    {
        //        query = query.Where(i => i.DersAdi.ToLower().Contains(dersAdi.ToLower()));
        //    }

        //    if (page == null || size == null)
        //    {
        //        var data = await query.Select(u => new ListAllDers
        //        {
        //            Id = u.Id.ToString(),
        //            DersAdi = u.DersAdi,
        //            IsTyt = u.IsTyt,
        //        }).ToListAsync();

        //        return data;
        //    }
        //    var totalCount = await query.CountAsync();
        //    var dersler = await query
        //        .Skip((page.Value - 1) * size.Value)
        //        .Take(size.Value)
        //        .Select(u => new ListAllDers
        //        {
        //            Id = u.Id.ToString(),
        //            DersAdi = u.DersAdi,
        //            IsTyt = u.IsTyt,

        //        }).ToListAsync();

        //}
        public async Task<object> GetAllDersler(bool? isTyt,string? dersAdi, int? page, int? size)
        {
            var query =  _dersReadRepository.GetAll(false).AsQueryable();
            if (isTyt != null)
            {
                query = query.Where(i => i.IsTyt == isTyt);
            }

            if (!string.IsNullOrEmpty(dersAdi))
            {
                query = query.Where(i => i.DersAdi.ToLower().Contains(dersAdi.ToLower()));
            }
            var totalCount = await query.CountAsync();

            if (page == null || size == null)
            {
                var data = await query.Select(u => new ListAllDers
                {
                    Id = u.Id.ToString(),
                    DersAdi = u.DersAdi,
                    IsTyt = u.IsTyt,
                }).ToListAsync();

                return new
                {
                    TotalCount = totalCount,
                    Dersler = data
                };
            }
            var dersler = await query
                .Skip((page.Value - 1) * size.Value)
                .Take(size.Value)
                .Select(u => new ListAllDers
                {
                    Id = u.Id.ToString(),
                    DersAdi = u.DersAdi,
                    IsTyt = u.IsTyt,
                }).ToListAsync();
            return new
            {
                TotalCount = totalCount,
                Dersler=dersler
            };

        }

        

        public async Task<ListAllDers> GetDersById(string id)
        {
            var ders = await _dersReadRepository.GetAll(false).Where(u => u.Id.ToString() == id).Select(u => new ListAllDers
            {
                Id = u.Id.ToString(),
                IsTyt = u.IsTyt,
                DersAdi = u.DersAdi
            }).FirstOrDefaultAsync();
            if (ders == null)
                throw new Exception("Ders bulunamadı.");
            return ders;
        }

       

        public async Task UpdateDers(UpdateDers updateDers)
        {
            var ders = await _dersReadRepository.GetByIdAsync(updateDers.DersId);
            if (ders == null)
                throw new Exception("Ders bulunamadı");
            var signalRMessage = $"{ders.DersAdi} dersi üzerinde değişiklikler:\n";

            if (ders.DersAdi != updateDers.DersAdi)
            {
                signalRMessage += $"Ders Adı: {ders.DersAdi} => {updateDers.DersAdi}\n";
            }

            if (ders.IsTyt != updateDers.IsTyt)
            {
                signalRMessage += $"Ders Türü: {(ders.IsTyt ? "TYT" : "AYT")} => {(updateDers.IsTyt ? "TYT" : "AYT")}\n";
            }

            if (string.IsNullOrWhiteSpace(signalRMessage))
            {
                signalRMessage = $"{ders.DersAdi} üzerinde değişiklik yapılmadı.";
            }
            ders.DersAdi = updateDers.DersAdi;
            ders.IsTyt = updateDers.IsTyt;
            await _dersHubService.DersUpdatedMessage(signalRMessage);
            await _dersWriteRepository.SaveAsync();
        }
    }
}
