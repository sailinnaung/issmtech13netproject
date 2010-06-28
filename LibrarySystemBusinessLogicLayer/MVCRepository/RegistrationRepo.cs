using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class RegistrationRepo
    {
        LibrarySystemDBEntities Entities;

        public RegistrationRepo(LibrarySystemDBEntities libDB)
        {
            Entities = libDB;
        }

        public Registration GetLatestRegistration(User usr)
        {
            try
            {
                var data = Entities.Registrations.Where(x => x.NRIC == usr.NRIC).OrderByDescending(y => y.RegistrationDate);

                if (data != null)
                {
                    return data.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

        }

    }
}
