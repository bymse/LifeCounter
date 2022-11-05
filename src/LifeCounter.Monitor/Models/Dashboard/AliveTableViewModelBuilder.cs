using LifeCounter.DataLayer.LifeStore;

namespace LifeCounter.Monitor.Models.Dashboard;

public class AliveTableViewModelBuilder
{
    private readonly ILifeStoreRepository lifeStore;

    public AliveTableViewModelBuilder(ILifeStoreRepository lifeStore)
    {
        this.lifeStore = lifeStore;
    }

    public async Task<AliveTableViewModel> BuildAsync(Guid widgetId, string page)
    {
        var alive = await lifeStore.GetAliveAsync(widgetId, page);
        var lifeIds = new List<Guid>();
        var lifeEnds = new List<DateTime>();
        var props = new List<IReadOnlyDictionary<string, string>>();
        
        foreach (var lifeModel in alive)
        {
            lifeIds.Add(lifeModel.LifeId);
            lifeEnds.Add(lifeModel.LifeEnd.DateTime);
            props.Add(lifeModel.Properties);
        }

        return new AliveTableViewModel(
            new AliveTableColumn<Guid>(lifeIds),
            new AliveTableColumn<DateTime>(lifeEnds),
            new AliveTableColumn<IReadOnlyDictionary<string, string>>(props)
        );
    }
}