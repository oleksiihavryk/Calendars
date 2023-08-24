﻿using Calendars.Proxy.Domain;

namespace Calendars.Proxy.RefactoredCore.Interfaces;

/// <summary>
///     Service for access to events resources.
/// </summary>
public interface IEventsService
{
    public Task<HttpResponseMessage> GetByIdAsync(string id);
    public Task<HttpResponseMessage> SaveAsync(Event @event);
    public Task<HttpResponseMessage> UpdateAsync(Event @event);
    public Task<HttpResponseMessage> DeleteAsync(string id);
}