using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public sealed class Service : AggregateRoot
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

    // EF Core parameterless constructor
    private Service() { }

    public Service(Guid id, string name, string description, string image, Money money)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required", nameof(description));
        if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Image is required", nameof(image));
        if (name.Length > 100) throw new ArgumentException("Name cannot exceed 100 characters", nameof(name));
        if (description.Length > 500) throw new ArgumentException("Description cannot exceed 500 characters", nameof(description));

        Id = id;
        Name = name;
        Description = description;
        Image = image;
        Price = money ?? throw new ArgumentNullException(nameof(money));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string? longDescription, string? duration)
    {
        if (longDescription != null && longDescription.Length > 2000) 
            throw new ArgumentException("LongDescription cannot exceed 2000 characters", nameof(longDescription));
        
        LongDescription = longDescription;
        Duration = duration;
        MarkAsModified();
    }

    public void UpdateCategory(Category category)
    {
        if (category == null) throw new ArgumentNullException(nameof(category));
        Category = category;
        CategoryId = category.Id;
        MarkAsModified();
    }

    public void AddBenefit(Benefit benefit)
    {
        if (benefit == null) throw new ArgumentNullException(nameof(benefit));
        if (!Benefits.Any(b => b.Id == benefit.Id))
        {
            Benefits.Add(benefit);
            MarkAsModified();
        }
    }

    public void Update(string name, string description, string image, Money price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required", nameof(description));
        if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Image is required", nameof(image));
        if (name.Length > 100) throw new ArgumentException("Name cannot exceed 100 characters", nameof(name));
        if (description.Length > 500) throw new ArgumentException("Description cannot exceed 500 characters", nameof(description));

        Name = name;
        Description = description;
        Image = image;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        MarkAsModified();
    }


}
