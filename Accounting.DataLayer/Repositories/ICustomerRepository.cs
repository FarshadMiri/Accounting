﻿using Accounting.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Repositories
{
    public interface ICustomerRepository
    {
        List<Customers> GetAllCustomers();
        IEnumerable<Customers> GetCustomerByFilter(string parameter);
        List<ListCustomerViewModel> GetCustomers(string filter = "");
        Customers GetCustomerById(int customerId);
        string GetCustomerNameById(int customerId);
        int GetCustomerIdByName(string name);   
        bool InsertCustomer(Customers customer);
        bool UpdateCustomer(Customers customer); 
        bool DeleteCustomer(Customers customer);
        bool DeleteCustomer(int customerId);
    }
}
