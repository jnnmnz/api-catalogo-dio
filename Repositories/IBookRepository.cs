using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoDIO.Entities;

namespace ApiCatalogoDIO.Repositories {
    public interface IBookRepository {
        Task<List<Book>> GetBooks(int page, int quantity);
        Task<Book> GetOneBook(Guid id);
        Task<List<Book>> GetOneBookNameAuthor (string title, string author);
        Task<Book> AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(Guid id);
    }
}