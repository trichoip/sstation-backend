﻿namespace ShipperStation.Infrastructure.Settings;
public class MailSettings
{
    public static readonly string Section = "Email";

    public int Port { get; set; }
    public string Host { get; set; } = default!;
    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool IsBodyHtml { get; set; }
    public string From { get; set; } = default!;
}