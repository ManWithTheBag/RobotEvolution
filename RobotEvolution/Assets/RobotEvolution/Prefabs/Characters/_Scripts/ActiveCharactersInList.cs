using System.Collections.Generic;
using UnityEngine;

public static class ActiveCharactersInList
{
    public static List<T> GetActiveCharactersInList<T>(List<T> caractersList) where T : MonoBehaviour
    {
        List<T> temparoryList = new List<T>();
        foreach (var item in caractersList)
        {
            if (!item.gameObject.tag.Equals("Died"))
            {
                temparoryList.Add(item);
            }
        }

        return temparoryList;
    }
}
