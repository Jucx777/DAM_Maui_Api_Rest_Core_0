
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Text.Json;
using Web.Cliente.Clases;

namespace Web.Cliente.Controllers
{
    public class PersonaController : Controller
    {
        private string urlbase;
        private string cadena;
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonaController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];

            _httpClientFactory = httpClientFactory;
        }

        // Traemos la data como string 

        // Listar personas
        public async Task<List<PersonaCLS>> ListarPersonas()
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona");
            //List<PersonaCLS> lista = JsonSerializer.Deserialize<List<PersonaCLS>>(cadena);
            //return lista;

            List<PersonaCLS> lista  =  await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona");

            lista.Where(p => p.fotocadena == "").ToList().ForEach(p => p.fotocadena = "/img/nofoto.jpg");
            return lista;

            
        }

        //Filtrar personas
        public async Task<List<PersonaCLS>> FiltrarPersonas(string nombrecompleto)
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona/" + nombrecompleto);
            //List<PersonaCLS> lista = JsonSerializer.Deserialize<List<PersonaCLS>>(cadena);
            //return lista;
            if (nombrecompleto != null)
            {
                List<PersonaCLS>lista = await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona/" + nombrecompleto);
                lista.Where(p => p.fotocadena == "").ToList().ForEach(p => p.fotocadena = "/img/nofoto.jpg");
                return lista;
            }
            return await ListarPersonas();
        }

        //Filtrar personas
        public async Task<PersonaCLS> RecuperarPersona(int id)
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona/" + nombrecompleto);
            //List<PersonaCLS> lista = JsonSerializer.Deserialize<List<PersonaCLS>>(cadena);
            //return lista;

            return await ClientHttp.Get<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona/recuperarPersona/" + id);
        }

        //Metodo para Eliminar DELETE
        public async Task<int> EliminarPersona(int id)
        {
            return await ClientHttp.Delete(_httpClientFactory, urlbase, "/api/Persona/" + id);
        }

        //Metodo para Guardar Post
    
        public async Task<int> GuardarPersona(PersonaCLS oPersonaCLS, IFormFile fotoenviar)
        {

            byte[] buffer = new byte[0];
            string nombrefoto = "";
            if (fotoenviar != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fotoenviar.CopyTo(ms);

                    nombrefoto = fotoenviar.FileName;
                    buffer = ms.ToArray();
                }
            }

            oPersonaCLS.nombrearchivo = nombrefoto;
            oPersonaCLS.archivo = buffer;


            return await ClientHttp.Post(_httpClientFactory, urlbase, "/api/Persona/", oPersonaCLS);
        }


        //metodo para listar personas sin usuario
        public async Task<List<PersonaCLS>> listarPersonasSinUsuario()
        {
            List<PersonaCLS> lista = await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase,
                "api/Persona/listarPersonaSinUsuario");

            return lista;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
