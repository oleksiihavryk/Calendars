﻿using Microsoft.AspNetCore.Http;

namespace Calendars.Proxy.Core;
/// <summary>
///     Unified response object for responses on inner requests.
/// </summary>
public class Response
{
    public bool IsSuccess { get; set; } = false;
    public int StatusCode { get; set; } = StatusCodes.Status100Continue;
    public object? Result { get; set; } = null;
    public IEnumerable<string> Messages { get; set; } = Enumerable.Empty<string>();
}