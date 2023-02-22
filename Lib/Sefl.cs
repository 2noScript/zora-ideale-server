using Microsoft.AspNetCore.Mvc;
using NoScrip.Models;
namespace NoScrip.Lib;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class ApiV1Attribute : RouteAttribute
{
    public ApiV1Attribute(string template)
    : base($"api/v1/{template}")
    {
        ArgumentNullException.ThrowIfNull(template);
    }
}




public static class Functions
{
    public static int TotalPage(int count, int page, int limit)
    {
        return count % limit == 0 ?
                 ((int)count) / limit :
                 ((int)count) / limit + 1;
    }
}