using espacioProducto;
using Microsoft.Data.Sqlite;

namespace espacioProductoRepository;
public class ProductoRepository
{
    string cadenaConexion = @"Data Source=Tienda.db;Cache=Shared";
    public List<Producto> ListarProductos()
    {
        List<Producto> listaProd = new List<Producto>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Productos;";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var prod = new Producto();
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    prod.Descripcion = reader["Descripcion"].ToString();
                    prod.Precio = Convert.ToInt32(reader["Precio"]);
                    listaProd.Add(prod);
                }
            }
            connection.Close();

        }
        return listaProd;
    }
    public void CrearNuevo(Producto prod)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@Descripcion", prod.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", prod.Precio));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void ModificarProducto(int idModificar, Producto nuevoProducto)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "UPDATE Productos SET Descripcion= @Descripcion , Precio= @Precio WHERE idProducto=@id ";
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@Descripcion", nuevoProducto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", nuevoProducto.Precio));
            command.Parameters.Add(new SqliteParameter("@id", idModificar));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public Producto ObtenerProductoPorId(int id)
    {
        Producto prod = new Producto();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Productos WHERE idProducto=@id;";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SqliteParameter("@id", id));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    prod.Descripcion = reader["Descripcion"].ToString();
                    prod.Precio = Convert.ToInt32(reader["Precio"]);
                }

            }
            connection.Close();

        }
        return prod;
    }
    public void EliminarProducto(int idEliminar)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "DELETE FROM PresupuestosDetalle WHERE idProducto=@id"; //Para eliminar de presupuesto detalle y no quede el id vacio
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", idEliminar));
            command.ExecuteNonQuery();
            query = "DELETE FROM Productos WHERE idProducto=@id";   //Para eliminar el producto
            connection.Open();
            command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", idEliminar));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}