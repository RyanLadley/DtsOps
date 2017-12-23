using dtso.core.Settings;
using dtso.core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace dtso.core
{
    public class ScheduledTasks
    {
        private IServiceProvider _serviceProvider;

        public ScheduledTasks(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Daily_SpreadsheetBackup()
        {
            //SInce these are scheduled Tasks, we have to inject the dependicies here
            var spreadsheetHandle = _serviceProvider.CreateScope().ServiceProvider.GetService<BackupSpreadsheetHandle>();
            var settings = _serviceProvider.CreateScope().ServiceProvider.GetService<IOptions<AppSettings>>().Value;

            var backupPath = spreadsheetHandle.WriteBackupSpreadsheet();

            SmtpClient client = new SmtpClient(settings.SmtpSettings.Server);
            client.UseDefaultCredentials = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("dtsops.cosprings@gmail.com");
            mailMessage.To.Add("dtsops.cosprings@gmail.com");
            mailMessage.Subject = "Daily Backup";
            mailMessage.Body = $"Attached is the backup for {DateTime.Now.ToString("dddd, dd MMMM yyyy")}";
            mailMessage.Attachments.Add(new System.Net.Mail.Attachment($"StaticDocuments/{backupPath}"));
            client.Send(mailMessage);
        }

        public void Daily_CleanupStaticDocuments()
        {
            var directory = new DirectoryInfo("StaticDocuments");

            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
