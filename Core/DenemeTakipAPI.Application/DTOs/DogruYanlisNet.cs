using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs
{
    public class DogruYanlisNet
    {
        public int Dogru { get; set; }
        public int Yanlis { get; set; }
        public float Net { get; set; }

        public DogruYanlisNet(int dogru,int yanlis)
        {
            Dogru = dogru;
            Yanlis = yanlis;
            Net = (float)(dogru - 0.25m * yanlis);
        }
    }
}
