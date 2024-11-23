using System.ComponentModel.DataAnnotations;

namespace espacioCliente;
public class Cliente
{
    private int clienteId;
    private string? nombre;

    private string? email;
    private string? telefono;

    public int ClienteId { get => clienteId; set => clienteId = value; }
    [Required]
    public string? Nombre { get => nombre; set => nombre = value; }
    [EmailAddress]
    public string? Email { get => email; set => email = value; }
    [Phone]
    public string? Telefono { get => telefono; set => telefono = value; }
}