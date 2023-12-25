using Kidoo.Learn.Students.Dtos;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace Kidoo.Learn.EmailSenders
{
    [Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    public class MailJetEmailSender : IMailJetEmailSender
    {
        private readonly IConfiguration _config;
        public MailJetEmailSender(
            IConfiguration config)
        {
            _config = config;
        }

        public async Task SendStudentRegistraionEmailAsync(CreateUpdateStudentDto student)
        {
            var apiKey = _config.GetValue<string>("MailJet:ApiKey");
            var apiSecret = _config.GetValue<string>("MailJet:ApiSecret");
            var fromEmail = _config.GetValue<string>("MailJet:FromEmail");
            var fromName = _config.GetValue<string>("MailJet:FromName");
            var templateId = _config.GetValue<int>("MailJet:RegistrationTemplate:TemplateId");
            var subject = _config.GetValue<string>("MailJet:RegistrationTemplate:EmailSubject");

            MailjetClient client = new MailjetClient(apiKey, apiSecret);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.FromEmail, fromEmail)
            .Property(Send.FromName, fromName)
            .Property(Send.Subject, subject)
            .Property(Send.MjTemplateID, templateId)
            .Property(Send.MjTemplateLanguage, "True")
            .Property(Send.To, student.EmailAddress)
            .Property(Send.Vars, new JObject {
                    {"gurdian_name", student.GuardianName},
                    {"student_name", student.FirstName + " " + student.LastName},
                    {"age_group", student.AgeGroup.ToString()},
                    {"email", student.EmailAddress},
                    {"phone_number", student.PhoneNumber},
                    {"student_psd", student.Password},
                    {"kidoo_discount_coupon", "TARBIYAH05"}
                });

            MailjetResponse response = await client.PostAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                // Log or something
            }
        }
    }
}
