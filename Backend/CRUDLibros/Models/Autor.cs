using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDLibros.Models
{
	public class Autor
	{
		[Key]
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public string CiudadProcedencia { get; set; }
		public string Email { get; set; }
	}
}
