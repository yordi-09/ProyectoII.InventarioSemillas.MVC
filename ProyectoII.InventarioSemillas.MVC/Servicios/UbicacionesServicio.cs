using ProyectoII.InventarioSemillas.MVC.Models.Ubicaciones;

namespace ProyectoII.InventarioSemillas.MVC.Servicios
{
    public class UbicacionesServicio
    {
        private readonly ConsumidorAPI _consumidorApi;

        public UbicacionesServicio(ConsumidorAPI consumidorApi)
        {
            _consumidorApi = consumidorApi;
        }

        public async Task<List<UbicacionDto>?> ObtenerTodasLasUbicaciones()
        {
            return await _consumidorApi.ConsumirGetAsync<List<UbicacionDto>>("api/Ubicaciones");
        }

        public async Task<UbicacionDto?> ObtenerUbicacionPorId(int id)
        {
            return await _consumidorApi.ConsumirGetAsync<UbicacionDto>($"api/Ubicaciones/{id}");
        }

        public async Task<bool> CrearUbicacion(UbicacionDto modelo)
        {
            return await _consumidorApi.ConsumirPostAsync<UbicacionDto, bool>("api/Ubicaciones", modelo);
        }

        public async Task<bool> ActualizarUbicacion(int id, UbicacionDto modelo)
        {
            return await _consumidorApi.ConsumirPutAsync($"api/Ubicaciones/{id}", modelo);
        }

        public async Task<bool> EliminarUbicacion(int id)
        {
            return await _consumidorApi.ConsumirDeleteAsync($"api/Ubicaciones/{id}");
        }
    }
}
