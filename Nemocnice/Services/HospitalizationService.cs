using Microsoft.EntityFrameworkCore;
using Nemocnice.DTO;
using Nemocnice.DTOs;
using Nemocnice.Models;
//using static Nemocnice.DTOs.HospitalizationDto;

namespace Nemocnice.Services
{
	public class HospitalizationService
	{
		private DbContext dbContext;
		public HospitalizationService(DbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<bool> Create(HospitalizationDto hospitalizationDto)
		{
			var entity = Map(hospitalizationDto);

			var createdEntity = await dbContext.Hospitalizations.AddAsync(entity);
			dbContext.SaveChanges();

			if (createdEntity == null)
				return false;

			return true;
		}

		private Hospitalization Map(HospitalizationDto hospitalizationDto)
		{
			
			return new Hospitalization
			{
				FromDate = hospitalizationDto.FromDate,
				ToDate = hospitalizationDto.ToDate,
				Procedure = hospitalizationDto.ProcedureId,
				DoctorId = hospitalizationDto.DoctorId,
				PatientId = hospitalizationDto.PatientId,
			};
		}
		private HospitalizationDto Map(Hospitalization hospitalization)
		{
			var fullNameDoc = hospitalization?.Doctor?.FirstName + " " + hospitalization?.Doctor?.LastName;
			var fullNamePat = hospitalization?.Patient?.FirstName + " " + hospitalization?.Patient?.LastName;

			return new HospitalizationDto
			{
				Id = hospitalization.Id,
				FromDate = hospitalization.FromDate,
				ToDate = hospitalization.ToDate,
                ProcedureId = hospitalization.Procedure,
				DoctorId = hospitalization.DoctorId,
				PatientId = hospitalization.PatientId,
				FullNameDoc = fullNameDoc,
				FullNamePat = fullNamePat,
			};
		}

		public bool Delete(int id)
		{
			var entity = dbContext.Hospitalizations.Find(id);

			if (entity != null)
			{
				var deletedEntity = dbContext.Hospitalizations.Remove(entity);

				dbContext.SaveChanges();
				return true;

			}
			return false;
		}
		public bool Edit(HospitalizationDto model)
		{
			if (model == null || !model.Id.HasValue)
				return false;

			var dbEntity = dbContext.Hospitalizations.Find(model.Id.Value);
			if (dbEntity == null)
				return false;

			dbEntity.FromDate = model.FromDate;
			dbEntity.ToDate = model.ToDate;
			dbEntity.Procedure = model.ProcedureId;
			dbEntity.DoctorId = model.DoctorId;
			dbEntity.PatientId = model.PatientId;

			var editedEntity = dbContext.Hospitalizations.Update(dbEntity);


			dbContext.SaveChanges();
			return true;
		}
		public HospitalizationDto Get(int id)
		{
			var toEdit = dbContext.Hospitalizations.Find(id);

			return Map(toEdit);
		}
		public IEnumerable<HospitalizationDto> GetAll()
		{
			var allHospitalizations = dbContext.Hospitalizations.Include(x=> x.Doctor).Include(y=>y.Patient).ToList();
			var getAllHospitalizations = new List<HospitalizationDto>();
			foreach (var hospitalization in allHospitalizations)
			{
				var entity = Map(hospitalization);
				getAllHospitalizations.Add(entity);
			}

			return getAllHospitalizations;
		}
	}
}

