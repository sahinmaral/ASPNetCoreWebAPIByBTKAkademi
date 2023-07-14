using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using StoreApp.Entities.Models.LinkModels;
using StoreApp.WebAPI.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetRoot")]
        public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")]string mediaType)
        {
            if (mediaType.Contains("application/vnd.storeapp.apiroot"))
            {
                var list = new List<Link>()
                { 
                    new Link()
                    {
                        HyperReference = _linkGenerator.GetUriByName(HttpContext,nameof(GetRoot),new{}),
                        Relation = "_self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        HyperReference = _linkGenerator.GetUriByName(HttpContext,nameof(BooksController.GetBooks),new{}),
                        Relation = "books",
                        Method = "GET"
                    },
                    new Link()
                    {
                        HyperReference = _linkGenerator.GetUriByName(HttpContext,nameof(BooksController.InsertBook),new{}),
                        Relation = "books",
                        Method = "POST"
                    }
                };


                return Ok(list);
            }

            return NoContent();
        }
    }
}
