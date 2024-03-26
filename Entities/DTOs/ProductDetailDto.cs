﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetailDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string CategoryName { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
    }
}
