using System.Diagnostics;
using espacioPresupuestos;
using espacioProducto;
using espacioPresupuestoRepository;
using espacioProductoRepository;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using tl2_tp6_2024_GonzaSanPla.Models;
using espacioPresupuestosDetalle;
using espacioClienteRepository;
using espacioPresupuestoViewModel;

namespace tl2_tp6_2024_GonzaSanPla.Controllers;

public class PresupuestoController : Controller
{
    private readonly ILogger<HomeController> _logger;
    PresupuestoRepository presupuestoRepository = new PresupuestoRepository();
    ProductoRepository productoRepository = new ProductoRepository();

    ClienteRepository clienteRepository= new ClienteRepository();
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
        Presupuesto pres= new Presupuesto();
        PresupuestoListaCliente viewPres= new PresupuestoListaCliente(pres,clienteRepository.ListarCliente());
        return View( viewPres);
    }

    [HttpPost]
    public IActionResult CrearPresupuesto(PresupuestoListaCliente viewPresupuesto)
    {
        presupuestoRepository.CrearNuevoPresupuesto(viewPresupuesto.Presupuesto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarPresupuesto(int id)
    {
        Presupuesto pres= presupuestoRepository.ObtenerPresupuestoPorId(id);
        PresupuestoListaCliente viewPres= new PresupuestoListaCliente(pres,clienteRepository.ListarCliente());
        return View(viewPres);
    }

    [HttpPost]
    public IActionResult ModificarPresupuesto(int id, PresupuestoListaCliente presupuestoView)
    {
        presupuestoRepository.ModificarPresupuesto(id, presupuestoView.Presupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EliminarPresupuesto(int id)
    {
        return View(presupuestoRepository.ObtenerPresupuestoPorId(id));
    }

    public IActionResult ConfirmarEliminacion(int id)
    {
        presupuestoRepository.EliminarPresupuesto(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult MostrarPresupuestoDetalle(int id)
    {
        return View(presupuestoRepository.ListarDetallePresupuesto(id));
    }
    
    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        PresupuestoDetalle presupuestoDetalle=new PresupuestoDetalle();
        presupuestoDetalle.IdPresupuesto=id;
        PresupuestoDetalleListaProducto viewPres= new PresupuestoDetalleListaProducto(presupuestoDetalle,productoRepository.ListarProductos());
        return View(viewPres);
    }

    [HttpPost]
    public IActionResult AgregarProducto(PresupuestoDetalle presupuestoDetalle)
    {
        presupuestoRepository.CrearNuevoDetalle(presupuestoDetalle);
        return RedirectToAction("Index");
    }
}
