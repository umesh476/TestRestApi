using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApiProject.Models;
using log4net;
using Microsoft.Extensions.Logging;

namespace TestApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestItemsController : ControllerBase
    {
        private readonly TestContext _context;
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private  readonly ILogger<TestItemsController> logger;

        //private readonly _logger;


        //public static bool SaveLogEntry(string ErrorMessage)
        //{
        //    try
        //    {
        //        StringBuilder sbMessage = new StringBuilder();
        //        sbMessage.Append("\r\n");
        //        sbMessage.Append("\r\n");
        //        sbMessage.Append("Date --" + System.DateTime.Now);
        //        sbMessage.Append("\r\n");
        //        sbMessage.Append("ErrorMessage --" + ErrorMessage);
        //        sbMessage.Append("\r\n");
        //        sbMessage.Append("\r\n");
        //        sbMessage.Append("****************************************************************************************");

        //        bool flag = WriteToLog(sbMessage);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex = null;
        //        return false;
        //    }
        //}

        //private static bool WriteToLog(StringBuilder sbMessage)
        //{
        //    try
        //    {
        //        FileStream fs;
        //        string strLogFileName = "Testproject_Log_" + DateTime.Now.ToString("yyyyMMdd") + ".config";
        //        string strLogFilePath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
        //        if (!Directory.Exists(strLogFilePath))
        //            Directory.CreateDirectory(strLogFilePath);

        //        //if (File.Exists(strLogFilePath + strLogFileName) == true) { fs = File.Open(strLogFilePath + strLogFileName, FileMode.Append, FileAccess.Write); }
        //        //else { fs = File.Create(strLogFilePath + strLogFileName); }
        //        //{
        //        //    using (StreamWriter sw = new StreamWriter(fs)) { sw.Write(sbMessage.ToString() + Environment.NewLine); }
        //        //    fs.Close();
        //        //}
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex = null;
        //        return false;
        //    }
        //}

        public TestItemsController(TestContext context,ILogger<TestItemsController> logger)
        {
            // logger.LogInformation("Erro success");
            _context = context;
            this.logger = logger;
           // log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~log4Net.. ")));
        }

        // GET: api/TestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiTestItem>>> GetTestItems()
        {
            logger.LogDebug("get by method");
            logger.LogInformation("Getting item {Id} not fount");
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
