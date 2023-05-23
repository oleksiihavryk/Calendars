﻿using Calendars.Proxy.Domain;

namespace Calendars.Proxy.Core.Interfaces;
/// <summary>
///     Service for access to calendars resources.
/// </summary>
public interface ICalendarsResourcesService
{
    public Task<HttpResponseMessage> GetAllByUserIdAsync(string userId);
    public Task<HttpResponseMessage> GetByIdAsync(Guid id);
    public Task<HttpResponseMessage> SaveAsync(Calendar calendar);
    public Task<HttpResponseMessage> UpdateAsync(Calendar calendar);
    public Task<HttpResponseMessage> DeleteAsync(Guid id);
}