using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SendGrid;
using StaffingWebService.Data;
using StaffingWebService.Model;
using System.Runtime.InteropServices;
using System.Web;

namespace StaffingWebService
{
    public class Mail
    {
        private const string Footer =
            "Ce message électronique a été généré et envoyé automatiquement. Merci de ne pas y répondre.";
        private const string FooterHTML =
            "Ce message &eacute;lectronique a &eacute;t&eacute; g&eacute;n&eacute;r&eacute; et envoy&eacute; automatiquement. Merci de ne pas y r&eacute;pondre.";

        private static string WebServiceRootPath = HttpContext.Current.Server.MapPath("."); // http://staffingservice.azurewebsites.net
        private const string CRARefusedObject = "[STAFFING] CRA refusé";
        private const string CRARequestObject = "[STAFFING] Demande de CRA";
        private const string CRASubmitedObject = "[STAFFING] CRA soumis";
        private const string CRAValidatedObject = "[STAFFING] CRA validé";
        private const string HolidayRefusedObject = "[STAFFING] Demande de congés refusée";
        private const string HolidayRequestObject = "[STAFFING] Demande de congés";
        private const string HolidayValidatedObject = "[STAFFING] Demande de congés validée";

        private const string CRARefused = "Votre CRA pour {0} {1} a été refusé.";

        private const string CRARequest =
            "Nous attendons votre CRA pour {0} {1}. Merci de nous le faire parvenir dès que possible.";

        private const string CRASubmited = "{0} a soumis son CRA pour {1} {2} : {3}";
        private const string CRAValidated = "Votre CRA pour {0} {1} a été validé : {2}";
        private const string HolidayRefused = "Votre demande de congés a été refusée.";
        private const string HolidayRequest = "{0} a soumis une demande de congés.";
        private const string HolidayValidated = "Votre demande de congés a été validée.";

        public static void Send(int? consultantId, MailType type, object item)
        {
            // Create the email object first, then add the properties.
            var newMessage = new SendGridMessage();

            var sender = CreateSender();
            IEnumerable<string> recipients;
            IEnumerable<string> recipientsCc;
            CreateRecipients(type, consultantId, out recipients, out recipientsCc);
            var mailObject = CreateObject(type);
            var body = CreateBody(type, consultantId, item);

            string attachement = null;
            if (type == MailType.CRASubmited)
                attachement = GetCRAPdfPath(consultantId, (CompteRenduActivite)item);

            CreateMail(sender, recipients, recipientsCc, mailObject, body, newMessage, attachement);
            
            var transport = CreateCredentials();
            // Send the email.
            transport.Deliver(newMessage);
        }

        private static string CreateObject(MailType type)
        {
            switch (type)
            {
                case MailType.CRARefused:
                    return CRARefusedObject;
                case MailType.CRARequest:
                    return CRARequestObject;
                case MailType.CRASubmited:
                    return CRASubmitedObject;
                case MailType.CRAValidated:
                    return CRAValidatedObject;
                case MailType.HolidayRefused:
                    return HolidayRefusedObject;
                case MailType.HolidayRequest:
                    return HolidayRequestObject;
                case MailType.HolidayValidated:
                    return HolidayValidatedObject;
                default:
                    return string.Empty;
            }
        }


        private static string GetCRAPdfPath(int? consultantId, CompteRenduActivite cra)
        {
            Consultant consultant = ConsultantDAL.GetConsultants().Single(o => o.Id == consultantId);
            return string.Format("{0}\\Files\\{1}{2}_{3}_{4}.pdf", WebServiceRootPath,
                                            cra.Year, cra.Month.ToString("00"), consultant.Nom, consultant.Prenom);
        }

