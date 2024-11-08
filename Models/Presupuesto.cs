using espacioPresupuestosDetalle;
using espacioProducto;
using System.Linq;

namespace espacioPresupuestos;

public class Presupuesto
{
    private int idPresupuesto;
    private string? nombreDestinatario;
    private string? fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    const int IVA = 21;
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string? NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; }
    public string? FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }

    public Presupuesto()
    {
        detalle = new List<PresupuestoDetalle>();
    }
    public void AgregarProducto(Producto nuevoProducto, int cantidadProducto)
    {
        PresupuestoDetalle nuevoPresupuestoDetalle = new PresupuestoDetalle();
        nuevoPresupuestoDetalle.CargarProducto(nuevoProducto);
        nuevoPresupuestoDetalle.Cantidad = cantidadProducto;

    }
    public int MontoPresupuesto()
    {
        int monto = 0;
        foreach (PresupuestoDetalle detalle in Detalle)
        {
            monto += detalle.Producto.Precio * detalle.Cantidad;
        }

        return monto;
    }
    public int MontoPresupuestoConIVA()
    {
        int monto = 0;
        foreach (PresupuestoDetalle detalle in Detalle)
        {
            monto += detalle.Producto.Precio * detalle.Cantidad * IVA;
        }

        return monto;
    }

    public int CantidadProductos()
    {
        return Detalle.Count();
    }
    public void CargarDetallesPresupuesto(List<PresupuestoDetalle> presupuestoDetalles)
    {
        detalle=presupuestoDetalles;
    }
}