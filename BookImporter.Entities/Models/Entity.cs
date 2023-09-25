using System.Text.Json.Serialization;

namespace BookImporter.Entities.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public DateTime? DateModified { get; set; } = null;
    }
}