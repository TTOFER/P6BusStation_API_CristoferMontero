using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P6BusStation_API_CristoferMontero.Models;

namespace P6BusStation_API_CristoferMontero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketPurchasesController : ControllerBase
    {
        private readonly P620242ticketBusContext _context;

        public TicketPurchasesController(P620242ticketBusContext context)
        {
            _context = context;
        }

        // GET: api/TicketPurchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPurchase>>> GetTicketPurchases()
        {
            return await _context.TicketPurchases.ToListAsync();
        }

        // GET: api/TicketPurchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketPurchase>> GetTicketPurchase(int id)
        {
            var ticketPurchase = await _context.TicketPurchases.FindAsync(id);

            if (ticketPurchase == null)
            {
                return NotFound();
            }

            return ticketPurchase;
        }

        // PUT: api/TicketPurchases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketPurchase(int id, TicketPurchase ticketPurchase)
        {
            if (id != ticketPurchase.TicketPurchaseId)
            {
                return BadRequest();
            }

            _context.Entry(ticketPurchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketPurchaseExists(id))
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

        // POST: api/TicketPurchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketPurchase>> PostTicketPurchase(TicketPurchase ticketPurchase)
        {
            _context.TicketPurchases.Add(ticketPurchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketPurchase", new { id = ticketPurchase.TicketPurchaseId }, ticketPurchase);
        }

        // DELETE: api/TicketPurchases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketPurchase(int id)
        {
            var ticketPurchase = await _context.TicketPurchases.FindAsync(id);
            if (ticketPurchase == null)
            {
                return NotFound();
            }

            _context.TicketPurchases.Remove(ticketPurchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketPurchaseExists(int id)
        {
            return _context.TicketPurchases.Any(e => e.TicketPurchaseId == id);
        }
    }
}
