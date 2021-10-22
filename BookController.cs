using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace GauronskisD_VismaLibrary
{
    public class BookController
    {
        
        private List<Book> allBooks = new List<Book>();
        private string Fd = string.Empty;
        public BookController(string file)
        {
            this.Fd = file;
        }
        public void ReadJson()
        {
            using (StreamReader r = new StreamReader(Fd))
            {
                string json = r.ReadToEnd();
                allBooks = JsonConvert.DeserializeObject<List<Book>>(json);
            }
        }
        private void UpdateJson()
        {
            File.WriteAllText(Fd, "[\n");
            foreach (var book in allBooks)
            {
                if (!(allBooks.Last() == book))
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(book) + ",";
                    File.AppendAllText(Fd, json);
                    File.AppendAllText(Fd, "\n");
                }
                else
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(book);
                    File.AppendAllText(Fd, json);
                    File.AppendAllText(Fd, "\n");
                }
            }
            File.AppendAllText(Fd, "]");
        }
        public void AddBook(Book book)
        {
            if (book != null && !allBooks.Any(i => i.ISBN == book.ISBN))
            {
                allBooks.Add(book);
                UpdateJson();
            }
            else
            {
                Console.WriteLine("Can not add book as it is null or ISBN already exists");
            }
        }
        public Book GetBook(string name)
        {
            return allBooks.Find(i => i.Name == name);
        }
        public IEnumerable<Book> GetAll()
        {
            Console.WriteLine("Do you want to filter the data? y/n");
            string x = Console.ReadLine();
            IEnumerable<Book> filteredData;
            if (x == "y" || x == "Y" || x == "Yes" || x == "yes")
            {
                Console.WriteLine("Select from the following filters:\n" +
                    "To filter by author type: 'aut'\n" +
                    "To filter by category type: 'c'\n" +
                    "To filter by language: 'l'\n" +
                    "To filter by ISBN: 'isbn'\n" +
                    "To filter by name type: 'n'\n" +
                    "To filter by taken books type: 't'\n" +
                    "To filter by available books type: 'a'\n");
                string filter = Console.ReadLine();
                filteredData = selectByFilter(filter);
            }
            else filteredData = allBooks;
            PrintToConsole(filteredData);
            return filteredData;
        }
        public bool DeleteBook(Book book)
        {
            if(book != null) 
            { 
                allBooks.Remove(book);
                UpdateJson();
                return true;
            }
            else Console.WriteLine("Book does not exist");
            return false;
        }
        public void DeleteByISBN(string isbn)
        {
            if (allBooks.Any(i => i.ISBN == isbn))
            {
                Book toDelete = allBooks.FirstOrDefault(x => x.ISBN == isbn);
                allBooks.Remove(toDelete);
                UpdateJson();
            }
            else Console.WriteLine("Book with ISBN '{0}' does not exist", isbn);
        }
        public void TakeBook()
        {
            string takenBy = string.Empty;
            int periodTaken = int.MinValue;
            string isbn = string.Empty;

            Console.WriteLine("Input who is taking the Book: ");
            takenBy = Console.ReadLine();
            Console.WriteLine("Input how long is the book taken for? Enter only the number of days: ");
            if (!int.TryParse(Console.ReadLine(), out periodTaken)) 
            {
                Console.WriteLine("Wrong input, enter only numbers");
                return;
            }
            if(periodTaken > 60) 
            {
                Console.WriteLine("Can not take book for longer than 2 months");
                return;
            }
            Console.WriteLine("Input the ISBN of the book that is being taken: ");
            isbn = Console.ReadLine();

            if (allBooks.Any(i => i.ISBN == isbn))
            {
                if (allBooks.Where(i => i.TakenBy == takenBy).Count() <= 3)
                {
                    Book book = allBooks.FirstOrDefault(x => x.ISBN == isbn);
                    book.IsTaken = true;
                    book.TakenBy = takenBy;
                    book.DateTaken = DateTime.Now;
                    book.EstimatedReturn = book.DateTaken.AddDays(periodTaken);
                    UpdateJson();
                }
                else Console.WriteLine("{0} has already taken 3 books", takenBy);
            }
            else Console.WriteLine("Book with ISBN '{0}' does not exist", isbn);
        }
        public void ReturnBook()
        {
            string isbn = string.Empty;

            Console.WriteLine("Input the ISBN of the book that is being returned: ");
            isbn = Console.ReadLine();

            if (allBooks.Any(i => i.ISBN == isbn))
            {
                Book book = allBooks.FirstOrDefault(x => x.ISBN == isbn);
                if (!(book.EstimatedReturn >= DateTime.Now))
                    Console.WriteLine("You're late... Again.");
                book.IsTaken = false;
                book.TakenBy = null;
                book.DateTaken = DateTime.MinValue;
                book.EstimatedReturn = DateTime.MinValue;
                UpdateJson();
            }
            else Console.WriteLine("Book with ISBN '{0}' does not exist", isbn);
        }
        private IEnumerable<Book> selectByFilter(string filter)
        {
            IEnumerable<Book> filteredData;
            string selected = string.Empty;
            filter = filter.ToLower();
            switch (filter)
            {
                case "aut":
                    Console.WriteLine("Enter the wanted authors name:");
                    selected = Console.ReadLine();
                    filteredData =
                        from book in allBooks
                        where book.Author == selected
                        select book;
                    break;
                case "c":
                    Console.WriteLine("Enter the wanted category type:");
                    selected = Console.ReadLine();
                    filteredData =
                        from book in allBooks
                        where book.Category.Equals(selected)
                        select book;
                    break;
                case "l":
                    Console.WriteLine("Enter the wanted language:");
                    selected = Console.ReadLine();
                    filteredData =
                        from book in allBooks
                        where book.Language.Equals(selected)
                        select book;
                    break;
                case "isbn":
                    Console.WriteLine("Enter the wanted ISBN type:");
                    selected = Console.ReadLine();
                    filteredData =
                        from book in allBooks
                        where book.ISBN.Equals(selected)
                        select book;
                    break;
                case "n":
                    Console.WriteLine("Enter the name of the book:");
                    selected = Console.ReadLine();
                    filteredData =
                        from book in allBooks
                        where book.Name.Equals(selected)
                        select book;
                    break;
                case "t":
                    filteredData =
                        from book in allBooks
                        where book.IsTaken == true
                        select book;
                    break;
                case "a":
                    filteredData =
                        from book in allBooks
                        where book.IsTaken == false
                        select book;
                    break;
                default:
                    Console.WriteLine("Displaying books without a filter:");
                    filteredData = allBooks;
                    break;
            }
            return filteredData;
        }
        private void PrintToConsole(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }
    }
}
