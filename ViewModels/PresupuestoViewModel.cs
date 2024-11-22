namespace espacioPresupuestoViewModel;

using espacioPresupuestos;
using espacioCliente;
using espacioProducto;
using espacioPresupuestosDetalle;

public class PresupuestoListaCliente
{
    private Presupuesto presupuesto;
    private List<Cliente> ?listaClientes;

    public PresupuestoListaCliente(){}
    public PresupuestoListaCliente(Presupuesto presu,List<Cliente> clientes)
    {
        this.presupuesto=presu;
        listaClientes=clientes;        
    }

    public Presupuesto Presupuesto { get => presupuesto; set => presupuesto = value; }
    public List<Cliente>? ListaClientes { get => listaClientes; set => listaClientes = value; }
}
public class PresupuestoDetalleListaProducto
{
    private PresupuestoDetalle presupuestoDetalle;
    private List<Producto> ?listaProducto;

    public PresupuestoDetalleListaProducto(){}
    public PresupuestoDetalleListaProducto(PresupuestoDetalle presu,List<Producto> productos)
    {
        this.presupuestoDetalle=presu;
        listaProducto=productos;
    }

    public PresupuestoDetalle PresupuestoDetalle { get => presupuestoDetalle; set => presupuestoDetalle = value; }
    public List<Producto>? ListaProducto { get => listaProducto; set => listaProducto = value; }
}