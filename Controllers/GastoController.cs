using Entidades;
using Gastos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BEGastos.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class GastoController : ControllerBase
    {
        
        private readonly GastosContext _context;

        public GastoController(GastosContext context)
        {
            _context = context;
        }

        // GET: api/Gasto
        [HttpGet]
        public ActionResult<IEnumerable<Gasto>> GetGastos()
        {
            return _context.Gastos;
        }

        // GET: api/Gasto/5
        [HttpGet("{id}")]
        public ActionResult<Gasto> GetGasto(int id)
        {
            var gasto = _context.Gastos.Find(id);

            if (gasto == null)
            {
                return NotFound();
            }

            return gasto;
        }

        // PUT: api/Gasto/5
        [HttpPut("{id}")]
        public IActionResult PutGasto(int id, Gasto gasto)
        {
            if (id != gasto.idGasto)
            {
                return BadRequest();
            }

            _context.Entry(gasto).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GastoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gasto
        [HttpPost]
        public ActionResult<Gasto> PostGasto(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            _context.SaveChanges();

            return CreatedAtAction("GetGasto", new { id = gasto.idGasto }, gasto);
        }

        // DELETE: api/Gasto/5
        [HttpDelete("{id}")]
        public ActionResult<Gasto> DeleteGasto(int id)
        {
            var gasto = _context.Gastos.Find(id);
            if (gasto == null)
            {
                return NotFound();
            }

            _context.Gastos.Remove(gasto);
            _context.SaveChanges();

            return gasto;
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.idGasto == id);
        }
    }
    
}