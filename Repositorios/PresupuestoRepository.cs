using espacioPresupuestos;
using espacioPresupuestosDetalle;
using espacioProducto;
using espacioProductoRepository;
using espacioClienteRepository;
using Microsoft.Data.Sqlite;

namespace espacioPresupuestoRepository;
public class PresupuestoRepository
{
    string cadenaConexion = @"Data Source=Tienda.db;Cache=Shared";
    public void CrearNuevoPresupuesto(Presupuesto pres)        // Que tipo es fecha? Porque en la BD es un string 
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "INSERT INTO Presupuestos (ClienteId, FechaCreacion) VALUES (@CliId, @Fecha)";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@CliId", pres.Cliente.ClienteId));
            command.Parameters.Add(new SqliteParameter("@Fecha", pres.FechaCreacion));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void CrearNuevoDetalle(PresupuestoDetalle detalles)  // El idProducto mando como int o mando el producto o lo mando dentro de detalle? Se qchequea que el producto existe?
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto,Cantidad ) VALUES (@idPres,@idProd,@Cantidad)";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@idPres", detalles.IdPresupuesto));
            command.Parameters.Add(new SqliteParameter("@idProd", detalles.Producto.IdProducto));
            command.Parameters.Add(new SqliteParameter("@Cantidad", detalles.Cantidad));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<Presupuesto> ListarPresupuestos()
    {
        List<Presupuesto> listaPres = new List<Presupuesto>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Presupuestos;";
            var clienteRepository=  new ClienteRepository();
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var presupuesto = new Presupuesto();
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.Cliente = clienteRepository.ObetnerClientePorId(Convert.ToInt32(reader["ClienteId"]));
                    presupuesto.FechaCreacion = reader["FechaCreacion"].ToString();
                    presupuesto.CargarDetallesPresupuesto(ListarDetallePresupuesto(Convert.ToInt32(reader["idPresupuesto"])));
                    listaPres.Add(presupuesto);
                }
            }
            connection.Close();

        }
        return listaPres;
    }

    public List<PresupuestoDetalle> ListarDetallePresupuesto(int idPresupuesto)
    {
        List<PresupuestoDetalle> listaDetalle = new List<PresupuestoDetalle>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM PresupuestosDetalle INNER JOIN Productos USING(idProducto) WHERE  idPresupuesto=@idPres";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SqliteParameter("@idPres", idPresupuesto));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Producto producto= new Producto();  
                    var presupuestoDetalle = new PresupuestoDetalle();
                    var productoRepository= new ProductoRepository();
                    presupuestoDetalle.Cantidad= Convert.ToInt32(reader["Cantidad"]);
                    producto.IdProducto= (Convert.ToInt32(reader["idProducto"]));
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    presupuestoDetalle.CargarProducto(producto);
                    listaDetalle.Add(presupuestoDetalle);
                }
            }
            connection.Close();

        }
        return listaDetalle;
    }
    public Presupuesto ObtenerPresupuestoPorId(int id)
    {
        Presupuesto pres = new Presupuesto();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Presupuestos WHERE idPresupuesto=@id;";
            SqliteCommand command = new SqliteCommand(query, connection);
            var clienteRepository=  new ClienteRepository();
            connection.Open();
            command.Parameters.Add(new SqliteParameter("@id", id));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    pres.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    pres.Cliente = clienteRepository.ObetnerClientePorId(Convert.ToInt32(reader["ClienteId"]));
                    pres.FechaCreacion = reader["FechaCreacion"].ToString();
                }

            }
            connection.Close();

        }
        return pres;
    }
     public void ModificarPresupuesto(int idModificar, Presupuesto nuevoPresupuesto)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "UPDATE Presupuestos SET ClienteId= @ClienteId , FechaCreacion= @FechaCreacion WHERE idPresupuesto=@id ";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@ClienteId", nuevoPresupuesto.Cliente.ClienteId));
            command.Parameters.Add(new SqliteParameter("@FechaCreacion", nuevoPresupuesto.FechaCreacion));
            command.Parameters.Add(new SqliteParameter("@id", idModificar));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
       public void EliminarPresupuesto(int idEliminar)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto=@id"; //Para eliminar de presupuesto detalle y no quede el id vacio
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", idEliminar));
            command.ExecuteNonQuery();
            query = "DELETE FROM Presupuestos WHERE idPresupuesto=@id";   //Para eliminar el Presupuesto
            connection.Open();
            command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", idEliminar));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
