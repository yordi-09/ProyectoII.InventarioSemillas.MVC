namespace ProyectoII.InventarioSemillas.MVC.Servicios
{
    public interface IApiCliente
    {
        Task<List<string>?> ObtenerPronosticoAsync();
    }

}
