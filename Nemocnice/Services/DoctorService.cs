using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nemocnice.DTO;
using Nemocnice.Models;

namespace Nemocnice.Services
{
	public class DoctorService
	{
		private DbContext dbContext;
		public DoctorService(DbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<bool> Create(DoctorDto doctorDto)
		{
			var entity = Map(doctorDto);

			var createdEntity = await dbContext.Doctors.AddAsync(entity);
			dbContext.SaveChanges();

			if (createdEntity == null)
				return false;

			return true;
		}

		private Doctor Map(DoctorDto doctorDto)
		{
			return new Doctor
			{
				FirstName = doctorDto.Name,
				LastName = doctorDto.SureName,
				Specialization = doctorDto.Specialization,
			};
		}
		private DoctorDto Map(Doctor doctor)
		{
			var specs = GetSpec();
			return new DoctorDto
			{
				Id = doctor.Id,
				Name = doctor.FirstName,
				SureName = doctor.LastName,
				Specialization = doctor.Specialization,
				SpecializationStr= specs.First(x =>x.Value==doctor.Specialization.ToString()).Text, 
			};
		}

		public bool Delete(int id)
		{
			var entity = dbContext.Doctors.Find(id);

			if (entity != null)
			{
				var deletedEntity = dbContext.Doctors.Remove(entity);

				dbContext.SaveChanges();
				return true;

			}
			return false;
		}
		public bool Edit(DoctorDto model)
		{
			if (model == null || !model.Id.HasValue)
				return false;

			var dbEntity = dbContext.Doctors.Find(model.Id.Value);
			if (dbEntity == null)
				return false;

			dbEntity.FirstName = model.Name;
			dbEntity.LastName = model.SureName;
			dbEntity.Specialization = model.Specialization;

			var editedEntity = dbContext.Doctors.Update(dbEntity);


			dbContext.SaveChanges();
			return true;
		}
		public DoctorDto Get(int id)
		{
			var data = dbContext.Doctors.Find(id);
			var mapped = Map(data);

			mapped.AllSpecializations = GetSpec();


			return mapped;
		}
		public IEnumerable<DoctorDto> GetAll()
		{
			var allDoctors = dbContext.Doctors.ToList();
			var getAllDoctors = new List<DoctorDto>();
			foreach (var doctor in allDoctors)
			{
				var entity = Map(doctor);
				getAllDoctors.Add(entity);
			}

			return getAllDoctors;
		}
		public IEnumerable<SelectListItem> GetAllSelect()
		{
			var hospitalizations = dbContext.Hospitalizations.ToList();
			var data = dbContext.Doctors.ToList();
			var result = new List<SelectListItem>();
			foreach (var doctor in data)
			{
				if (!hospitalizations.Any(x => x.DoctorId == doctor.Id))
					result.Add(new SelectListItem(doctor.LastName, doctor.Id.ToString()));
			}

			return result;

		}
		public IEnumerable<SelectListItem> GetSpec()
		{
			var pacs = new List<SelectListItem>()
			{
				new SelectListItem("Chirurg","1"),
				new SelectListItem("Kardiolog","2"),
				new SelectListItem("Fyzioterapeut","3"),
				new SelectListItem("Imunolog","4"),
			};
			return pacs;
		}





		//public ResponseDto Delete(int id)
		//{
		//    var entity = dbContext.Doctors.Find(id);

		//    if (entity != null)
		//    {
		//        var deletedEntity = dbContext.Doctors.Remove(entity);

		//        dbContext.SaveChanges();
		//        return new ResponseDto
		//        {
		//            IsSuccess = true,
		//            Message = "smazano good",
		//            HtmlStatusCode = 200
		//        };

		//    }
		//    return new ResponseDto
		//    {
		//        IsSuccess = true,
		//        Message = "smazano good",
		//        HtmlStatusCode = 200
		//    };
		//}
	}
}
