using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Models;

public class JsResult : ContentResult
{
    public JsResult(string js)
    {
        Content = js;
        ContentType = "text/javascript";
    }
}