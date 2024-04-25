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
    public class CategoriaController : ControllerBase
    {
        private readonly NotasContext _dbcontext;

        public CategoriaController(NotasContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        [Route("listaCatalogos")]
        public IActionResult Lista()
        {
            try
            {
                var lista = _dbcontext.Categorias.Find(_ => true).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("crearCategoria")]
        public IActionResult Crear([FromBody] Categoria categoria)
        {
            try
            {
                _dbcontext.Categorias.InsertOne(categoria);
                return Ok(new { mensaje = "Categoría creada exitosamente", categoria });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarCategoria/{id}")]
        public IActionResult Editar(string id, [FromBody] Categoria categoria)
        {
            try
            {
                var categoriaExistente = _dbcontext.Categorias.Find(c => c.Id == id).FirstOrDefault();
                if (categoriaExistente == null)
                    return NotFound(new { mensaje = "Categoría no encontrada" });

                categoria.Id = categoriaExistente.Id; // Mantenemos el mismo ID
                _dbcontext.Categorias.ReplaceOne(c => c.Id == id, categoria);
                return Ok(new { mensaje = "Categoría actualizada exitosamente", categoria });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarCategoria/{id}")]
        public IActionResult Eliminar(string id)
        {
            try
            {
                var resultado = _dbcontext.Categorias.DeleteOne(c => c.Id == id);
                if (resultado.DeletedCount == 0)
                    return NotFound(new { mensaje = "Categoría no encontrada" });

                return Ok(new { mensaje = "Categoría eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}
