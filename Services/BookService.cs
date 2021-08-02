using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoDIO.Entities;
using ApiCatalogoDIO.Exceptions;
using ApiCatalogoDIO.InputModel;
using ApiCatalogoDIO.Repositories;
using ApiCatalogoDIO.ViewModel;

namespace ApiCatalogoDIO.Services {
    public class BookService : IBookService {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository) {
            _bookRepository = bookRepository;
        }

        public async Task<BookViewModel> AddBook(BookInputModel book)
        {
            var entityBook =await _bookRepository.GetOneBookNameAuthor(book.Title, book.Author);

            if (entityBook.Count>0)
                throw new BookAlreadyExistsException();
            

            var bookNew = new Book {
                Id = Guid.NewGuid(),
                Title = book.Title,
                Author = book.Author,
                Pages = book.Pages
            };
            await _bookRepository.AddBook(bookNew);

            return new BookViewModel {
                Id = bookNew.Id,
                Title = book.Title,
                Author = book.Author,
                Pages = book.Pages
            };
        }

        public async Task DeleteBook(Guid id)
        {
            var entityBook = await _bookRepository.GetOneBook(id);

            if(entityBook==null)
                throw new BookDoesNotExistException();
            
            await _bookRepository.DeleteBook(id);
        }

        public async Task<List<BookViewModel>> GetBooks(int page, int quantity)
        {
            var books = await _bookRepository.GetBooks(page,quantity);
            return books.Select(book=>new BookViewModel {
                                    Id = book.Id,
                                    Title = book.Title,
                                    Author = book.Author,
                                    Pages = book.Pages
                                }).ToList();
        }

        public async Task<BookViewModel> GetOneBook(Guid id)
        {
            var book = await _bookRepository.GetOneBook(id);
            if (book==null)
                return null;
            
            return new BookViewModel {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Pages = book.Pages
            };

        }

        public async Task UpdateBook(Guid id, BookInputModel book)
        {
            var entityBook = await _bookRepository.GetOneBook(id);

            if(entityBook==null)
                throw new BookDoesNotExistException();
            
            entityBook.Title = book.Title;
            entityBook.Author = book.Author;
            entityBook.Pages = book.Pages;

            await _bookRepository.UpdateBook(entityBook);
        }

        public async Task UpdateFieldBook(Guid id, int pages)
        {
            var entityBook = await _bookRepository.GetOneBook(id);

            if(entityBook==null)
                throw new BookDoesNotExistException();
            
            entityBook.Pages = pages;

            await _bookRepository.UpdateBook(entityBook);
        }
    }

}