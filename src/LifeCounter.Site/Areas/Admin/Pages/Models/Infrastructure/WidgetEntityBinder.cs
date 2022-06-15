using LifeCounter.DataLayer.Db.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LifeCounter.Site.Areas.Admin.Pages.Models.Infrastructure;

public class WidgetEntityBinder : IModelBinder 
{
    private readonly ILifeCounterWidgetsRepository repository;

    public WidgetEntityBinder(ILifeCounterWidgetsRepository repository)
    {
        this.repository = repository;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var key = bindingContext.ModelName;
        var result = bindingContext.ValueProvider.GetValue(key);
        if (result == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = result.FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        var id = Guid.Parse(value);
        bindingContext.Result = ModelBindingResult.Success(repository.FindWidgetByInternalId(id));
        return Task.CompletedTask;
    }
}