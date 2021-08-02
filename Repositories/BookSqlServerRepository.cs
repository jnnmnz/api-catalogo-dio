// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Configuration;
// using ApiCatalogoDIO.Entities;
// using System.Data;

// namespace ApiCatalogoDIO.Repositories {
//     public class BookSqlServerRepository {
//         private readonly SqlConnection sqlConnection;

//         public BookSqlServerRepository(IConfiguration configuration)
//         {
//             sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
//         }

//         public async Task<List<Book>> GetBooks(int page, int quantity)
//         {
//             var books = new List<Book>();

//             var command = $"select * from BOOKS order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

//             await sqlConnection.OpenAsync();
//             SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
//             SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

//             while (sqlDataReader.Read())
//             {
//                 books.Add(new Book
//                 {
//                     Id = (Guid)sqlDataReader["Id"],
//                     Title = (string)sqlDataReader["Title"],
//                     Author = (string)sqlDataReader["Author"],
//                     Pages = (int)sqlDataReader["Pages"]
//                 });
//             }

//             await sqlConnection.CloseAsync();

//             return books;
//         }

//         public async Task<Book> GetOneBooks(Guid id)
//         {
//             Book book = null;

//             var command = $"select * from BOOKS where Id = '{id}'";

//             await sqlConnection.OpenAsync();
//             SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
//             SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

//             while (sqlDataReader.Read())
//             {
//                 book = new Book
//                 {
//                     Id = (Guid)sqlDataReader["Id"],
//                     Title = (string)sqlDataReader["Title"],
//                     Author = (string)sqlDataReader["Author"],
//                     Pages = (int)sqlDataReader["Pages"]
//                 };
//             }

//             await sqlConnection.CloseAsync();

//             return book;
//         }

//         public async Task<List<Book>> GetOneBookNameAuthor(string title, string author)
//         {
//             var books = new List<Book>();

//             var command = $"select * from BOOKS where Title = '{nome}' and Author = '{produtora}'";

//             await sqlConnection.OpenAsync();
//             SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
//             SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

//             while (sqlDataReader.Read())
//             {
//                 books.Add(new Book
//                 {
//                     Id = (Guid)sqlDataReader["Id"],
//                     Title = (string)sqlDataReader["Title"],
//                     Author = (string)sqlDataReader["Author"],
//                     Pages = (int)sqlDataReader["Pages"]
//                 });
//             }

//             await sqlConnection.CloseAsync();

//             return books;
//         }

//         public async Task AddBook(Book book)
//         {
//             var command = $"insert BOOKS (Id, Title, Author, Pages) values ('{book.Id}', '{book.Title}', '{book.Author}', {book.Pages.ToString()})";

//             await sqlConnection.OpenAsync();
//             SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
//             sqlCommand.ExecuteNonQuery();
//             await sqlConnection.CloseAsync();
//         }

//         public async Task UpdateBook(Book book)
//         {
//             var command = $"update BOOKS set Title = '{book.Title}', Author = '{book.Author}', Pages = {book.Pages.ToString()} where Id = '{book.Id}'";

//             await sqlConnection.OpenAsync();
//             SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
//             sqlCommand.ExecuteNonQuery();
//             await sqlConnection.CloseAsync();
//         }

//         public async Task DeleteBook(Guid id)
//         {
//             var command = $"delete from BOOKS where Id = '{id}'";

//             await sqlConnection.OpenAsync();
//             SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
//             sqlCommand.ExecuteNonQuery();
//             await sqlConnection.CloseAsync();
//         }

//         public void Dispose()
//         {
//             sqlConnection?.Close();
//             sqlConnection?.Dispose();
//         }
//     }
// }