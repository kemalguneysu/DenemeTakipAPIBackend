using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using DenemeTakipAPI.Domain.Entities.ToDo;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public List<AytDeneme> AytDenemes { get; set; } = new();
        public List<TytDeneme> TytDenemes { get; set; } = new();
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public List<UserKonu> UserKonular{ get; set; } = new();
        public List<ToDoElement> ToDoElements{ get; set; } = new();


    }
}
