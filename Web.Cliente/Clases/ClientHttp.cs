using System.Text.Json;

namespace Web.Cliente.Clases
{
    public class ClientHttp
    {
        //Lista generica para cualquier tipo de Entidad de la BD
        public static async Task<List<T>> GetAll<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                string cadena = await cliente.GetStringAsync(rutaapi);
                List<T> lista = JsonSerializer.Deserialize<List<T>>(cadena);
                return lista;
            }
            catch (Exception ex)
            {

                return new List<T>(); //Retornaría una lista vacia
            }

        }


        //Listar con filtro

        public static async Task<T> Get<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                string cadena = await cliente.GetStringAsync(rutaapi);
                T lista = JsonSerializer.Deserialize<T>(cadena);
                return lista;
            }
            catch (Exception ex)
            {

                return (T)Activator.CreateInstance(typeof(T));
            }

        }


        //Método Eliminar DELETE
        public static async Task<int> Delete(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                var response = await cliente.DeleteAsync(rutaapi);

                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return int.Parse(cadena);                
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        // Metodo Guardar POST
        public static async Task<int> Post<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi, T obj)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                var response = await cliente.PostAsJsonAsync(rutaapi, obj);
                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return int.Parse(cadena);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        // Metodo Guardar POST para una lista 
        public static async Task<List<T>> PostList<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi, T obj)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                var response = await cliente.PostAsJsonAsync(rutaapi, obj);
                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return  JsonSerializer.Deserialize<List<T>>(cadena);
                }
                else
                {
                    return new List<T>();
                }

            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
    }
}
