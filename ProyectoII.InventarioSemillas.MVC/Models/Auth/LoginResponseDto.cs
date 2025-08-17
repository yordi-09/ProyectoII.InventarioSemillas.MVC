namespace ProyectoII.InventarioSemillas.MVC.Models.Auth
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
        public required string Rol { get; set; }
        public required string Name { get; set; }
    }

}
