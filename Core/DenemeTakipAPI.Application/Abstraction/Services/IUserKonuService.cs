using DenemeTakipAPI.Application.DTOs;
using DenemeTakipAPI.Application.DTOs.UserKonu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Abstraction.Services
{
    public interface IUserKonuService
    {
        Task CreateUserKonuAsync(List<string> konuIds);
    }
}
