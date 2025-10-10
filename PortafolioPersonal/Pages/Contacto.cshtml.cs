using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using PortafolioPersonal.Services; // ¡MUY IMPORTANTE! Añade esta línea.

namespace PortafolioPersonal.Pages
{
    public class ContactoModel : PageModel
    {
        // 1. Un campo privado para guardar nuestro servicio de correo.
        private readonly EmailService _emailService;

        // Propiedades para enlazar con los campos del formulario.
        [BindProperty]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        public string Mensaje { get; set; }

        // 2. Modificamos el constructor para que reciba el EmailService (Inyección de Dependencias).
        public ContactoModel(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void OnGet()
        {
            // Este método se ejecuta cuando la página se carga por primera vez.
            // Lo dejamos vacío por ahora.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Este método se ejecuta cuando el usuario envía el formulario (POST).
            if (!ModelState.IsValid)
            {
                // Si el formulario no es válido (ej. campos vacíos),
                // simplemente volvemos a mostrar la página con los mensajes de error.
                return Page();
            }

            // --- ¡LA MAGIA OCURRE AQUÍ! ---
            // 3. Llamamos a nuestro servicio de correo con los datos del formulario.
            await _emailService.SendEmailAsync(Nombre, Email, Mensaje);

            // Guardamos un mensaje de éxito para mostrarlo en la página.
            TempData["SuccessMessage"] = "¡Mensaje enviado con éxito!";

            // Redirigimos a la misma página para limpiar el formulario.
            return RedirectToPage();
        }
    }
}