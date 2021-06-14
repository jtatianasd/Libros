using System.ComponentModel.DataAnnotations;

namespace CRUDLibros.Models.DTO
{
	public class EditorialDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "La direccion de correspondencia es obligatoria")]
		public string DireccionCorrespondencia { get; set; }

		[Required(ErrorMessage = "El Telefono es obligatorio")]
		public string Telefono { get; set; }

		[Required(ErrorMessage = "El Email es obligatorio")]
		public string Email { get; set; }

		public string MaximoLibros { get; set; }
	}
}
