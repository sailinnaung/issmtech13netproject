using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class BookCategoryRepo
    {
        LibrarySystemDBEntities entities = new LibrarySystemDBEntities();
        public IEnumerable<BookCategory> List()
        {
            return entities.BookCategories.ToList();
        }

        public BookCategory Get(int index)
        {
            return (from p in entities.BookCategories
                    where p.CategoryID == index
                    select p).FirstOrDefault();
        }
    }
}
