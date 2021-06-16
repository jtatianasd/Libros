using CRUDLibros.Models;
using System.Collections.Generic;

namespace CRUDLibros.Repository.IRepository
{
	public interface IEditorialRepository
	{
		ICollection<Editorial> GetEditoriales();
		Editorial GetEditorial(int EditorialId);
		bool ExisteEditorial(string nombre);
		bool ExisteEditorial(int id);
		bool CrearEditorial(Editorial editorial);
		bool ActualizarEditorial(Editorial editorial);
		bool BorrarEditorial(Editorial editorial);
		bool Guardar();
	}
}
