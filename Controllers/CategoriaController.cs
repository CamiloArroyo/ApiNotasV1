using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiNotas.Models;
using Microsoft.AspNetCore.Cors;

namespace ApiAlmacen.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        public readonly NotasContext _dbcontext;

        public CategoriaController(NotasContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listaCatalogos")]
        public IActionResult Lista()
        {
            List<Categoria> lista = new List<Categoria>();

            try
            {
                lista = _dbcontext.Categorias.Include(s=>s.Notas).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }


        [HttpPost]
        [Route("crearCategoria")]
        public IActionResult Crear([FromBody] Categoria categoria)
        {
            try
            {
                _dbcontext.Categorias.Add(categoria);
                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "Categoria creada exitosamente", categoria });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarCategoria/{id}")]
        public IActionResult Editar(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest(new { mensaje = "El ID de la categoría no coincide" });

                _dbcontext.Entry(categoria).State = EntityState.Modified;
                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "Categoría actualizada exitosamente", categoria });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarCategoria/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var categoria = _dbcontext.Categorias.Find(id);
                if (categoria == null)
                    return NotFound(new { mensaje = "Categoría no encontrada" });

                _dbcontext.Categorias.Remove(categoria);
                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "Categoría eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }


}
