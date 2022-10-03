using System.Collections.Generic;

public interface IAimsSelectable
{
    public List<IDistanceAimsComparable> GetEnemyVisibleList();
    public List<IDistanceAimsComparable> GetGearVisibleList();
    public List<IDistanceAimsComparable> GetBatteryVisibleList();
}
