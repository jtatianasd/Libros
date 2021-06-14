using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDLibros.Models.DTO
{
	public class LibroDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El titulo es obligatorio")]
		public string Titulo { get; set; }

		[Required(ErrorMessage = "El Año es obligatorio")]
		public string Año { get; set; }

		[Required(ErrorMessage = "El Genero es obligatorio")]
		public string Genero { get; set; }

		[Required(ErrorMessage = "El Numero de Paginas es obligatorio")]
		public string NumeroPaginas { get; set; }


		//Llave foranea con la tabla Editorial
		public int EditorialId { get; set; }
		public virtual Editorial Editorial { get; set; }

		//Llave foranea con la tabla Autor
		public int AutorId { get; set; }
		public virtual Autor Autor { get; set; }
	}
}
