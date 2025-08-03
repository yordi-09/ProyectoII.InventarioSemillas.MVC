using ProyectoII.InventarioSemillas.MVC.Models.Roles;

namespace ProyectoII.InventarioSemillas.MVC.Servicios
{
    public class RolesServicio
    {
        private readonly ConsumidorAPI _consumidorApi;

        public RolesServicio(ConsumidorAPI consumidorApi)
        {
            _consumidorApi = consumidorApi;
        }

        public async Task<List<RolDto>?> ObtenerTodosLosRoles()
        {
            return await _consumidorApi.ConsumirGetAsync<List<RolDto>>("api/roles");
        }


        public async Task<string?> CrearRol(CreateRoleDto modelo)
        {
            return await _consumidorApi.ConsumirPostAsync<CreateRoleDto, string>("api/roles/crear", modelo);
        }

        public async Task<bool> EliminarRol(string roleName)
        {
            return await _consumidorApi.ConsumirDeleteAsync($"api/roles/{roleName}");
        }

        public async Task<string?> AsignarRolAUsuario(UserRoleDto modelo)
        {
            return await _consumidorApi.ConsumirPostAsync<UserRoleDto, string>("api/roles/asignar-rol", modelo);
        }

        public async Task<string?> RemoverRolDeUsuario(UserRoleDto modelo)
        {
            return await _consumidorApi.ConsumirPostAsync<UserRoleDto, string>("api/roles/remover-rol", modelo);
        }
    }
}