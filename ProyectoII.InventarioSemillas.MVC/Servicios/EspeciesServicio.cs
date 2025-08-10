using ProyectoII.InventarioSemillas.MVC.Models.Especies;

namespace ProyectoII.InventarioSemillas.MVC.Servicios
{
    public class EspeciesServicio
    {
        private readonly ConsumidorAPI _consumidorApi;

        public EspeciesServicio(ConsumidorAPI consumidorApi)
        {
            _consumidorApi = consumidorApi;
        }

        public async Task<List<EspecieDto>?> ObtenerTodasLasEspecies()
        {
            return await _consumidorApi.ConsumirGetAsync<List<EspecieDto>>("api/Especies");
        }

        public async Task<EspecieDto?> ObtenerEspeciePorId(int id)
        {
            return await _consumidorApi.ConsumirGetAsync<EspecieDto>($"api/Especies/{id}");
        }

        public async Task<bool> CrearEspecie(EspecieDto modelo)
        {
            return await _consumidorApi.ConsumirPostAsync<EspecieDto, bool>("api/Especies", modelo);
        }

        public async Task<bool> ActualizarEspecie(int id, EspecieDto modelo)
        {
            return await _consumidorApi.ConsumirPutAsync($"api/Especies/{id}", modelo);
        }

        public async Task<bool> EliminarEspecie(int id)
        {
            return await _consumidorApi.ConsumirDeleteAsync($"api/Especies/{id}");
        }
    }
}
