using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrarySystemBusinessLogicLayer.MVCRepository;

namespace LibrarySystemBusinessLogicLayer.Utility
{
    public sealed class LibraryDBInstance
    {
        static readonly LibrarySystemDBEntities instance = new LibrarySystemDBEntities();

        static LibraryDBInstance() { }

        LibraryDBInstance() { }

        public static LibrarySystemDBEntities Instance
        {
            get { return instance; }
        }
    }
}
