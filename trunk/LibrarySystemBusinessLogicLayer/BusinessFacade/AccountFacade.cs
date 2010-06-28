using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Transactions;
using LibrarySystemBusinessLogicLayer.Utility;
using LibrarySystemBusinessLogicLayer.MVCRepository;

namespace LibrarySystemBusinessLogicLayer.BusinessFacade
{
    public class AccountFacade
    {
        LibrarySystemDBEntities Entities;

        public AccountFacade()
        {
            Entities = new LibrarySystemDBEntities();
        }

        public User GetUser(String NRIC)
        {
            return new UserRepo(Entities).GetUserByNRIC(NRIC);
        }

        public bool isExistingUser(string NRIC)
        {
            bool isExist = false;
            try
            {               
                User usr = GetUser(NRIC);
                if (usr != null)
                {
                    isExist = true;
                }
                
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Source: " + e.Source + " and Message: " + e.Message);                
            }
            return isExist;
        }

        public bool RegisterNewUser(User usr, AccessInfo aif, Address addr, UserInvoice uiv,
            MembershipHistory msh, Registration reg)
        {
            try
            {
                using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    Entities.AddToUsers(usr);
                    
                    Entities.AddToAccessInfoes(aif);
                    Entities.AddToAddresses(addr);
                    Entities.AddToUserInvoices(uiv);
                    Entities.AddToMembershipHistories(msh);
                    Entities.AddToRegistrations(reg);
                   
                    Entities.SaveChanges();

                    tran.Complete();                    
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Source: " + e.Source + " and Message: " + e.Message);
                return false;
            }            
        }

        public decimal GetRegistrationFees(CitizenStatus citiStatus, Int32 NumOfYear,Membership Mship)
        {
            decimal TotalRegFee = -1;
            decimal RegFee;
            RegFeeConstant RFC = new RegFeeConstant();
            switch (Mship)
            {
                case Membership.BASIC_MEMBER:
                    {
                        switch (citiStatus)
                        {
                            case CitizenStatus.CITIZEN:
                                {
                                    RegFee = RFC.Basic_Citizen; break;
                                }
                            case CitizenStatus.SPR:
                                {
                                    RegFee = RFC.Basic_SPR; break;
                                }
                            case CitizenStatus.FORIEGNER:
                                {
                                    RegFee = RFC.Basic_Foriegner; break;
                                }
                            default: 
                                {
                                    throw new Exception("There is no Citizenstatus in application for passing citiStatus parameter value: "+citiStatus+" !");                                   
                                }
                        }
                        
                        TotalRegFee = RegFee * NumOfYear;

                        break;
                    }
                case Membership.PREMIUM_MEMBER:
                    {
                        switch (citiStatus)
                        {
                            case CitizenStatus.CITIZEN:
                                {
                                    RegFee = RFC.Premium_Citizen; break;
                                }
                            case CitizenStatus.SPR:
                                {
                                    RegFee = RFC.Premium_SPR; break;
                                }
                            case CitizenStatus.FORIEGNER:
                                {
                                    RegFee = RFC.Premium_Foriegner; break;
                                }
                            default:
                                {
                                    throw new Exception("There is no Citizenstatus in application for passing citiStatus parameter value: " + citiStatus + " !");
                                }
                        }
                        
                        TotalRegFee = RegFee * NumOfYear;

                        break;
                    }
                case Membership.PREMIUMPLUS_MEMBER:
                    {
                        switch (citiStatus)
                        {
                            case CitizenStatus.CITIZEN:
                                {
                                    RegFee = RFC.PremiumPlus_Citizen; break;
                                }
                            case CitizenStatus.SPR:
                                {
                                    RegFee = RFC.PremiumPlus_SPR; break;
                                }
                            case CitizenStatus.FORIEGNER:
                                {
                                    RegFee = RFC.PremiumPlus_Foriegner; break;
                                }
                            default:
                                {
                                    throw new Exception("There is no Citizenstatus in application for passing citiStatus parameter value: " + citiStatus + " !");
                                }
                        }
                        
                        TotalRegFee = RegFee * NumOfYear;

                        break;
                    }
            }
            return TotalRegFee;
        }

        public Registration GetLatestRegistration(User usr)
        {            
            return new RegistrationRepo(Entities).GetLatestRegistration(usr);
        }


