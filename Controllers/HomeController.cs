using Microsoft.AspNetCore.Mvc;
using RichCRUDelicious.Models;

namespace RichCRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.AllDishes = _context.Dishes.ToList();
        return View();
    }

    [HttpGet("dish/new")]
    public IActionResult NewDish()
    {
        ViewBag.Button = "Add";
        return View("DishForm");
    }
    [HttpGet("dish/{id}/edit")]
    public IActionResult EditDish(string id)
    {
        ViewBag.Button = "Edit";
        Dish? DishToEdit = _context.Dishes.FirstOrDefault(d => d.DishId == Int32.Parse(id));
        return View("DishForm", DishToEdit);
    }

    [HttpGet("dish/{id}")]
    public IActionResult ViewDish(string id)
    {
        Dish? DishToView = _context.Dishes.FirstOrDefault(d => d.DishId == Int32.Parse(id));
        return View("ViewDish", DishToView);
    }

    [HttpPost("dish/{operation}")]
    public IActionResult Dish(string operation, Dish dish)
    {
        //Redirection/Delete
        if (operation == "RedEdit")
        {
            System.Console.WriteLine(dish.DishId);
            return Redirect($"/dish/{dish.DishId}/edit");
        }
        else if (operation == "Delete")
        {
            _context.Dishes.Remove(dish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //Add Form
        if (operation == "Add")
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View("DishForm");
        }
        //Edit Form
        else
        {
            if (ModelState.IsValid)
            {
                Dish? OldDish = _context.Dishes.FirstOrDefault(d => d.DishId == dish.DishId);
                OldDish.Chef = dish.Chef;
                OldDish.Name = dish.Name;
                OldDish.Calories = dish.Calories;
                OldDish.Tastiness = dish.Tastiness;
                OldDish.Description = dish.Description;
                OldDish.UpdatedAt = dish.UpdatedAt;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View("DishForm", dish);
        }
    }
}
