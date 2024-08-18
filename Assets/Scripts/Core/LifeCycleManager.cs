
using System.Collections.Generic;
using System.Diagnostics;

public class LifeCycleManager : Singleton<LifeCycleManager>, ILifeCycle
{
    private readonly List<ILifeCycle> Items = new();

    public void Register(ILifeCycle item)
    {
        Items.Add(item);
    }

    public void BeforeStage()
    {
        Items.ForEach((item) => item.BeforeStage());
    }

    public void EndStage()
    {
        Items.ForEach((item) => item.EndStage());
    }

    public void EndTurn()
    {
        Items.ForEach((item) => item.EndTurn());
    }

    public void StartStage()
    {
        Items.ForEach((item) => item.StartStage());
    }

    public void StartTurn()
    {
        Items.ForEach((item) => item.StartTurn());
    }
}