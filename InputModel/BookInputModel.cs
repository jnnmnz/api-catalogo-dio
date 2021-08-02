using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoDIO.InputModel {

    public class BookInputModel {
        [Required]
        [StringLength(100, MinimumLength=2,ErrorMessage="O TÍTULO DEVE TER ENTRE 2 E 100 CARACTERES")]
        public string Title {get;set;}

        [Required]
        [StringLength(120, MinimumLength=2,ErrorMessage="O NOME DO AUTOR DEVE TER ENTRE 2 E 100 CARACTERES")]
        public string Author {get;set;}

        [Required]
        [Range(1, 20000, ErrorMessage="O LIVRO DEVE TER ENTRE 1 E 20.000 PÁGINAS")]
        public int Pages {get;set;}


    }

}