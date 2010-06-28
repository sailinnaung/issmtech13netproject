using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    #region RepositoryModels
    public class BookOrderRepo
    {
        LibrarySystemDBEntities _entities = new LibrarySystemDBEntities();

        public int Create(BookOrder modelToAdd)
        {
            _entities.AddToBookOrders(modelToAdd);
            _entities.SaveChanges();
            return modelToAdd.OrderID;
        }

        public BookOrder Get(int index)
        {
            return (from p in _entities.BookOrders
                    where p.OrderID == index
                    select p).FirstOrDefault();
        }

        public IEnumerable<BookOrder> List()
        {
            return _entities.BookOrders.ToList();
        }

        public void Edit(BookOrder modelToEdit)
        {
            var original = Get(modelToEdit.OrderID);
            _entities.ApplyCurrentValues(original.EntityKey.EntitySetName, modelToEdit);
            _entities.SaveChanges();
        }

    }
    #endregion
}