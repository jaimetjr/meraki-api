using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Benefit : Entity
    {
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public List<Service> Services { get; private set; } = new();

        public Benefit()
        {
            
        }

        public Benefit(Guid id, string title, string? description = null)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
