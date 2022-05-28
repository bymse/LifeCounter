using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LifeCounter.DataLayer.Db.Entity;

public class Widget
{
    public Guid WidgetId { get; init; }
    
    [StringLength(512)]
    public string Title { get; init; }
    
    public Guid PublicUid { get; init; }
    
    public string OwnerId { get; init; }

    public IdentityUser Owner { get; init; } = null!;
}