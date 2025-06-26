namespace CapaEntidad
{
    public class PersonaCLS
    {
        // Listado

        public int iidpersona { get; set; } = 0;
        public string nombrecompleto { get; set; } = "";
        public string correo { get; set; } = "";
        public string fechanacimientocadena { get; set; } = "";

        // Recuperar

        public string nombre { get; set; } = "";
        public string appaterno { get; set; } = "";
        public string apmaterno { get; set; } = "";
        public DateTime fechanacimiento { get; set; }
        public int iidsexo { get; set; } = 0;

        ////Foto
        //public string nombrearchivo { get; set; } = "";
        //public string fotocadena { get; set; } = "";
        //public byte [] archivo {  get; set; } 

    }
}
