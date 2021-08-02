using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDIO.Entities {
    public class Book {
        public Guid Id {get;set;}
        public string Title {get;set;}
        public string Author {get;set;}
        public int Pages {get;set;}
    }
}