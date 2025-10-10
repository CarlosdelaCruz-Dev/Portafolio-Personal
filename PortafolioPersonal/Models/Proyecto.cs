using System.ComponentModel.DataAnnotations; //usamos esto para poder validar los datos
namespace PortafolioPersonal.Models
{
    public class Proyecto
    {
        //el Id es el unico numero que identifica a cada proyecto
        public int Id { get; set; }

        // '[Required]' hace que este campo sea obligatorio.
        // '[StringLength]' limita el número de letras para evitar textos muy largos.
        // Esta propiedad guardará el nombre de tu proyecto.
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        // Esta propiedad guardará una breve descripción de tu proyecto.
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }

        // '[DataType.Url]' le dice al programa que esto debe ser una dirección web válida.
        // Esta propiedad guardará el enlace a tu código en GitHub o similar.
        [DataType(DataType.Url)]
        [StringLength(255, ErrorMessage = "La URL del repositorio no puede exceder los 255 caracteres.")]
        public string UrlRepositorio { get; set; }

        // Esta propiedad guardará la tecnología principal que usaste (ej. C#, .NET).
        [StringLength(50, ErrorMessage = "La tecnología no puede exceder los 50 caracteres.")]
        public string Tecnologia { get; set; }

    }
}
