using System.Diagnostics;
using espacioProducto;
using espacioProductoRepository;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using tl2_tp6_2024_GonzaSanPla.Models;

namespace tl2_tp6_2024_GonzaSanPla.Controllers;

public class ProductoController : Controller
{
    private readonly ILogger<HomeController> _logger;
    ProductoRepository productoRepository= new ProductoRepository();

    public ProductoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Producto>> Index()
    {
        return View(productoRepository.ListarProductos());
    }
    [HttpGet]
    public IActionResult CrearProducto()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearProducto(Producto producto)
    {
        productoRepository.CrearNuevo(producto);
        return RedirectToAction("Index");
    }    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
