using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }

        public Category()
        {
            
        }
        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
