using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Método para listarUsusarios

        [HttpGet]
        public List<UsuarioCLS> ListarUsuario()
        {
            List<UsuarioCLS> lista = new List<UsuarioCLS>();

            try
            {
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
                {
                    lista = (from usuario in bd.Usuarios
                             join persona in bd.Personas
                             on usuario.Iidpersona equals persona.Iidpersona
                             join tipousuario in bd.TipoUsuarios
                             on usuario.Iidusuario equals tipousuario.Iidtipousuario
                             where usuario.Bhabilitado==1
                             select new UsuarioCLS
                             {
                                 iidusuario= usuario.Iidusuario,
                                 nombreusuario= usuario.Nombreusuario,
                                 nombrepersona= persona.Nombre+" "+persona.Appaterno+""+persona.Apmaterno,
                                 fotopersona = persona.Varchivo == null ? "" :
                                    "data:image/" + System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) +
                                    ";base64," + Convert.ToBase64String(persona.Varchivo),
                                 nombretipousuario= tipousuario.Nombre,
                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                return lista;
            }
            return lista;
        }

    }
}
