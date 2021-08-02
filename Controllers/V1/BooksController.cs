using ApiCatalogoDIO.InputModel;
using ApiCatalogoDIO.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;   
using ApiCatalogoDIO.Services;
using System.ComponentModel.DataAnnotations;
using ApiCatalogoDIO.Exceptions;


namespace ApiCatalogoDIO.Controllers.V1 {
    
    [Route("api/v1/[controller]")]
    [ApiController]

    public class BooksConstroller : ControllerBase {

        private readonly IBookService _bookService;

        public BooksConstroller(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooks([FromQuery,Range(1,int.MaxValue)] int page=1, [FromQuery,Range(1, 50)] int quantity=5) {
            var books = await _bookService.GetBooks(page,quantity);
            if (books.Count()==0)
                return NoContent();

            return Ok(books);
        }

        [HttpGet("{idBook:guid}")]
        public async Task<ActionResult<List<BookViewModel>>> GetOneBook([FromRoute] Guid idBook){
            var book = await _bookService.GetOneBook(idBook);
            if(book==null)
                return NoContent();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookViewModel>> AddBook([FromBody] BookInputModel bookInputModel){
            try {
                var book = await _bookService.AddBook(bookInputModel);
                return Ok(book);
            }
            catch (BookAlreadyExistsException) {
                return UnprocessableEntity("JÁ HÁ UM CADASTRO PARA ESSE TÍTULO DESSE AUTOR");
            }
        }

        [HttpPut("{idBook:guid}")]
        public async Task<ActionResult> UpdateBook([FromRoute] Guid idBook, [FromBody] BookInputModel bookInputModel){
            try {
                await _bookService.UpdateBook(idBook, bookInputModel);
                return Ok();
            }
            catch (BookDoesNotExistException) {
                return NotFound("LIVRO NÃO ECONTRADO");
            }
        }

        [HttpPatch("{idBook:guid}/title/{title}")]
        public async Task<ActionResult> UpdateFieldBook([FromRoute] Guid idBook, [FromRoute] int pages){
            try {
                await _bookService.UpdateFieldBook(idBook, pages);
                return Ok();
            }
            catch (BookDoesNotExistException) {
                return NotFound("LIVRO NÃO ENCONTRADO");
            }
        }

        [HttpDelete("{idBook:guid}")]
        public async Task<ActionResult> DeleteBook([FromRoute] Guid idBook){
            try {
                await _bookService.DeleteBook(idBook);
                return Ok();
            }
            catch (BookDoesNotExistException) {
                return NotFound("LIVRO NÃO ENCONTRADO");
            }
        }
    }
}