using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class AnnouncementMSGRepo
    {
        LibrarySystemDBEntities LibSysContex;

        public AnnouncementMSGRepo(LibrarySystemDBEntities libDB)
        {
            LibSysContex = libDB;
        }
    }
}
