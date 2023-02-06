using AutoMapper;

namespace Calendars.Resources.Extensions;
/// <summary>
///     AutoMapper extensions.
/// </summary>
internal static class AutoMapperExtensions
{
    internal static void CreateDoubleLinkedMap<T1, T2>(this IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<T1, T2>();
        mapper.CreateMap<T2, T1>();
    }
}