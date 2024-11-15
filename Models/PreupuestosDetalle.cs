using espacioProducto;

namespace espacioPresupuestosDetalle;

public class PresupuestoDetalle
{
    private Producto? producto;
    private int cantidad;

    private int idPresupuesto;

    public int Cantidad { get => cantidad; set => cantidad = value; }
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public Producto? Producto { get => producto; set => producto = value; }

    public PresupuestoDetalle()
    {
    }
    
    public void CargarProducto(Producto nuevoProducto)
    {
        Producto=nuevoProducto;
    }
}