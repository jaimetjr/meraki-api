using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Course : Entity
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

        public Course()
        {
            
        }
        public Course(Guid id, string title, string description, string image, string instructor, CourseType type, CourseStatus status)
        {
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
            Date = date;
            Modality = modality;
            Price = price;
            Link = link;
        }

    }
}
