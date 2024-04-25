using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiNotas.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ApiAlmacen.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly NotasContext _dbcontext;

        public NotaController(NotasContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        [Route("listaNotas")]
        public IActionResult Lista()
        {
            try
            {
                var lista = _dbcontext.Notas.Find(_ => true).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("crearNota")]
        public IActionResult Crear([FromBody] Nota nota)
        {
            try
            {
                _dbcontext.Notas.InsertOne(nota);
                return Ok(new { mensaje = "Nota creada exitosamente", nota });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarNota/{id}")]
        public IActionResult Editar(string id, [FromBody] Nota nota)
        {
            try
            {
                var notaExistente = _dbcontext.Notas.Find(n => n.Id == id).FirstOrDefault();
                if (notaExistente == null)
                    return NotFound(new { mensaje = "Nota no encontrada" });

                nota.Id = notaExistente.Id; // Mantenemos el mismo ID
                _dbcontext.Notas.ReplaceOne(n => n.Id == id, nota);
                return Ok(new { mensaje = "Nota actualizada exitosamente", nota });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarNota/{id}")]
        public IActionResult Eliminar(string id)
        {
            try
            {
                var resultado = _dbcontext.Notas.DeleteOne(n => n.Id == id);
                if (resultado.DeletedCount == 0)
                    return NotFound(new { mensaje = "Nota no encontrada" });

                return Ok(new { mensaje = "Nota eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
