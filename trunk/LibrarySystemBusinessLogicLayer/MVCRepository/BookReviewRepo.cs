using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class BookReviewRepo
    {
        private LibrarySystemDBEntities Entity = new LibrarySystemDBEntities();
        public IEnumerable<ReviewBook> GetList()
        {
            return Entity.ReviewBooks.ToList();
        }

        public IEnumerable<string> GetBookISNB()
        {
            return from catelog in Entity.BookCatelogs
                   select catelog.Books.FirstOrDefault().ISBN;
        }

        public IEnumerable<ReviewBook> FindListFromDate(DateTime dt)
        {
            return from review in Entity.ReviewBooks
                   where review.DateOfReview > dt.Date
                   orderby review.DateOfReview
                   select review;
        }

        public ReviewBook GetReview(int id)
        {
            return (from review in Entity.ReviewBooks
                    where review.ReviewCommentID == id
                    select review).FirstOrDefault();
        }
        public int FindBookSerialNo(string ISBN)
        {
            return (from book in Entity.Books
                    where book.ISBN == ISBN
                    select book).FirstOrDefault().SerialNo;
        }
        public void Create(ReviewBook rb)
        {
            Entity.AddToReviewBooks(rb);
        }

        public void Save()
        {
            Entity.SaveChanges();
        }

        public void Delete(ReviewBook p)
        {
            var review = GetReview(p.ReviewCommentID);
            if (review != null)
            {
                Entity.DeleteObject(review);
                Save();
            }
        }
    }
}
