using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using PortafolioPersonal.Services; // �MUY IMPORTANTE! A�ade esta l�nea.

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
        [EmailAddress(ErrorMessage = "El formato del correo no es v�lido.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        public string Mensaje { get; set; }

        // 2. Modificamos el constructor para que reciba el EmailService (Inyecci�n de Dependencias).
        public ContactoModel(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void OnGet()
        {
            // Este m�todo se ejecuta cuando la p�gina se carga por primera vez.
            // Lo dejamos vac�o por ahora.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Este m�todo se ejecuta cuando el usuario env�a el formulario (POST).
            if (!ModelState.IsValid)
            {
                // Si el formulario no es v�lido (ej. campos vac�os),
                // simplemente volvemos a mostrar la p�gina con los mensajes de error.
                return Page();
            }

            // --- �LA MAGIA OCURRE AQU�! ---
            // 3. Llamamos a nuestro servicio de correo con los datos del formulario.
            await _emailService.SendEmailAsync(Nombre, Email, Mensaje);

            // Guardamos un mensaje de �xito para mostrarlo en la p�gina.
            TempData["SuccessMessage"] = "�Mensaje enviado con �xito!";

            // Redirigimos a la misma p�gina para limpiar el formulario.
            return RedirectToPage();
        }
    }
}