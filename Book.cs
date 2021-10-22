using System;
using System.Collections.Generic;
using System.Text;

namespace GauronskisD_VismaLibrary
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
        public bool IsTaken { get; set; }
        public string TakenBy { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime EstimatedReturn { get; set; }

        public Book(string name, string author, string category, string language, DateTime publicationDate, string iSBN)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            PublicationDate = publicationDate;
            ISBN = iSBN;
            IsTaken = false;
            TakenBy = null;
            DateTaken = DateTime.MinValue;
            EstimatedReturn = DateTime.MinValue;
        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", 
                Name, Author, Category, Language, PublicationDate, ISBN, IsTaken, TakenBy, DateTaken, EstimatedReturn);
        }
    }
}
