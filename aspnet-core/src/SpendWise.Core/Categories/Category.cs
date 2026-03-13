using Abp.Domain.Entities.Auditing;

namespace SpendWise.Categories
{
    public class Category : FullAuditedEntity<int>
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }
}
