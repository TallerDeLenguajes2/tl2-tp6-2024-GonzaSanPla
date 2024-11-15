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
    ProductoRepository productoRepository = new ProductoRepository();

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
    [HttpGet]
    public IActionResult ModificarProducto(int id)
    {
        return View(productoRepository.ObtenerProductoPorId(id));
    }

    [HttpPost]
    public IActionResult ModificarProducto(int id, Producto producto)
    {
        productoRepository.ModificarProducto(id, producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EliminarProducto(int id)
    {
        return View(productoRepository.ObtenerProductoPorId(id));
    }


    // [HttpGet]
    // public IActionResult EliminarProducto(int id,bool confirmar)
    // {
    //     if(confirmar)
    //     {
    //         productoRepository.EliminarProducto(id);
    //     }
    //     return RedirectToAction("Index");
    // }


    // [HttpPost] No va Post porque recibe objeto formulario nomas
    public IActionResult ConfirmarEliminacion(int id)
    {
        productoRepository.EliminarProducto(id);
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
