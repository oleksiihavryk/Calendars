﻿using Calendars.Proxy.Domain;

namespace Calendars.Proxy.RefactoredCore.Interfaces;

/// <summary>
///     Service for access to days resources.
/// </summary>
public interface IDaysService
{
    public Task<HttpResponseMessage> GetByIdAsync(string id);
    public Task<HttpResponseMessage> SaveAsync(Day day);
    public Task<HttpResponseMessage> UpdateAsync(Day day);
    public Task<HttpResponseMessage> DeleteAsync(string id);
}