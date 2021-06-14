using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDLibros.Models
{
	public class Libro
	{
		[Key]
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string Año { get; set; }
		public string Genero { get; set; }
		public string NumeroPaginas { get; set; }

		//Llave foranea con la tabla Editorial
		public int EditorialId { get; set; }
		[ForeignKey("EditorialId")]
		public virtual Editorial Editorial { get; set; }

		//Llave foranea con la tabla Autor
		public int AutorId { get; set; }
		[ForeignKey("AutorId")]
		public virtual Autor Autor { get; set; }



	}
}
