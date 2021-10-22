using Microsoft.VisualStudio.TestTools.UnitTesting;
using GauronskisD_VismaLibrary;
using System;

namespace LibraryTests
{
    [TestClass]
    public class LibraryTest
    {
        [TestMethod]
        public void AddBookTest()
        {
            BookController bookController = new BookController(@"test.json");
            Book book1 = new Book("name1", "author1", "category1", "language1", DateTime.Now, "123-123-123");
            Book book2 = new Book("name2", "author2", "category2", "language2", DateTime.Now, "223-123-123");
            Book book3 = new Book("name3", "author3", "category3", "language3", DateTime.Now, "323-123-123");

            bookController.AddBook(book1);
            bookController.AddBook(book2);
            bookController.AddBook(book3);

            Assert.AreEqual(bookController.GetBook("name1"), book1);
            Assert.AreEqual(bookController.GetBook("name2"), book2);
            Assert.AreEqual(bookController.GetBook("name3"), book3);
        }
        [TestMethod]
        public void DeleteBookTest()
        {
            BookController bookController = new BookController(@"test.json");
            Book book1 = new Book("name1", "author1", "category1", "language1", DateTime.Now, "123-123-123");
            Book book2 = new Book("name2", "author2", "category2", "language2", DateTime.Now, "223-123-123");
            Book book3 = new Book("name3", "author3", "category3", "language3", DateTime.Now, "323-123-123");

            bookController.AddBook(book1);
            bookController.AddBook(book2);
            bookController.AddBook(book3);

            Assert.IsTrue(bookController.DeleteBook(book1));
            Assert.IsTrue(bookController.DeleteBook(book2));
            Assert.IsTrue(bookController.DeleteBook(book3));
        }
    }
}
