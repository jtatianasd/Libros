using CRUDLibros.Data;
using CRUDLibros.Models;
using CRUDLibros.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDLibros.Repository
{
	public class LibroRepository : ILibroRepository
	{
		private readonly ApplicationDbContext _bd;
		public LibroRepository(ApplicationDbContext bd)
		{
			_bd = bd;
		}
		public bool ActualizarLibro(Libro libro)
		{
			_bd.Libro.Update(libro);
			return Guardar();
		}

		public bool BorrarLibro(Libro libro)
		{
			_bd.Libro.Remove(libro);
			return Guardar();
		}

		public IEnumerable<Libro> BuscarLibro(string titulo)
		{
			IQueryable<Libro> query = _bd.Libro;
			if (!string.IsNullOrEmpty(titulo))
			{
				query = query.Where(e => e.Titulo.Contains(titulo));
			}
			return query.ToList();
		}

		public bool CrearLibro(Libro libro)
		{
			_bd.Libro.Add(libro);
			return Guardar();
		}

		public bool ExisteLibro(string titulo)
		{
			bool valor = _bd.Libro.Any(c => c.Titulo.ToLower().Trim() == titulo.ToLower().Trim());
			return valor;
		}

		public bool ExisteLibro(int id)
		{
			return _bd.Libro.Any(c => c.Id == id);
		}

		public Libro GetLibro(int libroId)
		{
			return _bd.Libro.FirstOrDefault(c => c.Id == libroId);
		}

		public ICollection<Libro> GetLibros()
		{
			return _bd.Libro.OrderBy(c => c.Titulo).ToList();
		}

		public ICollection<Libro> GetLibrosEnAutor(int autorId)
		{
			return _bd.Libro.Include(li => li.Autor).Where(li => li.AutorId == autorId).ToList();
		}

		public ICollection<Libro> GetLibrosEnEditorial(int editorialId)
		{
			return _bd.Libro.Include(li => li.Editorial).Where(li => li.EditorialId == editorialId).ToList();
		}

		public bool Guardar()
		{
			return _bd.SaveChanges() >= 0 ? true : false;
		}
	}
}
