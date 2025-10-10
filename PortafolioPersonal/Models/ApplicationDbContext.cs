using Microsoft.EntityFrameworkCore;//lo necesitamos para poder trabajar con la base de datos
using PortafolioPersonal.Models;//lo necesitamos poder usar la clase Proyecto

namespace PortafolioPersonal.Models//seguimos en la carpeta de modelos
{
    //con esta clase nos encargamos de conectar la base de datos con la aplicacion
    public class ApplicationDbContext : DbContext
    {
        //este es un constructor y es para que .net le pueda decir a esta clase que base de datos va a usar
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Esta línea es la más importante. Le dice a Entity Framework que debe crear
        // una tabla en la base de datos llamada 'Proyectos',
        // donde cada fila será un objeto de la clase 'Proyecto'.
        public DbSet<Proyecto> Proyectos { get; set; }
    }
}


