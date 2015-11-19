using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BookListService.ConsoleUI
{
    public class XmlStorage : IBookDataStorage
    {
        public List<Book> ReadListFromStorage()
        {
            XmlSerializer serializer = new
                XmlSerializer(typeof(List<Book>));
            List<Book> resultList = null;
            using (var fs = new FileStream("books.xml", FileMode.Open))
            {
                XmlReader reader = XmlReader.Create(fs);
                resultList = (List<Book>)serializer.Deserialize(reader);
            }
            return resultList;
        }

        public void SaveToStorage(List<Book> books)
        {
            if (books == null)
                throw new ArgumentNullException(nameof(books));
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            using (var fs = new FileStream("books.xml", FileMode.Create))
            {
                using (var writer = new XmlTextWriter(fs, new UTF8Encoding()))
                {
                    serializer.Serialize(writer, books);
                }
            }
        }
    }
}
