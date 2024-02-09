using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;
using System.Diagnostics;

namespace StudentPortal.Web.Controllers
{
	public class StudentsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public StudentsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel viewModel)
		{
			var student = new Student
			{
				Name = viewModel.Name,
				Email = viewModel.Email,
				Phone = viewModel.Phone,
				Subscribed = viewModel.Subscribed
			};

			await _context.AddAsync(student);
			await _context.SaveChangesAsync();

			return RedirectToAction("List");
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var students = await _context.Students.ToListAsync();
			return View(students);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var student = await _context.Students.FindAsync(id);
			return View(student);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Student model)
		{
			var student = await _context.Students.FindAsync(model.Id);

			if(student is not null)
			{
				student.Name = model.Name;
				student.Email = model.Email;
				student.Phone = model.Phone;
				student.Subscribed = model.Subscribed;

				await _context.SaveChangesAsync();
			}

			return RedirectToAction("List", "Students");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Student model)
		{
			var student = await _context.Students.FindAsync(model.Id);

			if(student is not null)
			{
			   _context.Students.Remove(student);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction("List", "Students");
		}
    }
}
