using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class AddressRepo
    {
        LibrarySystemDBEntities LibSysContex;

        public AddressRepo(LibrarySystemDBEntities libDB)
        {
            LibSysContex = libDB;
        }

    }
}
