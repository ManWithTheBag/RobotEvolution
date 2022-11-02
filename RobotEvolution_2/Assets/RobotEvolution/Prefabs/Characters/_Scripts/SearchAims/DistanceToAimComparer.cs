using System.Collections.Generic;

public class DistanceToAimComparer : IComparer<IDistanceAimsComparable>
{
    public int Compare(IDistanceAimsComparable x, IDistanceAimsComparable y)
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
