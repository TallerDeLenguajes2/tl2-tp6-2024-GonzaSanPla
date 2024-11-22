namespace espacioPresupuestoViewModel;

using espacioPresupuestos;
using espacioCliente;

public class PresupuestoListaCliente
{
    private Presupuesto presupuesto;
    private List<Cliente> ?listaClientes;

    public List<Cliente>? ListaClientes { get => listaClientes;  }
    public Presupuesto Presupuesto { get => presupuesto; set => presupuesto = value; }

    public PresupuestoListaCliente(Presupuesto presu,List<Cliente> clientes)
    {
        this.presupuesto=presu;
        listaClientes=clientes;        
    }
}