using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListService.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BookListService bookListService= new BookListService();
            Book[] books = new Book[5];
            books[0] = new Book("Albahari", "C# in a nutshell", 2012, 1043);
            books[1] = new Book("Richter", "CLR via C#", 2013, 896);
            books[2] = new Book("Eckel", "Thinking in Java", 2009, 637);
            books[3] = new Book("Lorem", "Ipsum", 2005, 1024);
            books[4] = new Book("Dolor", "Sit Amet", 2015, 512);
            foreach (Book book in books)
            {
                bookListService.AddBook(book);
            }
            var bfs=new BinaryFormatterStorage();
            bookListService.SaveCollectionToStorage(bfs);
            var anotherBookListService = new BookListService();
            anotherBookListService.ReadCollectionFromStorage(bfs);
            var xmlStorage=new XmlStorage();
            anotherBookListService.SaveCollectionToStorage(xmlStorage);
            anotherBookListService.SaveCollectionToStorage(new Linq2XmlStorage());
            anotherBookListService.ReadCollectionFromStorage(new Linq2XmlStorage());
            foreach (var book in anotherBookListService.ToList())
            {
                Console.WriteLine(book);
            }
            Console.ReadKey();
        }
    }
}
