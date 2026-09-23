using HealthcarePortal.Data;
using HealthcarePortal.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcarePortal.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        // DbContext injected via DI
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET all patients
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        // GET patient by ID
        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        // CREATE patient
        public async Task<Patient> CreateAsync(Patient patient)
        {
            patient.RegisteredOn = DateTime.UtcNow;
            patient.IsActive = true;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        // UPDATE patient
        public async Task<bool> UpdateAsync(int id, Patient updatedPatient)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) return false;

            patient.Name = updatedPatient.Name;
            patient.Email = updatedPatient.Email;
            patient.Phone = updatedPatient.Phone;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Department = updatedPatient.Department;

            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE patient (soft delete)
        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) return false;

            // Soft delete — just mark inactive
            patient.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // CHECK if patient exists
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Patients
                .AnyAsync(p => p.Id == id && p.IsActive);
        }
    }
}