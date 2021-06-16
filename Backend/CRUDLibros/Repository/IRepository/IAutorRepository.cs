using CRUDLibros.Models;
using System.Collections.Generic;

namespace CRUDLibros.Repository.IRepository
{
	public interface IAutorRepository
	{
		ICollection<Autor> GetAutores();
		Autor GetAutor(int AutorId);
		bool ExisteAutor(string nombre);
		bool ExisteAutor(int id);
		bool CrearAutor(Autor autor);
		bool ActualizarAutor(Autor autor);
		bool BorrarAutor(Autor autor);
		bool Guardar();
	}
}
