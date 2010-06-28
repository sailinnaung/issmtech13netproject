using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class UserInvoiceRepo
    {
        LibrarySystemDBEntities Entities;
        const int intialInvoiceID = 1000;

        public UserInvoiceRepo()
        {
            Entities = new LibrarySystemDBEntities();
        }

        public int GetLastUserInvoiceID()
        {
            int invoiceID = intialInvoiceID;
            try
            {
                var data = (from uiv in Entities.UserInvoices
                            select uiv).OrderByDescending(x => x.InvoiceID);

                if (data != null)
                {
                    invoiceID = data.First().InvoiceID + 1;
                }                
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);                
            }
            return invoiceID;
        }
    }
}
