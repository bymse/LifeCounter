using System.ComponentModel.DataAnnotations;

namespace LifeCounter.DataLayer.Db.Entity;

public enum TransportType
{
    [Display(Name = "HTTP")]
    Http = 0,
    
    [Display(Name = "SignalR")]
    SignalR = 1
}