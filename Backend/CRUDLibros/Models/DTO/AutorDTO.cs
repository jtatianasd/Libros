using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDLibros.Models.DTO
{
	public class AutorDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "El Apellido es obligatorio")]
		public string Apellido { get; set; }

		[Required(ErrorMessage = "La Fecha Nacimiento es obligatoria")]
		public DateTime FechaNacimiento { get; set; }

		[Required(ErrorMessage = "La Ciudad Procedencia es obligatoria")]
		public string CiudadProcedencia { get; set; }

		[Required(ErrorMessage = "El Email es obligatorio")]
		public string Email { get; set; }

	}
}
