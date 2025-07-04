using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Text.Json;
using Web.Cliente.Clases;

namespace Web.Cliente.Controllers
{
    public class UsuarioController : Controller
    {
        private string urlbase;
        private string cadena;
        private readonly IHttpClientFactory _httpClientFactory;

        public UsuarioController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];
            //cadena = "Hola";
            _httpClientFactory = httpClientFactory;
        }


        // Traemos la data como string 
        // Listar personas
        public async Task<List<UsuarioCLS>> listarUsuarios()
        {
            List<UsuarioCLS> lista = await ClientHttp.GetAll<UsuarioCLS>(_httpClientFactory, urlbase, "/api/Usuario");
            lista.Where(p => p.fotopersona == "").ToList().ForEach(p => p.fotopersona = "/img/nofoto.jpg");
            return lista;
        }


        // Método buscar usuarios
        public async Task<List<UsuarioCLS>> buscarUsuarios(UsuarioCLS oUsuarioCLS)
        {
            if (oUsuarioCLS.nombreusuario == null)
            {
                oUsuarioCLS.nombreusuario = "";
            }


            List<UsuarioCLS> lista = await ClientHttp.PostList<UsuarioCLS>(_httpClientFactory, urlbase, "/api/Usuario",oUsuarioCLS);
            lista.Where(p => p.fotopersona == "").ToList().ForEach(p => p.fotopersona = "/img/nofoto.jpg");
            return lista;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
