using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.Ayt;
using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Services
{
    public interface ITytService
    {
        Task CreateTytAsync(CreateTyt createTyt);
        Task<ListTyt> Get(int page, int size,List<string>? orderByAndDirections);
        Task UpdateTytAsync(UpdateTyt updateTyt);
        Task<ListSingleTyt> GetTytById(string id);
        Task<DeleteTytResponse> DeleteTyt(List<string> ids);
        Task<List<DenemeAnaliz>> GetTytAnaliz(int DenemeSayısı, int KonuSayısı, string DersId,string type);
        Task<ListTytAnaliz> GetTytNetAnaliz(int denemeSayisi,string? dersAdi);


    }
}
