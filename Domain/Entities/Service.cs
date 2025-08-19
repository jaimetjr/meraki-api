using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public sealed class Service : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }
    public string? LongDescription { get; private set; }
    public List<Benefit> Benefits { get; private set; } = new();
    public string? Duration { get; private set; }
    public Money Price { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public Service()
    {
        
    }

    public Service(Guid id, string name, string description, string image, Money money)
    {
        Id = id;
        Name = name;
        Description = description;
        Image = image;
        Price = money;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string? longDescription, string? duration)
    {
        LongDescription = longDescription;
        Duration = duration;
    }

    public void AssignCategory(Category category) => Category = category;

    public void AddBenefit(Benefit benefit)
    {
        if (!Benefits.Any(b => b.Id == benefit.Id))
            Benefits.Add(benefit);
    }


}