        // Karthi starts
        public string userName(string NRIC)
        {
            string userName = string.Empty;
            try
            {

                var userchk = (from usr in Entities.Users
                               where usr.NRIC == NRIC
                               select usr).FirstOrDefault();

                if (userchk != null)
                {
                    userName = userchk.Title + " " + userchk.FirstName.Trim() + " " + userchk.LastName.Trim();
                }


            }
            catch (Exception e)
            {
                System.Console.WriteLine("Source: " + e.Source + " and Message: " + e.Message);
            }
            return userName;
        }

       

        public string isValidUser(string NRIC, string pwd)
        {
            string usrrole = string.Empty;
            try
            {

                var userchk = (from usr in Entities.AccessInfoes
                               where usr.NRIC == NRIC && usr.Password == pwd
                               select usr).FirstOrDefault();

                if (userchk != null)
                {
                    usrrole = userchk.AccessRole;
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Source: " + e.Source + " and Message: " + e.Message);
            }
            return usrrole;
        }


        public DataTable BorrowedItems(string nric)
        {
            DataTable dtab = new DataTable();

            try
            {
                var AccountSummaryList = from h in Entities.LoanRecords
                                         join ch in Entities.Books on h.ISBN equals ch.ISBN
                                         where h.NRIC == nric
                                         select new
                                         {
                                             h.ISBN,
                                             ch.Title,
                                             h.StartDate,
                                             h.ReturnedDate,
                                             h.Duration
                                         };

                int Sno = 1;


                DataColumn No = new DataColumn("No", Type.GetType("System.Int32"));
                DataColumn ISBN = new DataColumn("ISBN", Type.GetType("System.String"));
                DataColumn Title = new DataColumn("TItle", Type.GetType("System.String"));
                DataColumn Duration = new DataColumn("Duration", Type.GetType("System.String"));
                DataColumn StartDate = new DataColumn("StartDate", Type.GetType("System.String"));
                DataColumn ReturnDate = new DataColumn("ReturnDate", Type.GetType("System.String"));
                DataColumn Status = new DataColumn("Status", Type.GetType("System.String"));
                dtab.Columns.Add(No);
                dtab.Columns.Add(ISBN);
                dtab.Columns.Add(Title);
                dtab.Columns.Add(Duration);
                dtab.Columns.Add(StartDate);
                dtab.Columns.Add(ReturnDate);
                dtab.Columns.Add(Status);


                foreach (var evt in AccountSummaryList)
                {
                    DataRow itemRow = dtab.NewRow();
                    itemRow["No"] = Sno++;
                    itemRow["ISBN"] = evt.ISBN;
                    itemRow["Title"] = evt.Title;
                    itemRow["Duration"] = evt.Duration;
                    itemRow["StartDate"] = String.Format("{0:M/d/yyyy}", evt.StartDate);
                    itemRow["ReturnDate"] = String.Format("{0:M/d/yyyy}", evt.ReturnedDate);

                    DateTime startdate = Convert.ToDateTime(evt.StartDate);

                    DateTime retndate = Convert.ToDateTime(evt.ReturnedDate);
                    System.TimeSpan diffResult = retndate - startdate;
                    int period = evt.Duration;
                    int datediff = diffResult.Days;
                    double fine = 0.0;
                    if ((period >= datediff) && (datediff > 0))   // (retndate.ToString().Length>0))
                    {
                        itemRow["Status"] = "Charged";
                    }
                    else if ((period <= datediff) && (retndate.ToString().Length > 0))
                    {
                        fine = 0.50 * (datediff - period);
                        itemRow["Status"] = String.Format("Fine: {0:C}", fine);
                        // "$"+fine.ToString();
                    }
                    else if (datediff < 0)
                    {
                        itemRow["Status"] = "OverDue";
                    }

                    dtab.Rows.Add(itemRow);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Borrowed Item Summary retrieval Issue Biz.Facade)" +
                ex.Message);
            }
            return dtab;
        }



