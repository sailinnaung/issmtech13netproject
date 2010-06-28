using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibrarySystemBusinessLogicLayer.MVCRepository;
using LibrarySystemBusinessLogicLayer.Utility;

namespace LibrarySystemBusinessLogicLayer.BusinessFacade
{
    public class SearchBookResult
    {
        private string iSBN;
        private string title;
        private string status;
        private DateTime dateOfPublication;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        
        public string Status
        {
            get { return status; }
            set { status = value; }
        }        

        public DateTime DateOfPublication
        {
            get { return dateOfPublication; }
            set { dateOfPublication = value; }
        }

        public string ISBN
        {
            get { return iSBN; }
            set { iSBN = value; }
        }
    }

    public class SearchFacade
    {
        LibrarySystemDBEntities Entities;

        public SearchFacade()
        {
            Entities = new LibrarySystemDBEntities();
        }

        public List<SearchBookResult> GetBookByTitle(string title)
        {
            BookRepo BRepo = new BookRepo();
            List<SearchBookResult> SBResult = new List<SearchBookResult>();
            IEnumerable<Book> FoundBooksList = BRepo.FindBookTitle(title);

            foreach (Book b in FoundBooksList)
            {
                SearchBookResult SBR = new SearchBookResult();
                SBR.ISBN = b.ISBN;
                SBR.Title = b.Title;
                if (b.DateOfPublication != null)
                {
                    SBR.DateOfPublication = DateTime.Parse(b.DateOfPublication.ToString());
                }                
                SBR.Status = b.Status;

                SBResult.Add(SBR);
            }

            return SBResult;
        }

        public List<SearchBookResult> GetBookByISBN(string ISBN)
        {
            BookRepo BRepo = new BookRepo();
            List<SearchBookResult> SBResult = new List<SearchBookResult>();
            IEnumerable<Book> FoundBooksList = BRepo.FindBookISBN(ISBN);

            foreach (Book b in FoundBooksList)
            {
                SearchBookResult SBR = new SearchBookResult();
                SBR.ISBN = b.ISBN;
                SBR.Title = b.Title;
                SBR.DateOfPublication = DateTime.Parse(b.DateOfPublication.ToString());
                SBR.Status = b.Status;

                SBResult.Add(SBR);
            }

            return SBResult;
        }

        public List<SearchBookResult> GetBookByAuthor(string Author)
        {
            BookRepo BRepo = new BookRepo();
            List<SearchBookResult> SBResult = new List<SearchBookResult>();
            IEnumerable<Book> FoundBooksList = BRepo.FindBookAuthor(Author.Trim());

            foreach (Book b in FoundBooksList)
            {
                SearchBookResult SBR = new SearchBookResult();
                SBR.ISBN = b.ISBN;
                SBR.Title = b.Title;
                SBR.DateOfPublication = DateTime.Parse(b.DateOfPublication.ToString());
                SBR.Status = b.Status;

                SBResult.Add(SBR);
            }

            return SBResult;
        }

        public int GetUserReserveCount(string NRIC)
        {
            int reserveCount = 0;
            UserRepo usrRepo = new UserRepo();
            User usr = usrRepo.GetUserByNRIC(NRIC);
            MembershipHistoryRepo mshRepo = new MembershipHistoryRepo();
            MembershipHistory msh = mshRepo.GetCurrentMembership(usr);

            BusinessRulesRepo br = new BusinessRulesRepo();

            if (msh.MemberShipLevel.Equals(EnumHelper<Membership>.GetEnumDesc(Membership.BASIC_MEMBER)))
            {
                reserveCount = br.Get(1).NumOfBookReservationPerPerson;
            }

            if (msh.MemberShipLevel.Equals(EnumHelper<Membership>.GetEnumDesc(Membership.BASIC_MEMBER)))
            {
                reserveCount = br.Get(2).NumOfBookReservationPerPerson;
            }

            if (msh.MemberShipLevel.Equals(EnumHelper<Membership>.GetEnumDesc(Membership.BASIC_MEMBER)))
            {
                reserveCount = br.Get(3).NumOfBookReservationPerPerson;
            }            
            

            return reserveCount;
        }
        
    }
}
