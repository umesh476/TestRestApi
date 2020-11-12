using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApiProject.Models;

namespace TestApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestItemsController : ControllerBase
    {
        private readonly TestContext _context;

        public TestItemsController(TestContext context)
        {
            _context = context;
        }

        // GET: api/TestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiTestItem>>> GetTestItems()
        {
            return await _context.TestItems.ToListAsync();
        }

        // GET: api/TestItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiTestItem>> GetTestItem(long id)
        {
            var testItem = await _context.TestItems.FindAsync(id);

            if (testItem == null)
            {
                return NotFound();
            }

            return testItem;
        }

        // PUT: api/TestItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestItem(long id, ApiTestItem testItem)
        {
            if (id != testItem.ApiTestId)
            {
                return BadRequest();
            }

            _context.Entry(testItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestItemExists(id))
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

        // POST: api/TestItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPost]
        public async Task<ActionResult<ApiTestItem>> PostTestItem(ApiTestItem TestItem)
        {
            _context.TestItems.Add(TestItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTestItem), new { id = TestItem.ApiTestId }, TestItem);
        }

        //[HttpPost]
        //public async Task<ActionResult<TestItem>> PostTestItem(TestItem testItem)
        //{
        //    _context.TestItems.Add(testItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTestItem", new { id = testItem.Id }, testItem);
        //}

        // DELETE: api/TestItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiTestItem>> DeleteTestItem(long id)
        {
            var testItem = await _context.TestItems.FindAsync(id);
            if (testItem == null)
            {
                return NotFound();
            }

            _context.TestItems.Remove(testItem);
            await _context.SaveChangesAsync();

            return testItem;
        }

        private bool TestItemExists(long id)
        {
            return _context.TestItems.Any(e => e.ApiTestId == id);
        }
    }
}
