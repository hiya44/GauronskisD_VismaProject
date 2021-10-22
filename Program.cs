using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GauronskisD_VismaLibrary
{
    public class Program
    {
        static void Main(string[] args)
        {
            string fd = @"library.json";
            BookController bookController = new BookController(fd);
            bookController.ReadJson();
            Book book1 = new Book("name1", "author1", "category1", "language1", DateTime.Now, "123-123-123");
            Book book2 = new Book("name2", "author2", "category2", "language2", DateTime.Now, "223-123-123");
            Book book3 = new Book("name3", "author3", "category3", "language3", DateTime.Now, "323-123-123");
            Book book4 = new Book("name3", "author3", "category3", "language3", DateTime.Now, "423-123-123");

            bookController.AddBook(book4);
            //bookController.AddBook(book2);
            //bookController.AddBook(book3);

            //bookController.TakeBook();
            //bookController.ReturnBook();
            bookController.GetAll();
        }
    }
}
