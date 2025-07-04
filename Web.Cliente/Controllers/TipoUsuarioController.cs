using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Text.Json;
using Web.Cliente.Clases;

namespace Web.Cliente.Controllers
{
    public class TipoUsuarioController : Controller
    {

        private string urlbase;
        private string cadena;
        private readonly IHttpClientFactory _httpClientFactory;

        public TipoUsuarioController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];

            _httpClientFactory = httpClientFactory;
        }


        //metodo para listar personas sin usuario
        public async Task<List<TipoUsuarioCLS>> listarTipoUsuario()
        {
            List<TipoUsuarioCLS> lista = await ClientHttp.GetAll<TipoUsuarioCLS>(_httpClientFactory, urlbase,
                "/api/TipoUsuario");

            return lista;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
