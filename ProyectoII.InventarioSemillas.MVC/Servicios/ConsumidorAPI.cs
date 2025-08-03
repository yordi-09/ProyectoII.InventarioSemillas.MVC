namespace ProyectoII.InventarioSemillas.MVC.Servicios
{
    public class ConsumidorAPI(IHttpClientFactory httpClientFactory, 
                               IHttpContextAccessor httpContextAccessor)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private HttpClient CrearClienteConToken()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken") ?? string.Empty;
            var client = _httpClientFactory.CreateClient("ApiJwt");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<TResponse?> ConsumirGetAsync<TResponse>(string endpoint)
        {
            var client = CrearClienteConToken();
            var response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode) return default;

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse?> ConsumirPostAsync<TRequest, TResponse>(string endpoint, TRequest datos)
        {
            var client = CrearClienteConToken();
            var response = await client.PostAsJsonAsync(endpoint, datos);

            if (!response.IsSuccessStatusCode) return default;

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<bool> ConsumirPutAsync<TRequest>(string endpoint, TRequest datos)
        {
            var client = CrearClienteConToken();
            var response = await client.PutAsJsonAsync(endpoint, datos);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ConsumirDeleteAsync(string endpoint)
        {
            var client = CrearClienteConToken();
            var response = await client.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
    }
}
