using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using Web.Api.Models;
using System.Transactions;
using Web.Api.Generic;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Método para listarUsusarios

        [HttpGet]
        public List<UsuarioCLS> listarUsuario()
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
                                 nombreusuario= usuario.Nombreusuario.ToLower(),
                                 nombrepersona= persona.Nombre+" "+persona.Appaterno+" "+persona.Apmaterno,
                                 iidtipousuario = (int)usuario.Iidtipousuario,
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

        //Método para buscarUsuarios por capas
        [HttpPost]
        public List<UsuarioCLS> buscarUsuarios([FromBody]UsuarioCLS oUsuarioCLS)
        {
            string nombreusuario = oUsuarioCLS.nombreusuario;
            int iidtipousuario = oUsuarioCLS.iidtipousuario;

            List<UsuarioCLS> lista = listarUsuario();

            if (nombreusuario != "")
            {
                lista = lista.Where(p => p.nombreusuario.Contains(nombreusuario)).ToList();
            }
            if (iidtipousuario != 0)
            {
                lista = lista.Where(p => p.iidtipousuario == iidtipousuario).ToList();
            }
            return lista;
        }

        //Método sirve para guardar datos de usuarios
        //1. -> Insertar en la tabla de usuarios (Insert Usuarios)
        //2. -> Cambiar Btieneusuario = 0 -> Btieneusuario = 1 (Update Personas)
        [HttpPost("guardarDatos")]
        public int guardarDatos([FromBody] UsuarioCLS oUsuarioCLS)
        {
            int rpta = 0;
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
                    {
                        if (oUsuarioCLS.iidusuario == 0) // Insertar Usuarios
                        {
                            Usuario oUsuario = new Usuario();
                            oUsuario.Nombreusuario = oUsuarioCLS.nombreusuario;
                            oUsuario.Iidtipousuario = oUsuarioCLS.iidtipousuario;
                            oUsuario.Iidpersona = oUsuarioCLS.iidpersona;

                            oUsuario.Contra = Utils.cifrarCadena(oUsuarioCLS.contra);

                            oUsuario.Bhabilitado = 1;
                            bd.Usuarios.Add(oUsuario);
                            bd.SaveChanges();

                            Persona oPersona = bd.Personas.Where(p => p.Iidpersona == oUsuarioCLS.iidpersona).First();
                            oPersona.Btieneusuario = 1;
                            bd.SaveChanges();

                            //Grabando en la transaction
                            transaction.Complete(); 

                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
