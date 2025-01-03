using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nemocnice.DTOs
{
	public class HospitalizationDto
	{
		public int? Id { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public int ProcedureId { get; set; }

		public int DoctorId { get; set; }
		public int PatientId { get; set; }
        public string FullNameDoc { get; set; }
		public string FullNamePat { get; set; }

		public IEnumerable<SelectListItem> AllDoctors { get; set; }
		public IEnumerable<SelectListItem> AllPatients { get; set; }
	}

}
