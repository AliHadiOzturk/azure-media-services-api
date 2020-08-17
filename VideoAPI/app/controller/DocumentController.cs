using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoAPI.app.services;
using VideoAPI.app.services.models;

namespace VideoAPI.app.controller
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService documentService;

        public DocumentController(DocumentService documentService)
        {
            this.documentService = documentService;
        }

        public async Task<ActionResult<DocumentListItem>> GetAll()
        {
            try
            {
                var items = await documentService.GetDocumentListItemsAsync();
                return Ok(items);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("getDocument")]
        public async Task<ActionResult<DocumentListItem>> GetAll([FromQuery]long documentId)
        {
            try
            {
                var items = await documentService.GetDocumentResponseAsync(documentId);
                return Ok(items);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

    }
}