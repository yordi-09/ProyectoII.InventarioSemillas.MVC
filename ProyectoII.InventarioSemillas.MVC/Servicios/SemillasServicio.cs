using ProyectoII.InventarioSemillas.MVC.Models.Semillas;

namespace ProyectoII.InventarioSemillas.MVC.Servicios
{
    public class SemillasServicio
    {
        private readonly ConsumidorAPI _consumidorApi;

        public SemillasServicio(ConsumidorAPI consumidorApi)
        {
            _consumidorApi = consumidorApi;
        }

        public async Task<List<SemillaDto>?> ObtenerTodasLasSemillas()
        {
            return await _consumidorApi.ConsumirGetAsync<List<SemillaDto>>("api/Semillas");
        }

        public async Task<SemillaDto?> ObtenerSemillaPorId(int id)
        {
            return await _consumidorApi.ConsumirGetAsync<SemillaDto>($"api/Semillas/{id}");
        }

        public async Task<bool> CrearSemilla(SemillaDto modelo)
        {
            return await _consumidorApi.ConsumirPostAsync<SemillaDto, bool>("api/Semillas", modelo);
        }

        public async Task<bool> ActualizarSemilla(int id, SemillaDto modelo)
        {
            return await _consumidorApi.ConsumirPutAsync($"api/Semillas/{id}", modelo);
        }

        public async Task<bool> EliminarSemilla(int id)
        {
            return await _consumidorApi.ConsumirDeleteAsync($"api/Semillas/{id}");
        }
    }
}