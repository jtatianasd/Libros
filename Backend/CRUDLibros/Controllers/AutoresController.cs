using AutoMapper;
using CRUDLibros.Models;
using CRUDLibros.Models.DTO;
using CRUDLibros.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUDLibros.Controllers
{
	[Route("api/Autores")]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public class AutoresController : Controller
	{
		private readonly IAutorRepository _autorRepo;
		private readonly IMapper _mapper;

		public AutoresController(IAutorRepository autorRepo, IMapper mapper)
		{
			_autorRepo = autorRepo;
			_mapper = mapper;
		}
		/// <summary>
		/// Obtener todos los autores
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<AutorDTO>))]
		[ProducesResponseType(400)]
		public IActionResult GetAutores()
		{
			var listaAutores = _autorRepo.GetAutores();
			var listaAutorDTO = new List<AutorDTO>();
			foreach (var lista in listaAutores)
			{
				listaAutorDTO.Add(_mapper.Map<AutorDTO>(lista));
			}
			return Ok(listaAutorDTO);
		}

		/// <summary>
		///Obtener un autor individual
		/// </summary>
		/// <param name="autorId"> </param>
		/// <returns></returns>
		[HttpGet("{autorId:int}", Name = "GetAutor")]
		[ProducesResponseType(200, Type = typeof(AutorDTO))]
		[ProducesResponseType(404)]
		[ProducesDefaultResponseType]
		public IActionResult GetAutor(int autorId)
		{
			var itemAutor = _autorRepo.GetAutor(autorId);
			if (itemAutor == null)
			{
				return NotFound();
			}
			else
			{
				var itemAutorDTO = _mapper.Map<AutorDTO>(itemAutor);
				return Ok(itemAutorDTO);
			}

		}
		/// <summary>
		/// Crear un nuevo autor
		/// </summary>
		/// <param name="autorDTO"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(AutorDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CrearAutor([FromBody] AutorDTO autorDTO)
		{
			if (autorDTO == null)
			{
				return BadRequest(ModelState);
			}
			if (_autorRepo.ExisteAutor(autorDTO.Nombre))
			{
				ModelState.AddModelError("", "El Autor ya existe");
				return StatusCode(404, ModelState);
			}

			var autor = _mapper.Map<Autor>(autorDTO);
			if (!_autorRepo.CrearAutor(autor))
			{
				ModelState.AddModelError("", $"Algo Salio mal guardando el registro{autor.Nombre}");
				return StatusCode(500, ModelState);
			}
			return CreatedAtRoute("GetAutor", new { autorId = autor.Id }, autor);
		}
		/// <summary>
		/// Actualizar un autor existente
		/// </summary>
		/// <param name="autorId"></param>
		/// <param name="autorDTO"></param>
		/// <returns></returns>
		[HttpPatch("{autorId:int}", Name = "ActualizarAutor")]
		[ProducesResponseType(204)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult ActualizarAutor(int autorId, [FromBody] AutorDTO autorDTO)
		{
			if (autorDTO == null || autorId != autorDTO.Id)
			{
				return BadRequest(ModelState);
			}
			var autor = _mapper.Map<Autor>(autorDTO);
			if (!_autorRepo.ActualizarAutor(autor))
			{
				ModelState.AddModelError("", $"Algo Salio mal actualizando el registro{autor.Nombre}");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}

		/// <summary>
		/// Borrar un autor existente
		/// </summary>
		/// <param name="autorId"></param>
		/// <returns></returns>
		[HttpDelete("{autorId:int}", Name = "BorrarAutor")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public IActionResult BorrarAutor(int autorId)
		{
			if (!_autorRepo.ExisteAutor(autorId))
			{
				return NotFound();
			}
			var autor = _autorRepo.GetAutor(autorId);
			if (!_autorRepo.BorrarAutor(autor))
			{
				ModelState.AddModelError("", $"Algo Salio mal borrando el registro{autor.Nombre}");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}


	}
}
