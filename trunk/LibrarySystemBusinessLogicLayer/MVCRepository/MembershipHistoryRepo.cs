using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrarySystemBusinessLogicLayer;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class MembershipHistoryRepo
    {
        LibrarySystemDBEntities Entities;
        const int intialMHistoryID = 1000;

        public MembershipHistoryRepo()
        {
            Entities = new LibrarySystemDBEntities();
        }

        public int GetLatestMembershipHistoryID()
        {
            int mHistoryID = intialMHistoryID;
            try
            {
                var data = (from mh in Entities.MembershipHistories
                            select mh).OrderByDescending(x => x.MembershipHistoryID);

                if (data != null)
                {
                    mHistoryID = data.First().MembershipHistoryID + 1;
                }   
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);                
            }
            return mHistoryID;
        }

        public MembershipHistory GetCurrentMembership(User usr)
        {
            try
            {
                var data = Entities.MembershipHistories.Where(x => x.NRIC == usr.NRIC).OrderByDescending(y => y.MemberStatusChangeDate);

                if (data != null)
                {
                    return data.First();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public DateTime GetCurrentMembershipExpiryDate(int mshipID)
        {
            DateTime dtExpiry = new DateTime() ;

            try
            {
                var data = Entities.MembershipHistories.Where(x => x.MembershipHistoryID == mshipID).First();

                if (data != null)
                {
                    DateTime dtMship = data.MemberStatusChangeDate;
                    //int yrOfMship = dtMship.Year;
                    int NoOfYr = data.NumOfYearOfMemberShip;
                    //yrOfMship = yrOfMship + NoOfYr;
                    dtExpiry = dtMship;
                    dtExpiry.AddYears(NoOfYr);

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return dtExpiry;
        }
    }
}
