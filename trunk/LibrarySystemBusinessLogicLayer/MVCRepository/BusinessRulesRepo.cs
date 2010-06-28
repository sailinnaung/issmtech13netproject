using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    #region RepositoryModels
    public class BusinessRulesRepo
    {
        private LibrarySystemDBEntities _entities = new LibrarySystemDBEntities();

        public BusinessRule Get(int index)
        {
            return (from p in _entities.BusinessRules
                    where p.RuleID == index
                    select p).FirstOrDefault();
        }

        public IEnumerable<BusinessRule> GetAll()
        {
            return _entities.BusinessRules.ToList();
        }

        public void Edit(BusinessRule modelToEdit)
        {
            var original = Get(modelToEdit.RuleID);
            _entities.ApplyCurrentValues(original.EntityKey.EntitySetName, modelToEdit);
            _entities.SaveChanges();
        }

        public void Add(BusinessRule modelToAdd)
        {
            _entities.AddToBusinessRules(modelToAdd);
            _entities.SaveChanges();
        }
                
    }
    #endregion
}