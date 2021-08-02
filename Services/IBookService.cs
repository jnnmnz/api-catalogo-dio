using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoDIO.InputModel;
using ApiCatalogoDIO.ViewModel;

namespace ApiCatalogoDIO.Services {
    public interface IBookService{
        Task<List<BookViewModel>> GetBooks(int page, int quantity);
        Task<BookViewModel> GetOneBook(Guid id);
        Task<BookViewModel> AddBook(BookInputModel book);
        Task UpdateBook(Guid id, BookInputModel book);
        Task UpdateFieldBook(Guid id, int pages);
        Task DeleteBook(Guid id);
    }
}