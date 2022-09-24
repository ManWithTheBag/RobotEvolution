using System.Collections.Generic;

public class DistanceToAimComparer : IComparer<IDistanceToAimQuikSortable>
{
    public int Compare(IDistanceToAimQuikSortable x, IDistanceToAimQuikSortable y)
    {
        if (x.SortDistanceAimToCharacter != y.SortDistanceAimToCharacter)
        {
            if (x.SortDistanceAimToCharacter > y.SortDistanceAimToCharacter)
                return 1;
            if (x.SortDistanceAimToCharacter < y.SortDistanceAimToCharacter)
                return -1;
            else
                return 0;
        }
        else
            return -1;
    }
}
