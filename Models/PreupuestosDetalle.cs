using espacioProducto;

namespace espacioPresupuestosDetalle;

public class PresupuestoDetalle
{
    private Producto? producto;
    private int cantidad;

    public Producto? Producto { get => producto;  }
    public int Cantidad { get => cantidad; set => cantidad = value; }
    public PresupuestoDetalle()
    {
    }
    
    public void CargarProducto(Producto nuevoProducto)
    {
        producto=nuevoProducto;
    }
}