using CRUDLibros.Data;
using CRUDLibros.Models;
using CRUDLibros.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace CRUDLibros.Repository
{
	public class EditorialRepository : IEditorialRepository
	{
		private readonly ApplicationDbContext _bd;
		public EditorialRepository(ApplicationDbContext bd)
		{
			_bd = bd;
		}
		public bool ActualizarEditorial(Editorial editorial)
		{
			_bd.Editorial.Update(editorial);
			return Guardar();
		}

		public bool BorrarEditorial(Editorial editorial)
		{
			_bd.Editorial.Remove(editorial);
			return Guardar();
		}

		public bool CrearEditorial(Editorial editorial)
		{
			_bd.Editorial.Add(editorial);
			return Guardar();
		}

		public bool ExisteEditorial(string nombre)
		{
			bool valor = _bd.Editorial.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
			return valor;
		}

		public bool ExisteEditorial(int id)
		{
			return _bd.Editorial.Any(c => c.Id == id);
		}

		public Editorial GetEditorial(int EditorialId)
		{
			return _bd.Editorial.FirstOrDefault(c => c.Id == EditorialId);
		}

		public ICollection<Editorial> GetEditoriales()
		{
			return _bd.Editorial.OrderBy(c => c.Nombre).ToList();
		}

		public bool Guardar()
		{
			return _bd.SaveChanges() >= 0 ? true : false;
		}
	}
}
