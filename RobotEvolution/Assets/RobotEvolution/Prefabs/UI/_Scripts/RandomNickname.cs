using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomNickname
{
    public string SetRandomNickname(NicknameDataSO nicknameDataSO)
    {
        List<string> arryNicknames = nicknameDataSO.AllNicknams.Split(',').ToList();
        int index = (int)Random.Range(0f, arryNicknames.Count);
        string randomNickname = arryNicknames[index].TrimStart(' ');

        while (randomNickname.Length > nicknameDataSO.MaxCountChurInNIcknamne)
        {
            index = (int)Random.Range(0f, arryNicknames.Count);
            randomNickname = arryNicknames[index].TrimStart(' ');
        }

        return randomNickname;
    }
}
