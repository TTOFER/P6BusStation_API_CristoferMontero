namespace P6BusStation_API_CristoferMontero.ModelsDTOs
{
    public class UsuarioDTO
    {
        //propiedadaes del modelo original
        public int UsuarioID { get; set; }

        public string Correo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Telefono { get; set; } 

        public string Contrasennia { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public int RolID { get; set; }

        //propiedad agregada - diferente al modelo original
        public string RolDescripcion { get; set; } 
    }
}
