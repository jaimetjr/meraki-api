using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Therapist : Entity
    {
        public string Name { get; private set; }
        public string Bio { get; private set; }
        public string Image { get; private set; }
        public string Experience { get; private set; }
        public string Education { get; private set; }
        public List<Specialty> Specialties { get; private set; } = new();

        public Therapist()
        {
            
        }

        public Therapist(Guid id, string name, string bio, string image, string experience, string education)
        {
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
            if (!Specialties.Any(x => x.Id == specialty.Id))
                Specialties.Add(specialty);
        }
    }
}
