using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShrimpPond.Application.Contract.EmailService;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.SmsService;
using ShrimpPond.Application.Models.Email;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Application.Models.Sms;
using ShrimpPond.Infrastructure.EmailService;
using ShrimpPond.Infrastructure.GmailService;
using ShrimpPond.Infrastructure.Logging;
using ShrimpPond.Infrastructure.SmsService;


namespace ShrimpPond.Infrastructure
{
	public static class InfrastructureServicesRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<GmailSettings>(configuration.GetSection("GmailSettings"));
            services.AddTransient<IGmailSender, GmailSender>();

            services.Configure<SmsSettings>(configuration.GetSection("SmsSettings"));
            services.AddTransient<ISmsSender, SmsSender>();
            return services;
		}
	}
}