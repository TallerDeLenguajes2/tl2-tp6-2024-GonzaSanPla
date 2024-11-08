using espacioPresupuestos;
using espacioPresupuestosDetalle;
using espacioProducto;
using espacioProductoRepository;
using Microsoft.Data.Sqlite;

namespace espacioPresupuestoRepository;
public class PresupuestoRepository
{
    string cadenaConexion = @"Data Source=Tienda.db;Cache=Shared";
    public void CrearNuevoPresupuesto(Presupuesto pres)        // Que tipo es fecha? Porque en la BD es un string 
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@Nombre, @Fecha)";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@Nombre", pres.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@Fecha", pres.FechaCreacion));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void CrearNuevoDetalle(int idPresupuesto, PresupuestoDetalle detalles)  // El idProducto mando como int o mando el producto o lo mando dentro de detalle? Se qchequea que el producto existe?
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto,Cantidad ) VALUES (@idPres,@idProd,@Cantidad)";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@idPres", idPresupuesto));
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
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var presupuesto = new Presupuesto();
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
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
            string query = "SELECT * FROM PresupuestosDetalle WHERE idPresupuesto=@idPres";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SqliteParameter("@idPres", idPresupuesto));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var presupuestoDetalle = new PresupuestoDetalle();
                    var productoRepository= new ProductoRepository();
                    presupuestoDetalle.Cantidad= Convert.ToInt32(reader["Cantidad"]);
                    presupuestoDetalle.CargarProducto(productoRepository.ObtenerProductoPorId(Convert.ToInt32(reader["idProducto"])));
                    listaDetalle.Add(presupuestoDetalle);
                }
            }
            connection.Close();

        }
        return listaDetalle;
    }
}
