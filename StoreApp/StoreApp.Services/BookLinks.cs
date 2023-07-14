using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models.LinkModels;
using StoreApp.Services.Abstract;

namespace StoreApp.Services
{
    public class BookLinks<T> : IBookLinks<T> where T:BookDto
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<T> _dataShaper;

        public BookLinks(LinkGenerator linkGenerator, IDataShaper<T> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }
        public LinkResponse TryGenerateLinks(IEnumerable<T> dtos, string fields, HttpContext httpContext)
        {
            var shapedBooks = ShapeData(dtos, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedBooks(dtos, fields,httpContext,shapedBooks);
            return ReturnShapedBooks(shapedBooks);
        }

        private LinkResponse ReturnLinkedBooks(IEnumerable<T> dtos, string fields, HttpContext httpContext, List<Entity> shapedBooks)
        {
            var dtoList = dtos.ToList();
            for (int index = 0; index < dtoList.Count; index++)
            {
                var bookLinks = CreateForBook(httpContext, dtoList[index], fields);
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

        private List<Link> CreateForBook(HttpContext httpContext, T dto, string fields)
        {
            var links = new List<Link>()
            {
                new Link()
                {
                    HyperReference = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                    $"/{dto.Id}",
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

        private List<Entity> ShapeData(IEnumerable<T> dtos, string fields)
        {
            return _dataShaper.ShapeData(dtos, fields).Select(se => se.Entity).ToList();
        }
    }
}
