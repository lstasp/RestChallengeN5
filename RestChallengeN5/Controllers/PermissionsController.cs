using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using RestChallengeN5.Models;

namespace RestChallengeN5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly DbChallengeN5Context _context;

        //public PermissionsController(DbChallengeN5Context context)
        //{
        //    _context = context;
        //}

        //Adding ElasticSearch settings
        private readonly IElasticClient _elasticClient;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PermissionsController(IElasticClient elasticClient, IWebHostEnvironment hostingEnvironment, DbChallengeN5Context context)
        {
            _elasticClient = elasticClient;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: api/Permissions
        [HttpGet("GetPermissions")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
          if (_context.Permissions == null)
          {
              return NotFound();
          }
            return await _context.Permissions.ToListAsync();
            //return _elasticClient.IndexDocumentAsync(document: Pe);
        }

        // GET: api/Permissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
          if (_context.Permissions == null)
          {
              return NotFound();
          }
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return NotFound();
            }

            return permission;
        }

        // PUT: api/Permissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ModifyPermission/{id}")]
        public async Task<IActionResult> PutPermission(int id, Permission permission)
        {
            if (id != permission.Id)
            {
                return BadRequest();
            }

            _context.Entry(permission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
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

        // POST: api/Permissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("RequestPermission")]
        public async Task<ActionResult<Permission>> PostPermission(Permission permission)
        {
          if (_context.Permissions == null)
          {
              return Problem("Entity set 'DbChallengeN5Context.Permissions'  is null.");
          }
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermission", new { id = permission.Id }, permission);
        }

        // DELETE: api/Permissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            if (_context.Permissions == null)
            {
                return NotFound();
            }
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionExists(int id)
        {
            return (_context.Permissions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
