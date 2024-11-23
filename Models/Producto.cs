
using System.ComponentModel.DataAnnotations;

namespace espacioProducto;

public class Producto
{
    private int idProducto;

    private string? descripcion;
   
    private int precio;

    public int IdProducto { get => idProducto; set => idProducto = value; }
    
    [Required] //Porque si entra en blanco se genera mal la consulta SQL
    [StringLength(250)]
    public string ?Descripcion { get => descripcion; set => descripcion = value; }
 [Required]
    [Range(0,999999)]
    public int Precio { get => precio; set => precio = value; }
}