using Microsoft.EntityFrameworkCore;
using PortafolioPersonal.Models;

namespace PortafolioPersonal.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Esta línea crea la base de datos si aún no existe.
                context.Database.EnsureCreated();

                // Si la base de datos ya tiene proyectos, no hacemos nada.
                if (context.Proyectos.Any())
                {
                    return;
                }

                // Si no hay datos, agregamos tus tres proyectos.
                context.Proyectos.AddRange(
                    new Proyecto
                    {
                        Nombre = "Tuxtlan Coffee & Pets",
                        Descripcion = "Sistema web full-stack para el control de inventarios y ventas, desarrollado con Python y Django. Implementé funcionalidades clave como la generación de reportes en Excel y filtros de búsqueda dinámicos en una base de datos SQLite.",
                        Tecnologia = "Python / Django",
                        UrlRepositorio = "https://github.com/CarlosdelaCruz-Dev/restaurante.git" 
                    },
                    new Proyecto
                    {
                        Nombre = "App Móvil de Tareas (Académico)",
                        Descripcion = "Aplicación móvil nativa para Android desarrollada en Java. Incluye funcionalidades de autenticación de usuarios, gestión completa de tareas (CRUD) y un módulo para reproducción de video local y en streaming.",
                        Tecnologia = "Java / Android",
                        UrlRepositorio = "https://github.com/CarlosdelaCruz-Dev/AplicacionMovil-Android-TaskMedia.git"
                    },
                    new Proyecto
                    {
                        Nombre = "Portafolio Personal Web",
                        Descripcion = "Aplicación web personal desarrollada desde cero con C# y ASP.NET Core. Presenta un diseño UI/UX personalizado con animaciones CSS y un backend funcional que incluye un formulario de contacto y sistema de proyectos.",
                        Tecnologia = "C# / ASP.NET Core",
                        UrlRepositorio = "https://github.com/CarlosdelaCruz-Dev/Portafolio-Personal.git"
                    }
                );

                // Guardamos todos los cambios en la base de datos.
                context.SaveChanges();
            }
        }
    }
}