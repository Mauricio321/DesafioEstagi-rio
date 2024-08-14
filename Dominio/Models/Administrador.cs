namespace Dominio.Models;

public class Administrador
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; } = default;
    public byte[] Salt { get; set; } = Array.Empty<byte>();
}
