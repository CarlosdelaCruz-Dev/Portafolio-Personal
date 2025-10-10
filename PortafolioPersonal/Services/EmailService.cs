// using son directivas que importan "cajas de herramientas" que necesitamos.
// MailKit contiene las herramientas para crear y enviar correos. MimeKit para construir el mensaje.
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

// El namespace es como la "dirección" de esta clase dentro de nuestro proyecto.
// Debe coincidir con la ubicación de la carpeta para que otras partes del código la encuentren.
namespace PortafolioPersonal.Services
{
    // Definimos nuestra clase. Es el "plano" para crear objetos que saben cómo enviar correos.
    public class EmailService
    {
        // Este es un campo privado para guardar nuestra "llave" de acceso a la configuración.
        // IConfiguration es la herramienta de .NET que nos permite leer el archivo appsettings.json.
        // 'readonly' significa que una vez que le asignemos un valor en el constructor, no se puede cambiar.
        private readonly IConfiguration _configuration;

        // Este es el "Constructor" de la clase. Se ejecuta automáticamente cada vez que se crea un nuevo EmailService.
        // Recibe un objeto IConfiguration (esto se llama Inyección de Dependencias, la aplicación se lo pasa automáticamente).
        public EmailService(IConfiguration configuration)
        {
            // Guardamos la configuración que nos pasaron en nuestro campo privado para poder usarla después.
            _configuration = configuration;
        }

        // Este es el método principal que hará todo el trabajo.
        // 'public' significa que se puede llamar desde otras clases (como nuestra página de Contacto).
        // 'async Task' significa que es una operación asíncrona. No bloqueará la aplicación mientras espera la respuesta del servidor de correo.
        // Recibe los datos del formulario como parámetros.
        public async Task SendEmailAsync(string name, string fromEmail, string message)
        {
            // --- 1. LEER LA CONFIGURACIÓN ---
            // Le pedimos a IConfiguration que nos dé la sección "SmtpSettings" de appsettings.json.
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            // Leemos cada valor específico de esa sección.
            var senderEmail = smtpSettings["SenderEmail"];
            var senderName = smtpSettings["SenderName"];
            var password = smtpSettings["Password"];

            // --- 2. CONSTRUIR EL MENSAJE (Como armar una carta y un sobre) ---
            var email = new MimeMessage(); // Creamos un objeto de correo vacío. Es nuestro "sobre".

            // Ponemos la dirección del remitente.
            email.From.Add(new MailboxAddress(senderName, senderEmail));
            // Ponemos la dirección del destinatario. En este caso, te lo envías a ti mismo.
            email.To.Add(new MailboxAddress(senderName, senderEmail));
            // Escribimos el asunto del correo.
            email.Subject = $"Nuevo mensaje de contacto de: {name}";

            // Creamos el cuerpo del correo. Usamos formato HTML para que se vea bonito.
            email.Body = new TextPart(TextFormat.Html)
            {
                // El texto del correo. Las '$' permiten incrustar variables (name, fromEmail, message) directamente.
                Text = $@"
                    <h1>Nuevo Mensaje del Portafolio</h1>
                    <p>Has recibido un nuevo mensaje de tu formulario de contacto.</p>
                    <ul>
                        <li><strong>Nombre:</strong> {name}</li>
                        <li><strong>Correo:</strong> {fromEmail}</li>
                    </ul>
                    <h2>Mensaje:</h2>
                    <p>{message}</p>"
            };

            // --- 3. ENVIAR EL CORREO (Como ir a la oficina de correos) ---
            using var smtp = new SmtpClient(); // Creamos un cliente SMTP, que es nuestro "camión de reparto".

            // Nos conectamos al servidor de correos (ej: smtp.gmail.com) en el puerto correcto.
            // SecureSocketOptions.StartTls establece una conexión segura.
            await smtp.ConnectAsync(smtpSettings["Server"], int.Parse(smtpSettings["Port"]), SecureSocketOptions.StartTls);

            // Nos autenticamos con nuestro usuario y contraseña de aplicación.
            await smtp.AuthenticateAsync(senderEmail, password);

            // Enviamos el mensaje que construimos.
            await smtp.SendAsync(email);

            // Nos desconectamos del servidor de forma limpia.
            await smtp.DisconnectAsync(true);
        }
    }
}