        public DataTable ReservedItems(string nric)
        {
            DataTable dtab = new DataTable();
            try
            {
                var AccountSummaryList = from h in Entities.ReservedBooks
                                         join ch in Entities.Books on h.ISBN equals ch.ISBN
                                         where h.NRIC == nric
                                         select new
                                         {
                                             h.ReservedID,
                                             h.ISBN,
                                             ch.Title,
                                             h.ReservedDateTime,
                                             h.ReserveStatus
                                         };

                int Sno = 1;
                DataColumn No = new DataColumn("No", Type.GetType("System.Int32"));
                DataColumn ISBN = new DataColumn("ISBN", Type.GetType("System.String"));
                DataColumn Title = new DataColumn("TItle", Type.GetType("System.String"));
                DataColumn ReservedID = new DataColumn("ReservedID", Type.GetType("System.String"));
                DataColumn ReservedDate = new DataColumn("ReservedDate", Type.GetType("System.String"));
                DataColumn Status = new DataColumn("Status", Type.GetType("System.String"));
                dtab.Columns.Add(No);
                dtab.Columns.Add(ISBN);
                dtab.Columns.Add(Title);
                dtab.Columns.Add(ReservedID);
                dtab.Columns.Add(ReservedDate);
                dtab.Columns.Add(Status);


                foreach (var evt in AccountSummaryList)
                {
                    DataRow itemRow = dtab.NewRow();
                    itemRow["No"] = Sno++;
                    itemRow["ISBN"] = evt.ISBN;
                    itemRow["Title"] = evt.Title;
                    itemRow["ReservedID"] = evt.ReservedID;
                    itemRow["ReservedDate"] = String.Format("{0:M/d/yyyy}", evt.ReservedDateTime);
                    itemRow["Status"] = evt.ReserveStatus;
                    dtab.Rows.Add(itemRow);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Reserved Item Summary retrieval Issue Biz.Facade)" +
                ex.Message);
            }
            return dtab;
        }

        public DataTable RenewalItems(string nric)
        {
            DataTable dtab = new DataTable();
            try
            {
                var AccountSummaryList = from h in Entities.LoanRecords
                                         join ch in Entities.Books on h.ISBN equals ch.ISBN
                                         where h.NRIC == nric && !(h.ReturnedDate.HasValue)
                                         select new
                                         {

                                             h.ISBN,
                                             ch.Title,
                                             h.StartDate,
                                             h.ReturnedDate,
                                             h.Duration,
                                             h.isLoanRenewable
                                         };

                int Sno = 1;
                DataColumn No = new DataColumn("No", Type.GetType("System.Int32"));
                DataColumn ISBN = new DataColumn("ISBN", Type.GetType("System.String"));
                DataColumn Title = new DataColumn("TItle", Type.GetType("System.String"));
                DataColumn BorrowedDate = new DataColumn("BorrowedDate", Type.GetType("System.String"));
                DataColumn DueDate = new DataColumn("DueDate", Type.GetType("System.String"));
                DataColumn Status = new DataColumn("Status", Type.GetType("System.String"));
                dtab.Columns.Add(No);
                dtab.Columns.Add(ISBN);
                dtab.Columns.Add(Title);
                dtab.Columns.Add(BorrowedDate);
                dtab.Columns.Add(DueDate);
                dtab.Columns.Add(Status);


                foreach (var evt in AccountSummaryList)
                {
                    DataRow itemRow = dtab.NewRow();
                    itemRow["No"] = Sno++;
                    itemRow["ISBN"] = evt.ISBN;
                    itemRow["Title"] = evt.Title;
                    itemRow["BorrowedDate"] = String.Format("{0:M/d/yyyy}", evt.StartDate);
                    DateTime duedate = Convert.ToDateTime(evt.StartDate).AddDays(evt.Duration);
                    itemRow["DueDate"] = String.Format("{0:M/d/yyyy}", duedate);
                    itemRow["Status"] = evt.isLoanRenewable;
                    dtab.Rows.Add(itemRow);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Reserved Item Summary retrieval Issue Biz.Facade)" +
                ex.Message);
            }
            return dtab;
        }

        public bool RenewItems(ArrayList isbnarray, string nric)
        {

            string isbnid;
            DateTime rtndate;
            LoanRecord r = new LoanRecord();

            try
            {

                foreach (string isbn in isbnarray)
                {
                    isbnid = isbn.ToString();
                    var renewitems = (from h in Entities.LoanRecords
                                      where h.NRIC == nric && h.ISBN == isbnid
                                      select h).First();

                    rtndate = Convert.ToDateTime(renewitems.StartDate);
                    rtndate = rtndate.AddDays(renewitems.Duration);

                    renewitems.ReturnedDate = rtndate;

                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Source: " + e.Source + " and Message: " + e.Message);
                return false;
            }
        }


        // Karthi ends





        // Karthi ends
                

    }
}
