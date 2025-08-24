﻿using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TherapistsController : ControllerBase
    {
        private readonly ITherapistService _therapistService;
        private readonly IValidator<TherapistDto> _validator;

        public TherapistsController(ITherapistService therapistService, IValidator<TherapistDto> validator)
        {
            _therapistService = therapistService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _therapistService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _therapistService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("specialty/{name}")]
        public async Task<IActionResult> FindBySpecialty(string name)
        {
            var result = await _therapistService.FindBySpecialtyAsync(name);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TherapistDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            var created = await _therapistService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TherapistDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            var success = await _therapistService.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _therapistService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
