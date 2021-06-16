using CRUDLibros.Models;
using System.Collections.Generic;

namespace CRUDLibros.Repository.IRepository
{
	public interface ILibroRepository
	{
		ICollection<Libro> GetLibros();
		ICollection<Libro> GetLibrosEnEditorial(int editorialId);
		ICollection<Libro> GetLibrosEnAutor(int autorId);
		Libro GetLibro(int libroId);
		bool ExisteLibro(string titulo);
		IEnumerable<Libro> BuscarLibro(string titulo);
		bool ExisteLibro(int id);
		bool CrearLibro(Libro libro);
		bool ActualizarLibro(Libro libro);
		bool BorrarLibro(Libro libro);
		bool Guardar();
	}
}
