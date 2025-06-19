using CapaEntidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;
using System.Text.Json;



namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        //Listado sin filtro
        [HttpGet]

        public List<PersonaCLS> listaPersona()
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAba87fBdveterinariaContext bd = new DbAba87fBdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" :
                        persona.Fechanacimiento.Value.ToString("dd/MM/yyyy")
                             }).ToList();

                }

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }



        //Listado con filtro
        [HttpGet("{nombrecompleto}")]

        public List<PersonaCLS> listaPersona(string nombrecompleto)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAba87fBdveterinariaContext bd = new DbAba87fBdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1
                             && (persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno).Contains(nombrecompleto)
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" :
                                 persona.Fechanacimiento.Value.ToString("dd/MM/yyyy")
                             }).ToList();

                }

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        // Recuperar por ID
        [HttpGet("recuperarPersona/{id}")]

        public PersonaCLS recuperarPersona(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();
            try
            {
                using (DbAba87fBdveterinariaContext bd = new DbAba87fBdveterinariaContext())
                {
                    oPersonaCLS = (from persona in bd.Personas
                                   where persona.Bhabilitado == 1 && persona.Iidpersona == id
                                   select new PersonaCLS
                                   {
                                       iidpersona = persona.Iidpersona,
                                       nombre = persona.Nombre,
                                       appaterno = persona.Appaterno,
                                       apmaterno = persona.Apmaterno,
                                       correo = persona.Correo,
                                       fechanacimiento = (DateTime)persona.Fechanacimiento,
                                       fechanacimientocadena = persona.Fechanacimiento == null ? "" :
                                       persona.Fechanacimiento.Value.ToString("dd/MM/yyyy"),
                                       iisexo = (int)persona.Iidsexo,
                                   }).First();
                }
                return oPersonaCLS;
            }
            catch (Exception ex)
            {
                return oPersonaCLS;
            }
        }
    }
}
