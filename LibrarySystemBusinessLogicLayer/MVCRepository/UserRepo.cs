using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LibrarySystemBusinessLogicLayer;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public enum CitizenStatus
    {
        [DescriptionAttribute("Citizen")]
        CITIZEN,
        [DescriptionAttribute("Singapore Permanent Residence")]
        SPR,
        [DescriptionAttribute("Foreigner")]
        FORIEGNER
    }

    public enum Membership
    {
        [DescriptionAttribute("Basic Member")]
        BASIC_MEMBER,
        [DescriptionAttribute("Premium Member")]
        PREMIUM_MEMBER,
        [DescriptionAttribute("PremiumPlus Member")]
        PREMIUMPLUS_MEMBER
    }

    public enum AccountStatus
    {
        [Description("Live")]
        LIVE,
        [Description("Dead")]
        DEAD
    }

    public class UserRepo
    {
        LibrarySystemDBEntities entities = new LibrarySystemDBEntities();

        public UserRepo(LibrarySystemDBEntities libDB)
        {
            entities = libDB;
        }

        public UserRepo()
        {
        }

        public User GetUserByNRIC(String NRIC)
        {
            var data = entities.Users.Where(x => x.NRIC.Equals(NRIC)).FirstOrDefault();
            if (data!= null)
            {
                return data;
            }
            else
            {
                return null;
            }
            
        }

        public User GetUserByFirstName(String FirstName)
        {
            var data = from u in entities.Users
                       where u.FirstName == FirstName
                       select u;
            User usr = data.First();
            return usr;
        }

        public bool UpgradeUser(User user)
        {
            return false;
        }

        public bool AddNewUser(User newUser)
        {
            entities.AddToUsers(newUser);
            entities.SaveChanges();
            return false;
        }

        public IEnumerable<User> List()
        {
            return entities.Users.ToList();
        }
        public User Get(string id)
        {
            return (from u in entities.Users
                    where u.NRIC == id      //?
                    select u).FirstOrDefault();
        }
        public void Create(User userToCreate)
        {
            entities.AddToUsers(userToCreate);
        }

        public void Save()
        {
            entities.SaveChanges();
        }
        public void Delete(User bookToDelete)
        {
            var originalUser = Get(bookToDelete.NRIC);
            if (originalUser != null)
            {
                entities.DeleteObject(originalUser);
                entities.SaveChanges();
            }
        }

    }
}
