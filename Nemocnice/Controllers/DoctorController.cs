using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nemocnice.DTO;
using Nemocnice.DTOs;
using Nemocnice.Services;

namespace Nemocnice.Controllers
{
	[Authorize]
	public class DoctorController : Controller
	{
		private DoctorService _service;

		public DoctorController(DoctorService service)
		{
			_service = service;
		}

		public IActionResult Index()
		{
			var allDoctors = _service.GetAll();
			var model = new DoctorViewDto { AllDoctors = allDoctors };
			return View(model);
		}

		//[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Create()
		{
			var allSpecialization = _service.GetSpec();
			var model = new DoctorDto { AllSpecializations = allSpecialization };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(DoctorDto model)
		{
			if (!ModelState.IsValid)
			{
				model.AllSpecializations = _service.GetSpec();
				return View(model);
			}

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
			
			var data = _service.Get(id);
			return View("Create", data);
		}

		[HttpPost]
		public IActionResult Update(DoctorDto model)
		{
			var isSuccess = _service.Edit(model);
			return RedirectToAction("Index");
		}
	}
}
