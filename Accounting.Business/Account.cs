using Accounting.DataLayer.Context;
using Accounting.ViewModels.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportForMain()
        {
            ReportViewModel report = new ReportViewModel();
            using (UnitOfWork db = new UnitOfWork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30);

                var recieve = db.AccountingRepository.Get(r => r.TypeID == 1 && r.DateTime >= startDate && r.DateTime <= endDate).Select(r => r.Amount).ToList();
                var pay = db.AccountingRepository.Get(r => r.TypeID == 2 && r.DateTime >= startDate && r.DateTime <= endDate).Select(r => r.Amount).ToList();

                report.Recieve = recieve.Sum();
                report.Pay = pay.Sum();
                report.Rest=(recieve.Sum()-pay.Sum());


            }
            return report;
        }
    }
}
