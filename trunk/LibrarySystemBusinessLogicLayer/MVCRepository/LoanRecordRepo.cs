using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{

    public class LoanRecordRepository
    {
        private LibrarySystemDBEntities Entity = new LibrarySystemDBEntities();

        public IEnumerable<LoanRecord> GetList()
        {
            return Entity.LoanRecords.ToList();
        }
        public IEnumerable<LoanRecord> GetLoanRecordIByUser(string nric)
        {
            return from lr in Entity.LoanRecords
                   where lr.NRIC == nric
                   select lr;
        }

        public IEnumerable<LoanRecord> GetLoanRecordIByISBN(string isbn)
        {
            return from lr in Entity.LoanRecords
                   where lr.ISBN == isbn
                   select lr;
        }

        public LoanRecord GetLoanRecordItem(int LoanRecordID)
        {
            return (from lr in Entity.LoanRecords
                    where lr.LoanRecordID == LoanRecordID
                    select lr).FirstOrDefault();
        }

        public void Save()
        {
            Entity.SaveChanges();
        }

        public void Add(LoanRecord lr)
        {
            Entity.AddToLoanRecords(lr);
        }

        public void Delete(LoanRecord lr)
        {
            var originalLR = GetLoanRecordItem(lr.LoanRecordID);
            if (originalLR != null)
            {
                Entity.DeleteObject(originalLR);
                Entity.SaveChanges();
            }
        }
    }
}