﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class MailService : IMailService
    {
        public void SendMail(string to, string from, string subject, string body)
        {
          Debug.WriteLine($"Sending mail... To: {to} From: {from} Subject: {subject}, Body:{body}");
        }
    }
}
