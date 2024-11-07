using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.Ayt;
using DenemeTakipAPI.Application.DTOs.Ders;
using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Services
{
    public interface IAytService
    {
        Task CreateAytAsync(CreateAyt createAyt);
        Task<ListAyt> Get(int page, int size,List<string>? orderByAndDirections);
        Task UpdateAytAsync(UpdateAyt updateAyt);
        Task<ListSingleAyt> GetAytById(string id);

        Task<DeleteAytResponse> DeleteAyt(List<string> ids);
        Task<List<DenemeAnaliz>> GetAytAnaliz(int DenemeSayısı, int KonuSayısı, string DersId, string type);
        Task<ListAytAnaliz> GetAytNetAnaliz(int denemeSayisi, string alanTuru, string? dersAdi);


    }
}
