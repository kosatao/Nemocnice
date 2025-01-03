using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nemocnice.DTO;
using Nemocnice.DTOs;
using Nemocnice.Models;
//using static Nemocnice.DTOs.PatientDto;

namespace Nemocnice.Services
{
	public class PatientService
	{
		private DbContext dbContext;
		public PatientService(DbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<bool> Create(PatientDto patientDto)
		{
			var entity = Map(patientDto);

			var createdEntity = await dbContext.Pacients.AddAsync(entity);
			dbContext.SaveChanges();

			if (createdEntity == null)
				return false;

			return true;
		}

		private Patient Map(PatientDto patientDto)
		{
			return new Patient
			{
				FirstName = patientDto.Name,
				LastName = patientDto.SureName,
				Email = patientDto.Email,
			};
		}
		private PatientDto Map(Patient patient)
		{
			return new PatientDto
			{
				Id = patient.Id,
				Name = patient.FirstName,
				SureName = patient.LastName,
				Email = patient.Email,
			};
		}
		public bool Delete(int id)
		{
			var entity = dbContext.Pacients.Find(id);

			if (entity != null)
			{
				var deletedEntity = dbContext.Pacients.Remove(entity);

				dbContext.SaveChanges();
				return true;

			}
			return false;
		}
		public bool Edit(PatientDto model)
		{
			if (model == null || !model.Id.HasValue)
				return false;

			var dbEntity = dbContext.Pacients.Find(model.Id.Value);
			if (dbEntity == null)
				return false;

			dbEntity.FirstName = model.Name;
			dbEntity.LastName = model.SureName;
			dbEntity.Email = model.Email;

			var editedEntity = dbContext.Pacients.Update(dbEntity);


			dbContext.SaveChanges();
			return true;
		}
		public PatientDto Get(int id)
		{
			var toEdit = dbContext.Pacients.Find(id);

			return Map(toEdit);
		}
		public IEnumerable<PatientDto> GetAll()
		{
			var allPatients = dbContext.Pacients.ToList();
			var getAllPatients = new List<PatientDto>();
			foreach (var patient in allPatients)
			{
				var entity = Map(patient);
				getAllPatients.Add(entity);
			}

			return getAllPatients;
		}
		public IEnumerable<SelectListItem> GetAllSelect()
		{
			var data = dbContext.Pacients.ToList();
			var result = new List<SelectListItem>();
			foreach (var patient in data)
			{
				result.Add(new SelectListItem(patient.LastName, patient.Id.ToString()));
			}

			return result;
		}
	}
}
