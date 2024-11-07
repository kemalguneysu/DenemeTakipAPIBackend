using DenemeTakipAPI.Application.DTOs.Ders;
using DenemeTakipAPI.Application.DTOs.Konu;
using DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonularPaginated;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Services
{
    public interface IKonuService
    {
        Task CreateKonuAsync(CreateKonu createKonu);
        Task<DeleteKonuResponse> DeleteKonu(List<string> ids);
        Task<ListKonu> Get(int page, int size);
        Task UpdateKonu(UpdateKonu updateKonu);

        Task<ListTytKonular> GetAllTytKonular(int page, int size, List<string>? dersIds,string? konuAdi);
        Task<ListAytKonular> GetAllAytKonular(int page, int size, List<string>? dersIds, string? konuAdi);
        Task<ListAllKonu> GetKonuById(string id);
        Task<ListAllKonuPaginated> GetAllKonularPaginated(int page, int size, List<string>? dersIds, string? konuAdi,bool IsTyt=true);
        Task<GetAllKonularPaginatedQueryResponse> GetAllKonular(int? page,int? size,List<string>? dersIds,string? konuOrDersAdi,bool? isTyt);


    }
}
