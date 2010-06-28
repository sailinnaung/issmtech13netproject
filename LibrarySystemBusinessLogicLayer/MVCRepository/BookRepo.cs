using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibrarySystemBusinessLogicLayer;



namespace LibrarySystemBusinessLogicLayer.MVCRepository
{
    public class BookRepo
    {
        LibrarySystemDBEntities Entities = new LibrarySystemDBEntities();
        public IEnumerable<Book> List()
        {
            return Entities.Books.ToList();
        }

        public Book GetBook(int id)
        {
            return (from p in Entities.Books
                    where p.SerialNo == id
                    select p).FirstOrDefault();
        }

        public IEnumerable<string> GetAuthorList()
        {
            return from p in Entities.Books
                   select p.AuthorName;
        }

        public IEnumerable<Book> FindBookISBN(string ISBN)
        {
            return from p in Entities.Books
                   where p.ISBN == ISBN
                   select p;
        }
        public IEnumerable<Book> FindBookTitle(string title)
        {
            var data = Entities.Books.Where(x => x.Title.ToUpper().Contains(title.ToUpper())).ToList();
            return data;
        }

        public IEnumerable<Book> FindBookAuthor(string author)
        {
            var data = Entities.Books.Where(x => x.AuthorName.ToUpper().Contains(author.ToUpper())).ToList();

            return data;
        }
        public Book FindBookByTitle(string title)
        {
            return (from p in Entities.Books
                    where p.Title == title
                    select p).FirstOrDefault();
        }

        public bool CheckISBNExist(string isbn)
        {
            var books = FindBookISBN(isbn);
            return (books.Count() != 0);
        }

        public void CreateBook(Book bookToCreate)
        {
            Entities.AddToBooks(bookToCreate);
        }

        public void DeleteBook(Book bookToDelete)
        {
            var originalBook = GetBook(bookToDelete.SerialNo);
            if (originalBook != null)
            {
                Entities.DeleteObject(originalBook);
                Entities.SaveChanges();
            }
        }

        //For PublisherList
        public IEnumerable<int> GetPublisherIDs()
        {
            return from p in Entities.Publishers
                   select p.PublisherID;
        }
        public IEnumerable<string> GetPublisherList()
        {
            return from p in Entities.Publishers
                   select p.Name;
        }

        public void CreatePublisher(Publisher p)
        {
            Entities.AddToPublishers(p);
        }

        public void DeletePublisher(Book bookToDelete)
        {
            var originalBook = GetPublisher(bookToDelete.SerialNo);
            if (originalBook != null)
            {
                Entities.DeleteObject(originalBook);
                Entities.SaveChanges();
            }
        }

        public Publisher GetPublisher(int id)
        {
            return (from p in Entities.Publishers
                    where p.PublisherID == id
                    select p).FirstOrDefault();
        }

        public Publisher GetPublisherString(string name)
        {
            return (from p in Entities.Publishers
                    where p.Name == name
                    select p).FirstOrDefault();
        }


        public Publisher FindPublisherByName(string name)
        {
            if (name == null) return null;
            return (from p in Entities.Publishers
                    where p.Name == name
                    select p).FirstOrDefault();
        }

        //For Categories
        public IEnumerable<int> GetCategoryIDs()
        {
            return from category in Entities.BookCategories
                   select category.CategoryID;
        }

        public IEnumerable<string> GetCategoryTitleList()
        {
            return from p in Entities.BookCategories
                   select p.CatergoyTitle;
        }

        public BookCategory GetCategoryIndex(int id)
        {
            return (from c in Entities.BookCategories
                    where c.CategoryID == id
                    select c).FirstOrDefault();
        }

        public BookCategory GetCategoryByName(string name)
        {
            return (from category in Entities.BookCategories
                    where category.CatergoyTitle == name
                    select category).FirstOrDefault();
        }

        //For Categories
        public IEnumerable<BookCategory> GetCategoryList()
        {
            return Entities.BookCategories.ToList();
        }

        public void CreateCategory(BookCategory c)
        {
            Entities.AddToBookCategories(c);
        }

        public void DeleteCategory(BookCategory bg)
        {
            var original = GetCategoryIndex(bg.CategoryID);
            if (original != null)
            {
                Entities.DeleteObject(original);
                Entities.SaveChanges();
            }
        }

        //For Catelog
        public BookCatelog GetCatelogItem(int id)
        {
            return (from p in Entities.BookCatelogs
                    where p.CatelogID == id
                    select p).FirstOrDefault();
        }
        //For Categories
        public IEnumerable<int> CatelogIDs()
        {
            return from bc in Entities.BookCatelogs
                   select bc.CatelogID;
        }

        public IEnumerable<BookCatelog> GetCatelogList()
        {
            return from bc in Entities.BookCatelogs
                   where bc.Books.FirstOrDefault().ISBN != null
                   select bc;
        }


        public void CreateCatelogItem(BookCatelog catelogToCreate)
        {
            Entities.AddToBookCatelogs(catelogToCreate);
        }

        public void DeleteCatelog(BookCatelog catelogToDelete)
        {
            var originalCatelog = GetCatelogItem(catelogToDelete.CategoryID);
            if (originalCatelog != null)
            {
                Entities.DeleteObject(originalCatelog);
                Entities.SaveChanges();
            }
        }

        public void Save()
        {
            Entities.SaveChanges();
        }
    }
}
