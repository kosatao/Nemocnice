using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Nemocnice.DTO
{
    public class DoctorDto
    {
        public int? Id { get; set; }


		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }


		[Required(ErrorMessage = "SureName is required")]
		public string SureName { get; set; }


		[Required(ErrorMessage = "Specialization is required")]
		public int Specialization { get; set; }


		public string SpecializationStr { get; set; }
		public IEnumerable<SelectListItem> AllSpecializations { get; set; }
	}
}
