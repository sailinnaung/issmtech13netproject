using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LibrarySystemBusinessLogicLayer;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public enum AccessRole
    {
        [DescriptionAttribute("Basic User")]
        BASIC_USER,
        [DescriptionAttribute("Premium User")]
        PREMIUM_USER,
        [DescriptionAttribute("Premium Plus User")]
        PREMIUMPLUS_USER,
        [DescriptionAttribute("Librarian")]
        LIBRARIAN,
        [DescriptionAttribute("Senior Librarian")]
        SENIOR_LIBRARIAN,
        [DescriptionAttribute("Management")]
        MANAGEMENT,
        [DescriptionAttribute("Admin")]
        ADMIN
    }

    #region Models
    public partial class AccessInfo
    {
        public bool IsInRole(string role)
        {
            if (this.Role == role)
                return true;
            else
                return false;
        }

        public string Role
        {
            get { return this.Role.ToString(); }
        }
    }
    #endregion

    #region RepositoryModels
    public class AccessInfoRepo
    {
        private LibrarySystemDBEntities entities = new LibrarySystemDBEntities();

        public AccessInfoRepo(LibrarySystemDBEntities libDB)
        {
            entities = libDB;
        }

        public AccessInfoRepo()
        {
        }

        public AccessInfo Get(string _nric)
        {
            return (from p in entities.AccessInfoes
                    where p.NRIC == _nric
                    select p).FirstOrDefault();
        }

        public void Edit(AccessInfo modelToEdit)
        {
            var original = Get(modelToEdit.NRIC);
            entities.ApplyCurrentValues(original.EntityKey.EntitySetName, modelToEdit);
            entities.SaveChanges();
        }
    }
    #endregion
}
