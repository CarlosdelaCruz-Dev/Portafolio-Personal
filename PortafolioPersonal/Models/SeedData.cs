using Microsoft.EntityFrameworkCore;
using PortafolioPersonal.Models;
using PortafolioPersonal.Data;

namespace PortafolioPersonal.Data // Asegúrate que el namespace sea el correcto para tu proyecto
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Usamos 'using' para asegurarnos que el 'context' se destruya correctamente
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Esta línea crea la base de datos y su esquema si aún no existen.
                context.Database.EnsureCreated();

                // Verificamos si ya existe algún proyecto en la base de datos.
                if (context.Proyectos.Any())
                {
                    return;   // Si ya hay datos, no hacemos nada y salimos del método.
                }

                // Si no hay datos, agregamos uno de prueba.
                context.Proyectos.Add(
                    new Proyecto
                    {
                        Nombre = "Mi Primer Proyecto",
                        Descripcion = "Un proyecto de prueba para mi portafolio.",
                        Tecnologia = "C# y .NET",
                        UrlRepositorio = "https://github.com/tu-usuario/mi-primer-proyecto"
                    }
                );

                // Guardamos los cambios en la base de datos.
                context.SaveChanges();
            }
        }
    }
}