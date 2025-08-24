using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Service ↔ DTO
            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency))
                .ReverseMap()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, src.Currency)));

            // Therapist ↔ DTO
            CreateMap<Therapist, TherapistDto>().ReverseMap();

            // Course ↔ DTO
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Modality, opt => opt.MapFrom(src => src.Modality.ToString()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price != null ? src.Price.Amount : 0))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price != null ? src.Price.Currency : "BRL"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()))
                .ForMember(dest => dest.Modality, opt => opt.MapFrom(src => src.Modality.GetDisplayName()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.GetDisplayName()))
                .ReverseMap()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price ?? 0, src.Currency)));

            // Category ↔ DTO
            CreateMap<Category, CategoryDto>().ReverseMap();

            // Benefit ↔ DTO
            CreateMap<Benefit, BenefitDto>().ReverseMap();

            // Specialty ↔ DTO
            CreateMap<Specialty, SpecialtyDto>().ReverseMap();

            // Testimonial ↔ DTO
            CreateMap<Testimonial, TestimonialDto>().ReverseMap();
        }
    }
}
