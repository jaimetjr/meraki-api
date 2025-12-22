using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Therapist : AggregateRoot
    {
        public string Name { get; private set; }
        public string Bio { get; private set; }
        public string Image { get; private set; }
        public string Experience { get; private set; }
        public string Education { get; private set; }
        public List<Specialty> Specialties { get; private set; } = new();

        // EF Core parameterless constructor
        private Therapist() { }

        public Therapist(Guid id, string name, string bio, string image, string experience, string education)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
            if (string.IsNullOrWhiteSpace(bio)) throw new ArgumentException("Bio is required", nameof(bio));
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Image is required", nameof(image));
            if (string.IsNullOrWhiteSpace(experience)) throw new ArgumentException("Experience is required", nameof(experience));
            if (string.IsNullOrWhiteSpace(education)) throw new ArgumentException("Education is required", nameof(education));
            if (name.Length > 100) throw new ArgumentException("Name cannot exceed 100 characters", nameof(name));
            if (bio.Length > 500) throw new ArgumentException("Bio cannot exceed 500 characters", nameof(bio));

            Id = id;
            Name = name;
            Bio = bio;
            Image = image;
            Experience = experience;
            Education = education;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddSpecialty(Specialty specialty)
        {
            if (specialty == null) throw new ArgumentNullException(nameof(specialty));
            if (!Specialties.Any(x => x.Id == specialty.Id))
            {
                Specialties.Add(specialty);
                MarkAsModified();
            }
        }

        public void Update(string name, string bio, string image, string experience, string education)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
            if (string.IsNullOrWhiteSpace(bio)) throw new ArgumentException("Bio is required", nameof(bio));
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Image is required", nameof(image));
            if (string.IsNullOrWhiteSpace(experience)) throw new ArgumentException("Experience is required", nameof(experience));
            if (string.IsNullOrWhiteSpace(education)) throw new ArgumentException("Education is required", nameof(education));
            if (name.Length > 100) throw new ArgumentException("Name cannot exceed 100 characters", nameof(name));
            if (bio.Length > 500) throw new ArgumentException("Bio cannot exceed 500 characters", nameof(bio));

            Name = name;
            Bio = bio;
            Image = image;
            Experience = experience;
            Education = education;
            MarkAsModified();
        }
    }
}
