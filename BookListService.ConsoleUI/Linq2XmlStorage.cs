using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookListService.ConsoleUI
{
    public class Linq2XmlStorage : IBookDataStorage
    {
        public void SaveToStorage(List<Book> books)
        {
            XElement elements = new XElement("Books",
                books.Select(x => new XElement("Book",
                    new XElement("Title", x.Title),
                    new XElement("Author", x.Author),
                    new XElement("PageCount", x.PageCount),
                    new XElement("Year", x.Year)
                    )));
            elements.Save("booksLinq.xml");

        }

        public List<Book> ReadListFromStorage()
        {
            List<Book> resultList = new List<Book>();
            XElement elements = XElement.Load("booksLinq.xml");
            var books = from book in elements.Descendants("Book")
                        select new
                        {
                            Title = book.Element("Title"),
                            Author = book.Element("Author"),
                            PageCount = book.Element("PageCount"),
                            Year = book.Element("Year")
                        };
            foreach (var book in books)
            {
                int year;
                if (!Int32.TryParse(book.Year.Value, out year))
                    throw new SerializationException("Can't deserialize list!");
                int pageCount;
                if (!Int32.TryParse(book.PageCount.Value, out pageCount))
                    throw new SerializationException("Can't deserialize list!");
                resultList.Add(new Book(book.Author.Value, book.Title.Value, year, pageCount));
            }
            return resultList;
        }
    }
}
