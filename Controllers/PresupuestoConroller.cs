using System.Diagnostics;
using espacioPresupuestos;
using espacioPresupuestoRepository;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using tl2_tp6_2024_GonzaSanPla.Models;

namespace tl2_tp6_2024_GonzaSanPla.Controllers;

public class PresupuestoController : Controller
{
    private readonly ILogger<HomeController> _logger;
    PresupuestoRepository presupuestoRepository = new PresupuestoRepository();

    public PresupuestoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Presupuesto>> Index()
    {
        return View(presupuestoRepository.ListarPresupuestos());
    }
    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearPresupuesto(Presupuesto presupuesto)
    {
        presupuestoRepository.CrearNuevoPresupuesto(presupuesto);
        return RedirectToAction("Index");
    }
}