using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDIO.Exceptions {
    public class BookDoesNotExistException : Exception {
        public BookDoesNotExistException() : base("ESTE LIVRO JÁ ESTÁ CADASTRADO!") {}
    }
}