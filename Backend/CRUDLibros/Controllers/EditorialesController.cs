using AutoMapper;
using CRUDLibros.Models;
using CRUDLibros.Models.DTO;
using CRUDLibros.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUDLibros.Controllers
{
	[Route("api/Editoriales")]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public class EditorialesController : Controller
	{
		private readonly IEditorialRepository _editorialRepo;
		private readonly IMapper _mapper;

		public EditorialesController(IEditorialRepository editorialRepo, IMapper mapper)
		{
			_editorialRepo = editorialRepo;
			_mapper = mapper;
		}
		/// <summary>
		/// Obtener todas las editoriales
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<EditorialDTO>))]
		[ProducesResponseType(400)]
		public IActionResult GetEditoriales()
		{
			var listaEditoriales = _editorialRepo.GetEditoriales();
			var listaEditorialDTO = new List<EditorialDTO>();
			foreach (var lista in listaEditoriales)
			{
				listaEditorialDTO.Add(_mapper.Map<EditorialDTO>(lista));
			}
			return Ok(listaEditorialDTO);
		}

		/// <summary>
		///Obtener una editorial individual
		/// </summary>
		/// <param name="editorialId"> </param>
		/// <returns></returns>
		[HttpGet("{editorialId:int}", Name = "GetEditorial")]
		[ProducesResponseType(200, Type = typeof(EditorialDTO))]
		[ProducesResponseType(404)]
		[ProducesDefaultResponseType]
		public IActionResult GetEditorial(int editorialId)
		{
			var itemEditorial = _editorialRepo.GetEditorial(editorialId);
			if (itemEditorial == null)
			{
				return NotFound();
			}
			else
			{
				var itemEditorialDTO = _mapper.Map<EditorialDTO>(itemEditorial);
				return Ok(itemEditorialDTO);
			}

		}
		/// <summary>
		/// Crear una nueva editorial
		/// </summary>
		/// <param name="editorialDTO"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(EditorialDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CrearLibro([FromBody] EditorialDTO editorialDTO)
		{
			if (editorialDTO == null)
			{
				return BadRequest(ModelState);
			}
			if (_editorialRepo.ExisteEditorial(editorialDTO.Nombre))
			{
				ModelState.AddModelError("", "La Editorial ya existe");
				return StatusCode(404, ModelState);
			}

			var editorial = _mapper.Map<Editorial>(editorialDTO);
			if (!_editorialRepo.CrearEditorial(editorial))
			{
				ModelState.AddModelError("", $"Algo Salio mal guardando el registro{editorial.Nombre}");
				return StatusCode(500, ModelState);
			}
			return CreatedAtRoute("GetEditorial", new { editorialId = editorial.Id }, editorial);
		}
		/// <summary>
		/// Actualizar una editorial existente
		/// </summary>
		/// <param name="editorialId"></param>
		/// <param name="editorialDTO"></param>
		/// <returns></returns>
		[HttpPatch("{editorialId:int}", Name = "ActualizarEditorial")]
		[ProducesResponseType(204)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult ActualizarEditorial(int editorialId, [FromBody] EditorialDTO editorialDTO)
		{
			if (editorialDTO == null || editorialId != editorialDTO.Id)
			{
				return BadRequest(ModelState);
			}
			var editorial = _mapper.Map<Editorial>(editorialDTO);
			if (!_editorialRepo.ActualizarEditorial(editorial))
			{
				ModelState.AddModelError("", $"Algo Salio mal actualizando el registro{editorial.Nombre}");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}

		/// <summary>
		/// Borrar una editorial existente
		/// </summary>
		/// <param name="editorialId"></param>
		/// <returns></returns>
		[HttpDelete("{editorialId:int}", Name = "BorrarEditorial")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public IActionResult BorrarEditorial(int editorialId)
		{
			if (!_editorialRepo.ExisteEditorial(editorialId))
			{
				return NotFound();
			}
			var editorial = _editorialRepo.GetEditorial(editorialId);
			if (!_editorialRepo.BorrarEditorial(editorial))
			{
				ModelState.AddModelError("", $"Algo Salio mal borrando el registro{editorial.Nombre}");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
	}
}
