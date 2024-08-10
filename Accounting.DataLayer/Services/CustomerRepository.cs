using Accounting.DataLayer.Repositories;
using Accounting.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private Accounting_DBEntities _db;

        public CustomerRepository(Accounting_DBEntities context)
        {
            _db = context;
        }
        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                _db.Entry(customer).State = EntityState.Deleted;
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public List<Customers> GetAllCustomers()
        {
            return _db.Customers.ToList();
        }

        public IEnumerable<Customers> GetCustomerByFilter(string parameter)
        {
            return _db.Customers.Where(c => c.FullName.ToLower().Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return _db.Customers.Find(customerId);
        }

        public int GetCustomerIdByName(string name)
        {
            return _db.Customers.First(c => c.FullName == name).CustomerID;
        }

        public string GetCustomerNameById(int customerId)
        {
            return _db.Customers.Find(customerId).FullName.ToString();
        }

        public List<ListCustomerViewModel> GetCustomers(string filter = "")
        {
            if (filter == "")
            {
                return _db.Customers.Select(c => new ListCustomerViewModel()
                {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName
                }).ToList();
            }
            return _db.Customers.Where(c => c.FullName.Contains(filter)).Select(c => new ListCustomerViewModel()
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName
            }).ToList();
        }

        public bool InsertCustomer(Customers customer)
        {
            try
            {
                _db.Customers.Add(customer);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateCustomer(Customers customer)
        {
            try
            {
                var local = _db.Set<Customers>()
                         .Local
                         .FirstOrDefault(f => f.CustomerID == customer.CustomerID);
                if (local != null)
                {
                    _db.Entry(local).State = EntityState.Detached;
                }

                _db.Entry(customer).State = EntityState.Modified;
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
