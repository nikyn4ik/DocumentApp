﻿using Microsoft.AspNetCore.Mvc;
using DocumentApp.Infrastructure;

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly Context _context;
        private readonly ISecurity _security;

        public ImportController(Context context)
        {
            _context = context;
            _security = new MockSecurityProvider();
        }

        // GET api/Import/5
        [HttpGet("{uri}")]
        public async Task<IActionResult> GetPublication(Uri uri)
        {
            try
            {
                Importer importer = new(uri, _context, _security.GetUserId());
                await importer.ImportAsync();

                return NoContent();
            }
            catch (ArgumentNullException exception) 
            {
                return NotFound(exception.ParamName);
            }
        }
    }
}