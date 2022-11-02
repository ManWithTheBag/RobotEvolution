using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomNickname
{
    public string SetRandomNickname(NicknameDataSO nicknameDataSO)
    {
        List<string> nicknamesList = nicknameDataSO.AllNicknams.Split(',').ToList();
        int index = (int)Random.Range(0f, nicknamesList.Count);
        string randomNickname = nicknamesList[index].TrimStart(' ');

        while (randomNickname.Length > nicknameDataSO.MaxCountChurInNIcknamne)
        {
            index = (int)Random.Range(0f, nicknamesList.Count);
            randomNickname = nicknamesList[index].TrimStart(' ');
        }

        return randomNickname;
    }
}
