using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nemocnice.DTO;
using Nemocnice.DTOs;
using Nemocnice.Services;

namespace Nemocnice.Controllers
{
	[Authorize]
	public class HospitalizationController : Controller
	{
		private HospitalizationService _service;
		private DoctorService _doctorService;
		private PatientService _patientService;

		public HospitalizationController(HospitalizationService service, DoctorService doctorService, PatientService patientService)
		{
			_service = service;
			_doctorService = doctorService;
			_patientService = patientService;

		}

		public IActionResult Index()
		{
			var allHospitalizations = _service.GetAll();
			var model = new HospitalizationViewDto { AllHospitalizations = allHospitalizations };
			return View(model);
		}

		//[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Create()
		{
			var allDoctors = _doctorService.GetAllSelect();
			var allPatients = _patientService.GetAllSelect();
			var model = new HospitalizationDto { AllDoctors = allDoctors, AllPatients = allPatients };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(HospitalizationDto model)
		{

			await _service.Create(model);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			_service.Delete(id);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var model = _service.Get(id);
			var allDoctors = _doctorService.GetAllSelect();
			var allPatients = _patientService.GetAllSelect();

			model.AllDoctors = allDoctors;
			model.AllPatients = allPatients;

			return View("Create", model);
		}

		[HttpPost]
		public IActionResult Update(HospitalizationDto model)
		{
			var isSuccess = _service.Edit(model);
			return RedirectToAction("Index");
		}
	}
}

