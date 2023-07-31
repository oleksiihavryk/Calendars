﻿namespace Calendars.Authentication.Core;
/// <summary>
///     Configuration of all clients in identity server in-memory configuration.
/// </summary>
public class ClientsConfiguration
{
    public ClientConfiguration Resources { get; set; }
    public ClientConfiguration Web { get; set; }
    public ClientConfiguration Proxy { get; set; }
    public ClientConfiguration Authentication { get; set; }

    public ClientsConfiguration(
        ClientConfiguration resources,
        ClientConfiguration web,
        ClientConfiguration proxy, 
        ClientConfiguration authentication)
    {
        Resources = resources;
        Web = web;
        Proxy = proxy;
        Authentication = authentication;
    }
}