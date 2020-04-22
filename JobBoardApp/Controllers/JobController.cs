using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobBoardApp.Models;
using JobBoardApp_Infrastructure;
using JobBoardApp_Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardApp.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        // GET: Job
        public IActionResult Index()
        {
            return View();
        }

        // GET: Job/Create
        public IActionResult Create()
        {
            ViewBag.PageTitle = "Create Job";
            return View("NewEdit", new JobViewModel());
        }

        // POST: Job/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobViewModel job)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    ViewBag.Error = "Required data is missing!";
                    return View("NewEdit", job);
                }
                _jobRepository.CreateJob(new JobDTO {
                    Title = job.Title,
                    Description = job.Description,
                    ExpiresAt = job.ExpiresAt
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("NewEdit", job);
            }
        }

        // GET: Job/Edit/5
        public IActionResult Edit(int id)
        {
            try 
            {
                var job = _jobRepository.GetById(id);
                return View("NewEdit", new JobViewModel
                {
                    Id = job.Id,
                    Title = job.Title,
                    Description = job.Description,
                    ExpiresAt = job.ExpiresAt
                });
            }
            catch (Exception ex) 
            {
                ViewBag.Error = ex.Message;
                return View("NewEdit", new JobViewModel());
            }
            
        }

        // POST: Job/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, JobViewModel job)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Job/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult GetJobPaginated([FromQuery]int? draw, [FromQuery] int? start, [FromQuery] int? length, [FromQuery] string search, [FromQuery] int? page, [FromQuery] string order = "desc")
        {
            search = search ?? Request.Query["search[value]"].ToString();
            order = Request.Query["order[0][dir]"].ToString() ?? order;
            length = length ?? 10;
            var totalRecords = 0;
            var recordsFiltered = 0;
            start = start.HasValue ? start / length : 0;
            start = page.HasValue ? page - 1 : start;

            try
            {
                var list = _jobRepository.GetPaginated(search, start.Value, length.Value, order, out totalRecords, out recordsFiltered);

                return new JsonResult(
                    new { draw, start, length, totalRecords, recordsFiltered, data = list }
                    );
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}