using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truedrive_backend.Data;
using truedrive_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace truedrive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly TrueDriveContext _context;

        public PolicyController(TrueDriveContext context)
        {
            _context = context;
        }

        // GET: api/Policy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies()
        {
            return await _context.Policy.ToListAsync();
        }

        // GET: api/Policy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Policy>> GetPolicy(int id)
        {
            var policy = await _context.Policy.FindAsync(id);

            if (policy == null)
            {
                return NotFound();
            }

            return policy;
        }

        // POST: api/Policy
        [HttpPost]
        public async Task<ActionResult<Policy>> PostPolicy(PolicyDto policyDto)
        {
            var policy = new Policy
            {
                PolicyName = policyDto.PolicyName,
                Content = policyDto.Content,
                Status = policyDto.Status,
                EffectiveDate = policyDto.EffectiveDate,
                LastUpdated = DateTime.UtcNow // Set the current date and time
            };

            _context.Policy.Add(policy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPolicy), new { id = policy.PolicyId }, policy);
        }

        // PUT: api/Policy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicy(int id, Policy policy)
        {
            if (id != policy.PolicyId)
            {
                return BadRequest();
            }

            policy.LastUpdated = DateTime.UtcNow; // Update the LastUpdated field

            _context.Entry(policy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
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

        // DELETE: api/Policy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var policy = await _context.Policy.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            _context.Policy.Remove(policy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PolicyExists(int id)
        {
            return _context.Policy.Any(e => e.PolicyId == id);
        }
    }

    public class PolicyDto
    {
        public string PolicyName { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
