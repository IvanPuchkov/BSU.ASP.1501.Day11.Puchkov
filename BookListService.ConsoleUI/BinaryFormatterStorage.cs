using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BookListService.ConsoleUI
{
    public class BinaryFormatterStorage : IBookDataStorage
    {
        public List<Book> ReadListFromStorage()
        {
            List<Book> books = null;
            using (FileStream fs = new FileStream("DataFile.dat", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                books = (List<Book>)formatter.Deserialize(fs);
            }
            return books;
        }

        public void SaveToStorage(List<Book> books)
        {
            if (books == null)
                throw new ArgumentNullException(nameof(books));
            using (FileStream fs = new FileStream("DataFile.dat", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, books);
            }
        }
    }
}
