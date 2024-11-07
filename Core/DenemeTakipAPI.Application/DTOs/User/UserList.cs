using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.User
{
    public class UserList
    {
        public int TotalCount { get; set; }
        public object Users { get; set; }

    }
    public class UserListSingle
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public bool IsAdmin { get; set; }

    }
}
