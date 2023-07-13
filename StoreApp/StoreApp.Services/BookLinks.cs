using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models.LinkModels;
using StoreApp.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public class BookLinks : IBookLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<BookDto> _dataShaper;

        public BookLinks(LinkGenerator linkGenerator, IDataShaper<BookDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }
        public LinkResponse TryGenerateLinks(IEnumerable<BookDto> bookDtos, string fields, HttpContext httpContext)
        {
            var shapedBooks = ShapeData(bookDtos, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedBooks(bookDtos,fields,httpContext,shapedBooks);
            return ReturnShapedBooks(shapedBooks);
        }

        private LinkResponse ReturnLinkedBooks(IEnumerable<BookDto> bookDtos, string fields, HttpContext httpContext, List<Entity> shapedBooks)
        {
            var bookDtoList = bookDtos.ToList();
            for (int index = 0; index < bookDtoList.Count; index++)
            {
                var bookLinks = CreateForBook(httpContext, bookDtoList[index], fields);
                shapedBooks[index].Add("Links", bookLinks);
            }

            var bookCollectionWrapper = new LinkCollectionWrapper<Entity>(shapedBooks);
            CreateForBooks(httpContext, bookCollectionWrapper);

            return new LinkResponse { HasLinks = true, LinkedEntities = bookCollectionWrapper };
        }

        private LinkCollectionWrapper<Entity> CreateForBooks(HttpContext httpContext,LinkCollectionWrapper<Entity> bookCollectionWrapper)
        {
            bookCollectionWrapper.Links.Add(
                    new Link()
                    {
                        HyperReference = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                        Relation = "self",
                        Method = "GET"
                    }
                );

            return bookCollectionWrapper;
        }

        private List<Link> CreateForBook(HttpContext httpContext, BookDto bookDto, string fields)
        {
            var links = new List<Link>()
            {
                new Link()
                {
                    HyperReference = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                    $"/{bookDto.Id}",
                    Relation = "self",
                    Method = "GET"
                },
                new Link()
                {
                    HyperReference = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                    Relation = "create",
                    Method = "POST"
                }
            };
            return links;
        }

        private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
        {
            return new LinkResponse() { HasLinks = false, ShapedEntities = shapedBooks };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType
                .SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<BookDto> bookDtos, string fields)
        {
            return _dataShaper.ShapeData(bookDtos, fields).Select(se => se.Entity).ToList();
        }
    }
}
