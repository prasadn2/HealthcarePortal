using AutoMapper;
using HealthcarePortal.DTOs;
using HealthcarePortal.Models;
using HealthcarePortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthcarePortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public PatientController(
            IPatientRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET all patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponseDTO>>>
            GetAllPatients()
        {
            var patients = await _repository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<PatientResponseDTO>>(patients);
            return Ok(response);
        }

        // GET single patient
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientResponseDTO>> GetPatient(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
                return NotFound($"Patient with ID {id} not found");

            var response = _mapper.Map<PatientResponseDTO>(patient);
            return Ok(response);
        }

        // POST create patient
        [HttpPost]
        public async Task<ActionResult<PatientResponseDTO>>
            CreatePatient(CreatePatientDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to model
            var patient = _mapper.Map<Patient>(dto);

            var created = await _repository.CreateAsync(patient);

            // Map model to response DTO
            var response = _mapper.Map<PatientResponseDTO>(created);

            return CreatedAtAction(nameof(GetPatient),
                new { id = response.Id }, response);
        }

        // PUT update patient
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient(
            int id, UpdatePatientDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to model
            var patient = _mapper.Map<Patient>(dto);

            var result = await _repository.UpdateAsync(id, patient);
            if (!result)
                return NotFound($"Patient with ID {id} not found");

            return NoContent();
        }

        // DELETE patient
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound($"Patient with ID {id} not found");

            return NoContent();
        }
    }
}