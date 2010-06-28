using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrarySystemBusinessLogicLayer.MVCRepository;

namespace LibrarySystemBusinessLogicLayer.BusinessFacade
{

    public class ReviewFacade
    {
        private LibrarySystemDBEntities libSysDB;
        const Int32 initialReviewID = 1000;

        public ReviewFacade()
        {
            libSysDB = new LibrarySystemDBEntities();
        }

        public IEnumerable<ReviewBook> GetReviews(string isbnNumber)
        {
            //IEnumerable<ReviewBook> data = libSysDB.ReviewBooks.Where(re => re.ISBN == isbnNumber).OrderBy(re =>re.DateOfReview).ToList();
            IEnumerable<ReviewBook> data = (from re in libSysDB.ReviewBooks
                                            where re.ISBN == isbnNumber
                                            orderby re.DateOfReview descending
                                            select re).ToList();
            //IEnumerable<ReviewBook> data = libSysDB.ReviewBooks.Where(re => re.ISBN == isbnNumber).ToList();
            return data;
        }

        public Book GetBookDetailsByIsbn(string isbnNumber)
        {
            Book book = new Book();
            IEnumerable<Book> data = (from bk in libSysDB.Books
                                      where bk.ISBN == isbnNumber
                                      select bk).ToList();
            try
            {
                book = data.First();
                return book;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                return book;
            }

           

        }

        
        public int GetNextReviewCommentID()
        {
            int ReviewID = initialReviewID;

            try
            {
                var data = (from re in libSysDB.ReviewBooks
                            select re).OrderByDescending(x => x.ReviewCommentID);

                if (data != null)
                {
                    ReviewID = data.First().ReviewCommentID + 1;
                }

                return ReviewID;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return initialReviewID;
            }
        }

        public bool CreateReviews(ReviewBook re)
        {
            try
            {
                libSysDB.AddToReviewBooks(re);
                libSysDB.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
