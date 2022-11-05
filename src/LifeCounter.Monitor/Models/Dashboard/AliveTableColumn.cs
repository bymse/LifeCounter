using System.Collections.ObjectModel;

namespace LifeCounter.Monitor.Models.Dashboard;

public class AliveTableColumn<T> : ReadOnlyCollection<T>
{
    public AliveTableColumn(IList<T> list) : base(list)
    {
    }
}