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
    public class NotaController : ControllerBase
    {
        public readonly NotasContext _dbcontext;

        public NotaController(NotasContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listaNotas")]
        public IActionResult Lista()
        {
            List<Nota> lista = new List<Nota>();

            try
            {
                lista = _dbcontext.Notas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }


        [HttpPost]
        [Route("crearNota")]
        public IActionResult Crear([FromBody] Nota nota)
        {
            try
            {
                _dbcontext.Notas.Add(nota);
                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "Nota creada exitosamente", nota });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarNota/{id}")]
        public IActionResult Editar(int id, [FromBody] Nota nota)
        {
            try
            {
                if (id != nota.Id)
                    return BadRequest(new { mensaje = "El ID de la nota no coincide" });

                _dbcontext.Entry(nota).State = EntityState.Modified;
                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "Nota actualizada exitosamente", nota });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarNota/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var nota = _dbcontext.Notas.Find(id);
                if (nota == null)
                    return NotFound(new { mensaje = "Nota no encontrada" });

                _dbcontext.Notas.Remove(nota);
                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "Nota eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}
