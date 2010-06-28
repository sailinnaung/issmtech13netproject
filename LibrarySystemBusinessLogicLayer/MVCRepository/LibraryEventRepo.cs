using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    #region RepositoryModels
    public class LibraryEventRepo
    {
        private LibrarySystemDBEntities _entities = new LibrarySystemDBEntities();
        public void Add(LibraryEvent eventToAdd)
        {
            _entities.AddToLibraryEvents(eventToAdd);
            _entities.SaveChanges();
        }

        public LibraryEvent Get(int index)
        {
            return (from p in _entities.LibraryEvents
                    where p.EventID == index
                    select p).FirstOrDefault();
        }

        public void Delete(LibraryEvent eventToDelete)
        {
            var original = Get(eventToDelete.EventID);
            _entities.DeleteObject(original);
            _entities.SaveChanges();
        }

        public IEnumerable<LibraryEvent> GetAll(string sortId = "eventId", string sortOrder = "desc")
        {
            if (sortId == "EventId")
                if (sortOrder == "desc")
                    return _entities.LibraryEvents.ToList().OrderByDescending(x => x.EventID).ToList();
                else
                    return _entities.LibraryEvents.ToList().OrderBy(x => x.EventID).ToList();
            else if (sortId == "Title")
                if (sortOrder == "desc")
                    return _entities.LibraryEvents.ToList().OrderByDescending(x => x.Title).ToList();
                else
                    return _entities.LibraryEvents.ToList().OrderBy(x => x.Title).ToList();
            else if (sortId == "EventDetail")
                if (sortOrder == "desc")
                    return _entities.LibraryEvents.ToList().OrderByDescending(x => x.EventDetail).ToList();
                else
                    return _entities.LibraryEvents.ToList().OrderBy(x => x.EventDetail).ToList();
            else if (sortId == "EventDate")
                if (sortOrder == "desc")
                    return _entities.LibraryEvents.ToList().OrderByDescending(x => x.EventDate).ToList();
                else
                    return _entities.LibraryEvents.ToList().OrderBy(x => x.EventDate).ToList();
            else if (sortId == "EventTime")
                if (sortOrder == "desc")
                    return _entities.LibraryEvents.ToList().OrderByDescending(x => x.EventTime).ToList();
                else
                    return _entities.LibraryEvents.ToList().OrderBy(x => x.EventTime).ToList();
            else if (sortId == "Venue")
                if (sortOrder == "desc")
                    return _entities.LibraryEvents.ToList().OrderByDescending(x => x.Venue).ToList();
                else
                    return _entities.LibraryEvents.ToList().OrderBy(x => x.Venue).ToList();
            else
                return _entities.LibraryEvents.ToList().OrderBy(x => x.EventID).ToList();
        }

        public void Edit(LibraryEvent eventToEdit)
        {
            var original = Get(eventToEdit.EventID);
            _entities.ApplyCurrentValues(original.EntityKey.EntitySetName, eventToEdit);
            _entities.SaveChanges();
        }
    }
    #endregion
}