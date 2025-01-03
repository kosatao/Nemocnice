using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nemocnice.DTO;
using Nemocnice.DTOs;
using Nemocnice.Models;
using Nemocnice.Services;

namespace Nemocnice.Controllers
{
	[Authorize]
	public class PatientController : Controller
	{
		private PatientService patientService;
		private UserManager<AppUser> userManager;

		public PatientController(PatientService patientService, UserManager<AppUser> userManager)
		{
			this.patientService = patientService;
			this.userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			//AppUser appUser = new AppUser()
			//{
			//	Email = "kosata@kurz.cz",
			//	UserName = "Neadmin",

			//};
			//await userManager.CreateAsync(appUser, "Abcd1234.");
			var allPatients = patientService.GetAll();
			var model = new PatientViewDto { AllPatients = allPatients };
			return View(model);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(PatientDto model)
		{

			await patientService.Create(model);

			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			patientService.Delete(id);

			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var model = patientService.Get(id);
			return View("Create", model);
		}
		[HttpPost]
		public IActionResult Update(PatientDto model)
		{
			var isSuccess = patientService.Edit(model);
			return RedirectToAction("Index");
		}
	}
}