        private static string CreateBody(MailType type, int? consultantId, object item)
        {
            string message;
          
            switch (type)
            {
                case MailType.CRARefused:
                    message = string.Format(CRARefused,
                                            CultureInfo.GetCultureInfo("fr-fr").DateTimeFormat.GetMonthName(
                                                ((CompteRenduActivite)item).Month),
                                            ((CompteRenduActivite)item).Year);
                    break;

                case MailType.CRAValidated:
                    message = string.Format(CRAValidated,
                                            CultureInfo.GetCultureInfo("fr-fr").DateTimeFormat.GetMonthName(
                                                ((CompteRenduActivite)item).Month),
                                            ((CompteRenduActivite)item).Year, GetCRAPdfPath(consultantId,(CompteRenduActivite)item));
                    break;

                case MailType.CRASubmited:
                  
                    message = string.Format(CRASubmited, CreateConsultantName(consultantId),
                                            CultureInfo.GetCultureInfo("fr-fr").DateTimeFormat.GetMonthName(
                                                ((CompteRenduActivite)item).Month),
                                            ((CompteRenduActivite)item).Year, GetCRAPdfPath(consultantId,(CompteRenduActivite)item));
                    break;

                case MailType.CRARequest:
                    message = string.Format(CRARequest,
                                            CultureInfo.GetCultureInfo("fr-fr").DateTimeFormat.GetMonthName(
                                                DateTime.Now.Month), DateTime.Now.Year);
                    break;

                case MailType.HolidayValidated:
                    message = HolidayValidated;
                    break;

                case MailType.HolidayRequest:
                    message = string.Format(HolidayRequest, CreateConsultantName(consultantId));
                    break;

                case MailType.HolidayRefused:
                    message = HolidayRefused;
                    break;

                default:
                    return string.Empty;
            }

            return string.Format("<p>Bonjour,</p><p>{0}</p><p>Cordialement.</p>", message);
        }

        private static MailAddress CreateSender()
        {
            // Add the message properties.
            var sender = new MailAddress(@"Staffing<no-reply-staffing@cellenza.com>");
            return sender;
        }

        private static ITransport CreateCredentials()
        {
            // Create network credentials to access your SendGrid account.
            var username = ConfigurationManager.AppSettings["SMTPUsername"];
            var pswd = ConfigurationManager.AppSettings["SMTPPwd"];

            var credentials = new NetworkCredential(username, pswd);

            ITransport transport = new Web(credentials);

            return transport;
        }

        private static void CreateMail(MailAddress sender, IEnumerable<string> recipients,
                                       IEnumerable<string> recipientsCc, string mailObject,
                                       string body, ISendGrid newMessage, [Optional]string attachementPath)
        {
            newMessage.From = sender;

            foreach (var recipient in recipients)
            {
                newMessage.AddTo(recipient);
            }

            foreach (var recipient in recipientsCc)
            {
                newMessage.AddTo(recipient);
            }

            // Add a message body in HTML format.
            newMessage.Html = body;

            // Add the subject.
            newMessage.Subject = mailObject;

            // Add a footer to the message.
            newMessage.EnableFooter(Footer, string.Format("<p><em>{0}</em></p>", FooterHTML));

            //Add attachement
            if (!string.IsNullOrEmpty(attachementPath))
                newMessage.AddAttachment(attachementPath);
        }

        private static void CreateRecipients(MailType type, int? consultantId, out IEnumerable<string> recipients, out IEnumerable<string> recipientsCc)
        {
            recipients = null;
            recipientsCc = null;

            switch (type)
            {
                case MailType.HolidayRefused:
                case MailType.CRARefused:
                case MailType.HolidayValidated:
                case MailType.CRAValidated:
                    recipients =
                        ConsultantDAL.GetConsultants().Where(o => o.Id == consultantId)
                            .Select(
                                recipient =>
                                string.Format("{0} <{1}>", CreateConsultantName(recipient.Id), recipient.Email));
                    recipientsCc =
                        ConsultantDAL.GetConsultants().Where(o => o.IsAdmin && o.Id != consultantId)
                            .Select(
                                recipient =>
                                string.Format("{0} <{1}>", CreateConsultantName(recipient.Id), recipient.Email));
                    break;
                case MailType.HolidayRequest:
                case MailType.CRASubmited:
                    recipientsCc =
                        ConsultantDAL.GetConsultants().Where(o => o.Id == consultantId)
                            .Select(
                                recipient =>
                                string.Format("{0} <{1}>", CreateConsultantName(recipient.Id), recipient.Email))
                            .ToList();
                    recipients =
                        ConsultantDAL.GetConsultants().Where(o => o.IsAdmin && o.Id != consultantId)
                            .Select(
                                recipient =>
                                string.Format("{0} <{1}>", CreateConsultantName(recipient.Id), recipient.Email));
                    break;
                case MailType.CRARequest:
                    recipients =
                        ConsultantDAL.GetConsultants()
                            .Select(
                                recipient =>
                                string.Format("{0} <{1}>", CreateConsultantName(recipient.Id), recipient.Email));
                    break;
            }
        }

        private static string CreateConsultantName(int? consultantId)
        {
            if (!consultantId.HasValue)
            {
                return string.Empty;
            }

            var consultant = ConsultantDAL.GetConsultant(consultantId.Value);
            var consultantName = string.Format("{0} {1}", consultant.Prenom,
                                               consultant.Nom);
            return consultantName;
        }
    }
}