using COMMON;
using Microsoft.EntityFrameworkCore;
using PMS_DAL.IRepositories.ProviderSync;
using PMS_DAL.Repositories.Common;
using PMS_DATA;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PMS_DAL.Repositories.ProviderSync
{
    public class AmazonRepository: IAmazonRepository
    {
        JsonModel responce = null;
        private readonly ApplicationContext _context;

        public AmazonRepository(ApplicationContext context)
        {
            _context=context;
        }

        public async Task<List<AmazonProducts>> GetProduct(string RefrenceId)
        {
            List<AmazonProducts> data = new List<AmazonProducts>();

            try
            {
                // Ensure that _context is properly injected and available
                data =  _context.AmazonProducts
                                     // .Where(p => p.RefrenceId == RefrenceId)
                                      .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's error handling strategy
                Console.WriteLine($"Error in GetProduct method: {ex.Message}");
                throw; // Optionally, re-throw the exception to propagate it further
            }

            return data;
        }

    }
}
