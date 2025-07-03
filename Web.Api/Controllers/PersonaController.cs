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
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //Listado sin filtro
        [HttpGet]
        public List<PersonaCLS> ListaPersona()
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 correo = persona.Correo,
                                 fechanacimientocadena = persona.Fechanacimiento == null ? "" :
                                    persona.Fechanacimiento.Value.ToString("yyyy-MM-dd"),

                                 fotocadena = persona.Varchivo == null ? "" :
                                    "data:image/" + System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) +
                                    ";base64," + Convert.ToBase64String(persona.Varchivo),
                             }).ToList();

                }

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //Listado con filtro
        [HttpGet("{nombrecompleto}")]

        public List<PersonaCLS> BuscarPersona(string nombrecompleto)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
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
                                 persona.Fechanacimiento.Value.ToShortDateString(),

                                 fotocadena = persona.Varchivo == null ? "" :
                                    "data:image/" + System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) +
                                    ";base64" + Convert.ToBase64String(persona.Varchivo),

                             }).ToList();

                }

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Recuperar por ID
        [HttpGet("recuperarPersona/{id}")]

        public PersonaCLS RecuperarPersona(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();
            try
            {
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
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
                                       persona.Fechanacimiento.Value.ToString("yyyy-MM-dd"),
                                       iidsexo = (int)persona.Iidsexo,

                                       fotocadena = persona.Varchivo == null ? "":
                                       "data:image/" + System.IO.Path.GetExtension(persona.Vnombrearchivo).Substring(1) +
                                       ";base64," + Convert.ToBase64String(persona.Varchivo),
                                   }).First();
                }
                return oPersonaCLS;
            }
            catch (Exception ex)
            {
                return oPersonaCLS;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Método ontrolador Eliminar Delete
        [HttpDelete("{id}")]

        public int EliminarPersona(int id)
        {
            // 0 -> Error; 1 -> Exito
            int rpta;
            try
            {
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
                {
                    Persona oPersona = bd.Personas.Where(p => p.Iidpersona == id).First();
                    oPersona.Bhabilitado = 0;
                    bd.SaveChanges();
                    rpta = 1;
                    return rpta;

                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Método Controlador Guardar POST
        // Metodo para guardar persona POST
        [HttpPost]
        public int GuardarPersona([FromBody] PersonaCLS oPersonaCLS)
        {
            int rpta = 0;
            try
            {
                int id = oPersonaCLS.iidpersona;
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
                {
                    //Si es 0 es nuevo registro
                    if (id == 0)
                    {
                        Persona oPersona = new Persona();

                        oPersona.Nombre = oPersonaCLS.nombre;
                        oPersona.Appaterno = oPersonaCLS.appaterno;
                        oPersona.Apmaterno = oPersonaCLS.apmaterno;
                        oPersona.Correo = oPersonaCLS.correo;
                        oPersona.Fechanacimiento = DateTime.Parse(oPersonaCLS.fechanacimientocadena);
                        oPersona.Iidsexo = oPersonaCLS.iidsexo;
                        oPersona.Btieneusuario = 0;
                        //oPersona.Btieneusuario = 0;

                        if (oPersonaCLS.nombrearchivo != "")
                        {
                            oPersona.Vnombrearchivo = oPersonaCLS.nombrearchivo;
                            oPersona.Varchivo = oPersonaCLS.archivo;
                        }

                        oPersona.Bhabilitado = 1;
                        bd.Personas.Add(oPersona);
                        bd.SaveChanges();
                        rpta = 1;

                    }
                    else // si es 1 entonces se trata de editar
                    {
                        Persona oPersona = bd.Personas.Where(p => p.Iidpersona == id).First();

                        oPersona.Nombre = oPersonaCLS.nombre;
                        oPersona.Appaterno = oPersonaCLS.appaterno;
                        oPersona.Apmaterno = oPersonaCLS.apmaterno;
                        oPersona.Correo = oPersonaCLS.correo;
                        oPersona.Fechanacimiento = DateTime.Parse(oPersonaCLS.fechanacimientocadena);
                        oPersona.Iidsexo = oPersonaCLS.iidsexo;

                        if (oPersonaCLS.nombrearchivo != "")
                        {
                            oPersona.Vnombrearchivo = oPersonaCLS.nombrearchivo;
                            oPersona.Varchivo = oPersonaCLS.archivo;
                        }

                        bd.SaveChanges();
                        rpta = 1;

                    }

                }
                return rpta;



            }
            catch (Exception ex)
            {
                return rpta;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //Método para listar personas sin usuario
        [HttpGet("listarPersonaSinUsuario")]
        public List<PersonaCLS> listarPersonaSinUsuario()
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();

            try
            {
                using (DbAbafa4BdveterinariaContext bd = new DbAbafa4BdveterinariaContext())
                {
                    lista = (from persona in bd.Personas
                             where persona.Bhabilitado == 1
                             && persona.Btieneusuario == 0
                             select new PersonaCLS
                             {
                                 iidpersona = persona.Iidpersona,
                                 nombrecompleto = persona.Nombre + " " + 
                                 persona.Appaterno + " " + 
                                 persona.Apmaterno,
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
