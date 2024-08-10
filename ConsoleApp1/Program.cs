using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Accounting_DBEntities db=new Accounting_DBEntities();   
            GenericRepository<Customers> customerRepository = new GenericRepository<Customers>(db);
            var result=customerRepository.Get(c=>c.FullName.Contains("فرشاد")).ToList();
        }
    }
}
