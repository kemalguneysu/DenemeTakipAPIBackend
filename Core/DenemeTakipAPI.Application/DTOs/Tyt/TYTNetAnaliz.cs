using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Tyt
{
    public class TYTNetAnaliz
    {
        public string Id { get; set; }               
        public DateTime CreatedDate { get; set; }    
        public int Dogru { get; set; }          
        public int Yanlis { get; set; }            
        public double Net { get; set; }
        public TYTNetAnaliz() { }

        public TYTNetAnaliz(string id, DateTime createdDate, int dogru, int yanlis, double net)
        {
            Id = id;
            CreatedDate = createdDate;
            Dogru = dogru;
            Yanlis = yanlis;
            Net = net;
        }
    }

}
