using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDIO.Exceptions {
    public class BookAlreadyExistsException : Exception {
        public BookAlreadyExistsException() : base("ESTE LIVRO JÁ ESTÁ CADASTRADO!") {}
    }
}