using CapaEntidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;
using System.Text.Json;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {

        [HttpGet]
        public List<TipoUsuarioCLS> listarTipoUsuario()
        {
            List<TipoUsuarioCLS> lista = new List<TipoUsuarioCLS>();
            try
            {
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
                {
                    lista = (from tipoUsuario in bd.TipoUsuarios
                             where tipoUsuario.Bhabilitado == 1
                             select new TipoUsuarioCLS
                             {
                                 iidtipousuario = tipoUsuario.Iidtipousuario,
                                 nombretipousuario = tipoUsuario.Nombre,
                             }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }


    }
}
