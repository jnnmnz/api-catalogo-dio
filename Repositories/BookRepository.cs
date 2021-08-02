using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoDIO.Entities;

namespace ApiCatalogoDIO.Repositories {
    public class BookRepository : IBookRepository {
        private static Dictionary<Guid, Book> books = new Dictionary<Guid, Book>() {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Book{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Title = "A Guerra dos Tronos", Author = "George R. R. Martin", Pages = 500} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Book{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Title = "A Fúria dos Reis", Author = "George R. R. Martin", Pages = 420} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Book{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Title = "O Senhor dos Anéis", Author = "J. R. R. Tolkien", Pages = 250} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Book{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Title = "O Retorno do Rei", Author = "J. R. R. Tolkien", Pages = 200} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Book{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Title = "O Leão, A Feiticeira e o Guarda-Roupa", Author = "C. S. Lewis", Pages = 97} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Book{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Title = "The Silent Planet", Author = "C. S. Lewis", Pages = 230} }
        };

        public Task<List<Book>> GetBooks(int page, int quantity) {
            return Task.FromResult(books.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Book> GetOneBook(Guid id) {
            if (!books.ContainsKey(id))
                return Task.FromResult<Book>(null);
                //return null;

            return Task.FromResult(books[id]);
        }

        public Task<List<Book>> GetOneBookNameAuthor(string title, string author) {
            return Task.FromResult(books.Values.Where(book => book.Title.Equals(title) && book.Author.Equals(author)).ToList());
        }

        public Task<List<Book>> GetBookWithNoLambda(string title, string author) {
            var bookReturned = new List<Book>();

            foreach(var book in books.Values)
            {
                if (book.Title.Equals(book) && book.Author.Equals(author))
                    bookReturned.Add(book);
            }

            return Task.FromResult(bookReturned);
        }

        public Task AddBook(Book book){
            books.Add(book.Id, book);
            return Task.CompletedTask;
        }

        public Task UpdateBook(Book book) {
            books[book.Id] = book;
            return Task.CompletedTask;
        }

        public Task DeleteBook(Guid id) {
            books.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose() {
            //Fechar conexão com o banco
        }

        Task<Book> IBookRepository.AddBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}