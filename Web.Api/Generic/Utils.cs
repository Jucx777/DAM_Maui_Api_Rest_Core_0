using System.Text;
using XSystem.Security.Cryptography;

namespace Web.Api.Generic
{
    public class Utils
    {
        public static string cifrarCadena(string cadena)
        {
            SHA256Managed sha = new SHA256Managed();
            //Bytes
            byte[] bytecadena = Encoding.Default.GetBytes(cadena);
            byte[] bytecifrado = sha.ComputeHash(bytecadena);
            return BitConverter.ToString(bytecifrado).Replace("-", "");
        }
    }
}
