namespace LifeCounter.Site.Extensions;

public static class HttpContextExtensions
{
    public static bool IsInArea(this HttpContext context, string areaName)
    {
        var area = context.Request.RouteValues["area"]?.ToString();
        return areaName.Equals(area, StringComparison.InvariantCultureIgnoreCase);
    }
}