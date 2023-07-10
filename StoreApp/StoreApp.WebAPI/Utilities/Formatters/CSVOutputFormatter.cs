using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

using StoreApp.Entities.DTOs;
using StoreApp.Entities.Models;

using System.Text;

namespace StoreApp.WebAPI.Utilities.Formatters
{
    public class CSVOutputFormatter : TextOutputFormatter
    {
        public CSVOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if(context.Object is IEnumerable<BookDto>)
            {
                foreach (var book in (IEnumerable<BookDto>)context.Object)
                {
                    FormatCSV(buffer, book);
                }
            }
            else
            {
                FormatCSV(buffer, (BookDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCSV(StringBuilder buffer,BookDto dto)
        {
            buffer.AppendLine($"{dto.Id}, {dto.Title}, {dto.Price}");
        }

        protected override bool CanWriteType(Type? type)
        {
            if(typeof(BookDto).IsAssignableFrom(type) || typeof(IEnumerable<BookDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }
    }
}
