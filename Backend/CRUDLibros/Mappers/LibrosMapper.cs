using CRUDLibros.Models;
using CRUDLibros.Models.DTO;
using AutoMapper;


namespace CRUDLibros.Mappers
{
	public class LibrosMapper : Profile
	{
		public LibrosMapper()
		{
			CreateMap<Autor, AutorDTO>().ReverseMap();
			CreateMap<Editorial, EditorialDTO>().ReverseMap();
			CreateMap<Libro, LibroDTO>().ReverseMap();


		}
	}
}
