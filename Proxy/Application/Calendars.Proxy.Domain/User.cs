﻿namespace Calendars.Proxy.Domain;
/// <summary>
///     User data.
/// </summary>
public class User
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}