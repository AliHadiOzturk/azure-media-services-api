using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.app.services;
using VideoAPI.app.services.models;

namespace VideoAPI.app.controller
{
    [Route("[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ContentService contentService;

        public ContentController(ContentService contentService)
        {
            this.contentService = contentService;
        }
        
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<bool>> CreateContent([FromForm]IFormFile file)
        {
            try
            {
                await contentService.CreateContent(file);
                return Ok(true);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<bool>> PublishDocument([FromQuery]long documentId)
        {
            try
            {
                await contentService.publishDocument(documentId);
                return Ok(true);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}