using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrarySystemBusinessLogicLayer.MVCRepository;

namespace LibrarySystemBusinessLogicLayer.BusinessFacade
{
    public class LoanReservedFacade
    {
        private LibrarySystemDBEntities entities;

        public LoanReservedFacade()
        {
            entities = new LibrarySystemDBEntities();
        }

        public List<LoanRecord> GetRecordByNric(string NRIC)
        {
            var data = entities.LoanRecords.Where(x => x.NRIC == NRIC).ToList();

            return data;
        }

        public List<ReservedBook> GetReservedByNric(string NRIC)
        {
            var data = entities.ReservedBooks.Where(x => x.NRIC == NRIC).ToList();

            return data;
        }
    }
}
