using adminApp.Data;
using adminApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace adminApp.Controllers;
public class TimesheetController : Controller
{
    private readonly AdminDbContext _dbContext;
    public TimesheetController(AdminDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var timesheets = _dbContext.Timesheets.ToList();
        return View(timesheets);
    }


    [HttpGet]
    public ActionResult Create()
    {
        return View("CreateOrEdit", new Timesheet());
    }

    [HttpPost]
    public ActionResult Create(Timesheet timesheet)
    {
        if (ModelState.IsValid)
        {
            // Add the new timesheet to the context
            _dbContext.Timesheets.Add(timesheet);
            _dbContext.SaveChanges(); // Save changes to the database
            TempData["SuccessMessage"] = "Timesheet created successfully."; // Set success message

            return RedirectToAction("Index"); // Redirect to the timesheet list
        }

        return View("CreateOrEdit", timesheet);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        // Find the existing timesheet by ID
        var timesheet = _dbContext.Timesheets.Find(id);
        if (timesheet == null)
        {
            return NotFound(); // Return 404 if not found
        }

        return View("CreateOrEdit", timesheet); // Return the view with the existing timesheet data
    }

    [HttpPost]
    public IActionResult Edit(Timesheet timesheet)
    {
        if (ModelState.IsValid)
        {
            // Update the existing timesheet in the context
            _dbContext.Timesheets.Update(timesheet);
            _dbContext.SaveChanges(); // Save changes to the database
            TempData["SuccessMessage"] = "Timesheet created successfully."; // Set success message

            return RedirectToAction("Index"); // Redirect to the timesheet list
        }

        return View("CreateOrEdit", timesheet);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var timesheet = _dbContext.Timesheets.Find(id);
        if (timesheet != null)
        {
            _dbContext.Timesheets.Remove(timesheet);
            _dbContext.SaveChanges();
            TempData["SuccessMessage"] = "Timesheet deleted successfully."; // Set success message
        }
        return RedirectToAction("Index");
    }
}
