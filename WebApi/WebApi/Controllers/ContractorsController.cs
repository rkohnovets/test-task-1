using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContractorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contractors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contractor>>> GetContractors()
        {
            return await _context.Contractors.Include(c => c.Contacts).ToListAsync();
        }

        // GET: api/Contractors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contractor>> GetContractor(int id)
        {
            var contractor = await _context.Contractors.Include(c => c.Contacts).FirstOrDefaultAsync(c => c.ContractorID == id);

            if (contractor == null)
            {
                return NotFound();
            }

            return contractor;
        }

        // GET: api/Contractors/5/contacts
        [HttpGet("{contractorId}/contacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContactsByContractor(int contractorId)
        {
            var contacts = await _context.Contacts
                                         .Where(c => c.ContractorID == contractorId)
                                         .ToListAsync();

            if (!contacts.Any())
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        // POST: api/Contractors
        [HttpPost]
        public async Task<ActionResult<Contractor>> PostContractor(Contractor contractor)
        {
            _context.Contractors.Add(contractor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractor", new { id = contractor.ContractorID }, contractor);
        }

        // PUT: api/Contractors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractor(int id, Contractor contractor)
        {
            if (id != contractor.ContractorID)
            {
                return BadRequest();
            }

            _context.Entry(contractor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractorExists(id))
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

        // DELETE: api/Contractors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractor(int id)
        {
            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }

            _context.Contractors.Remove(contractor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractors.Any(e => e.ContractorID == id);
        }
    }
}
