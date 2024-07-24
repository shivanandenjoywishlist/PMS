﻿using COMMON;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Amazon
{
    public interface IAmazon
    {
        Task<JsonModel> CreateProductAsync(Products product);
        Task<JsonModel> UpdateProduct(Products product);
        Task<JsonModel> GetProducts();
        Task<JsonModel> DeleteProduct(int Id);
    }
}
