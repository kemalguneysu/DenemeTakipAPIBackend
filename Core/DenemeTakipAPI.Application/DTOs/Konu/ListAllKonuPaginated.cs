﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Konu
{
    public class ListAllKonuPaginated
    {
        public int TotalCount { get; set; }
        public object Konular { get; set; }
    }
}
