using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace AppCapacitacion.Services // 👈 Asegúrate que coincida con el namespace real de tu app
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // No envía nada (modo prueba)
            return Task.CompletedTask;
        }
    }
}
