namespace CapaEntidad
{
    public class PersonaCLS
    {
        // Listado

        public int iidpersona { get; set; }
        public string nombrecompleto { get; set; }
        public string correo { get; set; }
        public string fechanacimientocadena { get; set; }

        // Recuperar

        public string nombre { get; set; }
        public string appaterno { get; set; }
        public string apmaterno { get; set; }
        public DateTime fechanacimiento { get; set; }
        public int iisexo { get; set; }

    }
}
