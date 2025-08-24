﻿using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITherapistService
    {
        Task<List<TherapistDto>> GetAllAsync();
        Task<TherapistDto?> GetByIdAsync(Guid id);
        Task<List<TherapistDto>> FindBySpecialtyAsync(string specialtyName);
        Task<TherapistDto> CreateAsync(TherapistDto dto);
        Task<bool> UpdateAsync(Guid id, TherapistDto dto);
        Task<bool> DeleteAsync(Guid id);

    }
}
