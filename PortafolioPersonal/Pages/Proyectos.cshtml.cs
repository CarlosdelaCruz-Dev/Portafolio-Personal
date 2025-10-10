using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Necesario para 'ToListAsync'
using PortafolioPersonal.Models; // Necesario para la clase 'Proyecto'


namespace PortafolioPersonal.Pages
{
    public class ProyectosModel : PageModel
    {
        // variable privada para guardar la base de datos
        private readonly ApplicationDbContext _context; 

        // se le pide a .NET que le "inyecte" la base de datos que se configuro en este caso programs.cs
        public ProyectosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // crea una propiedad llamada Proyectos que es una lista de objetos Proyecto
        public IList<Proyecto> Proyectos { get; set; } = new List<Proyecto>();


        //metodo que se ejecuta al cargar la pagina
        public async Task OnGetAsync()
        {
            // Busca todos los proyectos en la base de datos y los asigna a la lista.
            if (_context.Proyectos != null)
            {
                Proyectos = await _context.Proyectos.ToListAsync();
            }
        }
    }

    
}