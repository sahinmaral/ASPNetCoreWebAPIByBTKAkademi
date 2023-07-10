using System.Text.Json;

namespace StoreApp.Entities.Models
{
    public class LogDetail
    {
        public Object? ModelName { get; set; }
        public Object? Controller { get; set; }
        public Object? Action { get; set; }
        public Object? Id { get; set; }
        public Object? CreatedAt { get; set; }
        public LogDetail()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
