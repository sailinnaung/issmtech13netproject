using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class PublisherRepo
    {
        private LibrarySystemDBEntities Entity = new LibrarySystemDBEntities();
        public IEnumerable<Publisher> GetList()
        {
            return Entity.Publishers.ToList();
        }

        public Publisher GetPublisher(int id)
        {
            return (from p in Entity.Publishers
                    where p.PublisherID == id
                    select p).FirstOrDefault();
        }
        public void Create(Publisher p)
        {
            Entity.AddToPublishers(p);
        }

        public void Save()
        {
            Entity.SaveChanges();
        }

        public void Delete(Publisher p)
        {
            var originalPublisher = GetPublisher(p.PublisherID);
            if (originalPublisher != null)
            {
                Entity.DeleteObject(originalPublisher);
                Save();
            }
        }
    }
}
