using AutoMapper;
using CRUDLibros.Models;
using CRUDLibros.Models.DTO;
using CRUDLibros.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUDLibros.Controllers
{
	[Route("api/Libros")]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public class LibrosController : Controller
	{
		private readonly ILibroRepository _libroRepo;
		private readonly IMapper _mapper;

		public LibrosController(ILibroRepository libroRepo, IMapper mapper)
		{
			_libroRepo = libroRepo;
			_mapper = mapper;
		}
		/// <summary>
		/// Obtener todos los libros
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<LibroDTO>))]
		[ProducesResponseType(400)]
		public IActionResult GetLibros()
		{
			var listaLibros = _libroRepo.GetLibros();
			var listaLibrosDTO = new List<LibroDTO>();
			foreach (var lista in listaLibros)
			{
				listaLibrosDTO.Add(_mapper.Map<LibroDTO>(lista));
			}
			return Ok(listaLibrosDTO);
		}

		/// <summary>
		///Obtener un libro individual
		/// </summary>
		/// <param name="libroId"> </param>
		/// <returns></returns>
		[HttpGet("{libroId:int}", Name = "GetLibro")]
		[ProducesResponseType(200, Type = typeof(LibroDTO))]
		[ProducesResponseType(404)]
		[ProducesDefaultResponseType]
		public IActionResult GetLibro(int libroId)
		{
			var itemLibros = _libroRepo.GetLibro(libroId);
			if (itemLibros == null)
			{
				return NotFound();
			}
			else
			{
				var itemLibroDTO = _mapper.Map<LibroDTO>(itemLibros);
				return Ok(itemLibroDTO);
			}

		}
		/// <summary>
		/// Obtener libros por autor
		/// </summary>
		/// <param name="autorId"> </param>
		/// <returns></returns>
		[HttpGet("GetLibrosEnAutor/{autorId:int}")]
		[ProducesResponseType(200, Type = typeof(LibroDTO))]
		[ProducesResponseType(404)]
		[ProducesDefaultResponseType]
		public IActionResult GetLibrosEnAutor(int autorId)
		{
			var listaLibros = _libroRepo.GetLibrosEnAutor(autorId);
			if (listaLibros == null || listaLibros.Count == 0)
			{
				return NotFound();
			}
			var itemLibro = new List<LibroDTO>();
			foreach (var item in listaLibros)
			{
				itemLibro.Add(_mapper.Map<LibroDTO>(item));
			}
			return Ok(itemLibro);
		}
		/// <summary>
		/// Obtener libros por editorial
		/// </summary>
		/// <param name="editorialId"> </param>
		/// <returns></returns>
		[HttpGet("GetLibrosEnEditorial/{editorialId:int}")]
		[ProducesResponseType(200, Type = typeof(LibroDTO))]
		[ProducesResponseType(404)]
		[ProducesDefaultResponseType]
		public IActionResult GetLibrosEnEditorial(int editorialId)
		{
			var listaLibros = _libroRepo.GetLibrosEnEditorial(editorialId);
			if (listaLibros == null || listaLibros.Count == 0)
			{
				return NotFound();
			}
			var itemLibro = new List<LibroDTO>();
			foreach (var item in listaLibros)
			{
				itemLibro.Add(_mapper.Map<LibroDTO>(item));
			}
			return Ok(itemLibro);
		}
		/// <summary>
		/// Crear un nuevo libro
		/// </summary>
		/// <param name="libroDTO"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(LibroDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CrearLibro([FromBody] LibroDTO libroDTO)
		{
			if (libroDTO == null)
			{
				return BadRequest(ModelState);
			}
			if (_libroRepo.ExisteLibro(libroDTO.Titulo))
			{
				ModelState.AddModelError("", "El libro ya existe");
				return StatusCode(404, ModelState);
			}

			var libro = _mapper.Map<Libro>(libroDTO);
			if (!_libroRepo.CrearLibro(libro))
			{
				ModelState.AddModelError("", $"Algo Salio mal guardando el registro{libro.Titulo}");
				return StatusCode(500, ModelState);
			}
			return CreatedAtRoute("GetLibro", new { libroId = libro.Id }, libro);
		}
		/// <summary>
		/// Actualizar un libro existente
		/// </summary>
		/// <param name="libroId"></param>
		/// <param name="libroDTO"></param>
		/// <returns></returns>
		[HttpPatch("{libroId:int}", Name = "ActualizarLibro")]
		[ProducesResponseType(204)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult ActualizarLibro(int libroId, [FromBody] LibroDTO libroDTO)
		{
			if (libroDTO == null || libroId != libroDTO.Id)
			{
				return BadRequest(ModelState);
			}
			var libro = _mapper.Map<Libro>(libroDTO);
			if (!_libroRepo.ActualizarLibro(libro))
			{
				ModelState.AddModelError("", $"Algo Salio mal actualizando el registro{libro.Titulo}");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}

		/// <summary>
		/// Borrar un libro existente
		/// </summary>
		/// <param name="libroId"></param>
		/// <returns></returns>
		[HttpDelete("{libroId:int}", Name = "BorrarLibro")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public IActionResult BorrarLibro(int libroId)
		{
			if (!_libroRepo.ExisteLibro(libroId))
			{
				return NotFound();
			}
			var libro = _libroRepo.GetLibro(libroId);
			if (!_libroRepo.BorrarLibro(libro))
			{
				ModelState.AddModelError("", $"Algo Salio mal borrando el registro{libro.Titulo}");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
	}
}
