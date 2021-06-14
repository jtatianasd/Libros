using System.ComponentModel.DataAnnotations;

namespace CRUDLibros.Models
{
	public class Editorial
	{
		[Key]
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string DireccionCorrespondencia { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public string MaximoLibros { get; set; }

	}
}
