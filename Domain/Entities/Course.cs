using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Course : AggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string Instructor { get; private set; }
        public DateTime? Date { get; private set; }
        public Modality? Modality { get; private set; }
        public Money? Price { get; private set; }
        public CourseType Type { get; private set; }
        public CourseStatus Status { get; private set; }
        public string? Link { get; private set; }

        // EF Core parameterless constructor
        private Course() { }

        public Course(Guid id, string title, string description, string image, string instructor, CourseType type, CourseStatus status)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required", nameof(title));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required", nameof(description));
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Image is required", nameof(image));
            if (string.IsNullOrWhiteSpace(instructor)) throw new ArgumentException("Instructor is required", nameof(instructor));
            if (title.Length > 200) throw new ArgumentException("Title cannot exceed 200 characters", nameof(title));
            if (description.Length > 1000) throw new ArgumentException("Description cannot exceed 1000 characters", nameof(description));

            Id = id;
            Title = title;
            Description = description;
            Image = image;
            Instructor = instructor;
            Type = type;
            Status = status;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Schedule(DateTime date, Modality modality, Money price, string? link)
        {
            if (date < DateTime.UtcNow) throw new ArgumentException("Date cannot be in the past", nameof(date));
            if (price == null) throw new ArgumentNullException(nameof(price));

            Date = date;
            Modality = modality;
            Price = price;
            Link = link;
            MarkAsModified();
        }

        public void Update(string title, string description, string image, string instructor, CourseType type, CourseStatus status)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required", nameof(title));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required", nameof(description));
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Image is required", nameof(image));
            if (string.IsNullOrWhiteSpace(instructor)) throw new ArgumentException("Instructor is required", nameof(instructor));
            if (title.Length > 200) throw new ArgumentException("Title cannot exceed 200 characters", nameof(title));
            if (description.Length > 1000) throw new ArgumentException("Description cannot exceed 1000 characters", nameof(description));

            Title = title;
            Description = description;
            Image = image;
            Instructor = instructor;
            Type = type;
            Status = status;
            MarkAsModified();
        }

    }
}
