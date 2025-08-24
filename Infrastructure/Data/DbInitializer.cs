using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Categorias
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new[]
                {
                new Category(Guid.NewGuid(), "Terapia"),
                new Category(Guid.NewGuid(), "Mindfulness"),
                new Category(Guid.NewGuid(), "Cursos")
            });
                await context.SaveChangesAsync();
            }

            // Benefícios
            if (!context.Benefits.Any())
            {
                context.Benefits.AddRange(new[]
                {
                new Benefit(Guid.NewGuid(), "Alívio do Estresse", "Ajuda a reduzir o estresse e a ansiedade"),
                new Benefit(Guid.NewGuid(), "Foco Aprimorado", "Melhora a concentração e a clareza mental"),
                new Benefit(Guid.NewGuid(), "Equilíbrio Emocional", "Auxilia na regulação das emoções"),
                new Benefit(Guid.NewGuid(), "Melhora do Sono", "Promove um sono mais tranquilo"),
                new Benefit(Guid.NewGuid(), "Autoconhecimento", "Estimula a reflexão e o crescimento pessoal")
            });
                await context.SaveChangesAsync();
            }

            // Especialidades
            if (!context.Specialties.Any())
            {
                context.Specialties.AddRange(new[]
                {
                new Specialty(Guid.NewGuid(), "Ansiedade", "Apoio para lidar com crises de ansiedade"),
                new Specialty(Guid.NewGuid(), "Depressão", "Ajuda no tratamento de sintomas depressivos"),
                new Specialty(Guid.NewGuid(), "Autoestima", "Fortalecimento da confiança e valorização pessoal"),
                new Specialty(Guid.NewGuid(), "Trauma", "Processo terapêutico para experiências traumáticas")
            });
                await context.SaveChangesAsync();
            }

            // Terapeutas
            if (!context.Therapists.Any())
            {
                var specialties = context.Specialties.ToList();

                var terapeuta1 = new Therapist(Guid.NewGuid(), "Dra. Ana Souza", "Psicóloga clínica com mais de 10 anos de experiência", "/images/ana.jpg", "10 anos atuando com TCC e trauma", "Doutorado em Psicologia");
                terapeuta1.AddSpecialty(specialties[0]);
                terapeuta1.AddSpecialty(specialties[3]);

                var terapeuta2 = new Therapist(Guid.NewGuid(), "Carlos Lima", "Instrutor de mindfulness e meditação", "/images/carlos.jpg", "8 anos ensinando práticas de atenção plena", "Certificação Internacional em Mindfulness");
                terapeuta2.AddSpecialty(specialties[2]);

                context.Therapists.AddRange(terapeuta1, terapeuta2);
                await context.SaveChangesAsync();
            }

            // Cursos
            if (!context.Courses.Any())
            {
                var curso1 = new Course(Guid.NewGuid(), "Fundamentos do Mindfulness", "Aprenda os princípios básicos da atenção plena", "/images/mindfulness.jpg", "Carlos Lima", CourseType.Curso, CourseStatus.Disponivel);
                curso1.Schedule(DateTime.UtcNow.AddDays(10), Modality.Online, new Money(150, "BRL"), "https://exemplo.com/mindfulness");

                var curso2 = new Course(Guid.NewGuid(), "Superando o Trauma", "Curso guiado para recuperação emocional", "/images/trauma.jpg", "Dra. Ana Souza", CourseType.Curso, CourseStatus.EmBreve);
                curso2.Schedule(DateTime.UtcNow.AddDays(20), Modality.Presencial, new Money(300, "BRL"), null);

                context.Courses.AddRange(curso1, curso2);
                await context.SaveChangesAsync();
            }

            // Serviços
            if (!context.Services.Any())
            {
                var categoria = context.Categories.First(c => c.Name == "Terapia");
                var beneficios = context.Benefits.Take(3).ToList();

                var servico1 = new Service(Guid.NewGuid(), "Respiração Consciente", "Técnica de relaxamento com foco na respiração", "/images/breathing.jpg", new Money(120, "BRL"));
                servico1.UpdateDetails("Exercício respiratório para aliviar ansiedade", "30 minutos");
                servico1.UpdateCategory(categoria);
                beneficios.ForEach(b => servico1.Benefits.Add(b));
                beneficios.ForEach(b => b.Services.Add(servico1));

                var servico2 = new Service(Guid.NewGuid(), "Sessão de Terapia Cognitiva", "Atendimento estruturado para reprogramação de pensamentos", "/images/cbt.jpg", new Money(200, "BRL"));
                servico2.UpdateDetails("Sessão de TCC com terapeuta especializado", "1 hora");
                servico2.UpdateCategory(categoria);
                beneficios.Skip(1).ToList().ForEach(b => servico2.Benefits.Add(b));
                beneficios.Skip(1).ToList().ForEach(b => b.Services.Add(servico2));

                context.Services.AddRange(servico1, servico2);
                await context.SaveChangesAsync();
            }

            // Depoimentos
            if (!context.Testimonials.Any())
            {
                context.Testimonials.AddRange(new[]
                {
                new Testimonial(Guid.NewGuid(), "Juliana Costa", "/images/juliana.jpg", 5, "Esse serviço transformou minha vida!", "Verificado"),
                new Testimonial(Guid.NewGuid(), "Marcelo Tavares", "/images/marcelo.jpg", 4, "Muito profissional e acolhedor."),
                new Testimonial(Guid.NewGuid(), "Fernanda Rocha", "/images/fernanda.jpg", 5, "Recomendo fortemente para quem busca apoio emocional.", "Top Reviewer")
            });
                await context.SaveChangesAsync();
            }
        }

    }
}
