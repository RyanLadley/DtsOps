using dtso.core.Settings;
using dtso.core.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace dtso.core
{
    public class ScheduledTasks
    {
        private SpreadsheetDocumentHandle _spreadsheet;
        private AppSettings _settings;

        public ScheduledTasks(SpreadsheetDocumentHandle spreadsheet, IOptions<AppSettings> settings)
        {
            _spreadsheet = spreadsheet;
            _settings = settings.Value;
        }

        public void Daily_SpreadsheetBackup()
        {
            var backupPath = _spreadsheet.WriteBackupSpreadsheet();

            SmtpClient client = new SmtpClient("mysmtpserver");
            client.UseDefaultCredentials = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("dtsops@gmail.com");
            mailMessage.To.Add("dtsops@gmail.com");
            mailMessage.Body = $"Attatched is the backup for {DateTime.Now.ToString("dddd, dd MMMM yyyy")}";
            mailMessage.Attachments.Add(new System.Net.Mail.Attachment($"StaticDocuments/{backupPath}"));
            client.Send(mailMessage);
        }
    }
}
