using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    #region RepositoryModelRepo
    public class BookOrderDetailRepo
    {
        LibrarySystemDBEntities _entities = new LibrarySystemDBEntities();

        public void Create(BookOrderDetail modelToAdd)
        {
            _entities.AddToBookOrderDetails(modelToAdd);
            _entities.SaveChanges();
        }

        public BookOrderDetail Get(int index)
        {
            return (from p in _entities.BookOrderDetails
                    where p.OrderID == index
                    select p).FirstOrDefault();
        }

    }
    #endregion
}