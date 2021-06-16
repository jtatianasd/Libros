using CRUDLibros.Data;
using CRUDLibros.Models;
using CRUDLibros.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDLibros.Repository
{
	public class AutorRepository : IAutorRepository
	{
		private readonly ApplicationDbContext _bd;
		public AutorRepository(ApplicationDbContext bd)
		{
			_bd = bd;
		}
		public bool ActualizarAutor(Autor autor)
		{
			_bd.Autor.Update(autor);
			return Guardar();
		}

		public bool BorrarAutor(Autor autor)
		{
			_bd.Autor.Remove(autor);
			return Guardar();
		}

		public bool CrearAutor(Autor autor)
		{
			_bd.Autor.Add(autor);
			return Guardar();
		}

		public bool ExisteAutor(string nombre)
		{
			bool valor = _bd.Autor.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
			return valor;
		}

		public bool ExisteAutor(int id)
		{
			return _bd.Autor.Any(c => c.Id == id);
		}

		public Autor GetAutor(int AutorId)
		{
			return _bd.Autor.FirstOrDefault(c => c.Id == AutorId);
		}

		public ICollection<Autor> GetAutores()
		{
			return _bd.Autor.OrderBy(c => c.Nombre).ToList();
		}

		public bool Guardar()
		{
			return _bd.SaveChanges() >= 0 ? true : false;
		}
	}
}
