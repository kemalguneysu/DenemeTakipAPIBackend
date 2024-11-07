using DenemeTakipAPI.Application.DTOs.Ders;
using DenemeTakipAPI.Application.DTOs.Konu;
using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Services
{
    public interface IDersService
    {
        Task CreateDersAsync(CreateDers createDers);
        Task<DeleteDersResponse> DeleteDers(List<string> ids);
        Task<ListDers> Get(int page,int size);
        Task UpdateDers(UpdateDers updateDers);

        Task<object> GetAllDersler(bool? isTyt, string? dersAdi, int? page, int? size);

        Task<ListAllDers> GetDersById(string id);


    }
}
