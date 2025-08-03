namespace ProyectoII.InventarioSemillas.MVC.Servicios
{
    public class ApiCliente(IHttpClientFactory httpClientFactory, 
                            IHttpContextAccessor httpContextAccessor) : IApiCliente
    {
        public async Task<List<string>?> ObtenerPronosticoAsync()
        {
            ConsumidorAPI consumidorAPI = new(httpClientFactory, httpContextAccessor);

            var resultado = await consumidorAPI.ConsumirGetAsync<List<string>>("WeatherForecast");

            return resultado;
        }
    }
}
