using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class LibraryMethods
    {
        private LibraryDb db = new LibraryDb();

        public Task<IEnumerable<Books>> GetAllBooks()
        {
            return Task.Run(() =>
            {
                return (IEnumerable<Books>)db.Books.ToList();
            });
        }
        public Task<IEnumerable<Authors>> GetAllAuthors()
        {
            return Task.Run(() =>
            {
                return (IEnumerable<Authors>)db.Authors.ToList();
            });
        }

        public async Task<IEnumerable<Books>> GetBooksByAuthors(string AuthorName)
        {
            var res = db.Books.Where(a => a.Authors.Name == AuthorName);
            return await res.ToListAsync();
        }

        public async Task<IEnumerable<Books>> GetBooksByName(string BName)
        {
            var res = db.Books.Where(a => a.Name.StartsWith(BName));
            return await res.ToListAsync();
        }
        public async Task<IEnumerable<Books>> SearchBook(int price)
        {
            var res = db.Books.Where(b => b.Price == price).OrderBy(o => o.Price);
            return await res.ToListAsync();  
        }
    }
}
