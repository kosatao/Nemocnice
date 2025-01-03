using Nemocnice.DTO;

namespace Nemocnice.DTOs
{
	public class PatientViewDto
	{
		public IEnumerable<PatientDto> AllPatients { get; set; }
	}
}
