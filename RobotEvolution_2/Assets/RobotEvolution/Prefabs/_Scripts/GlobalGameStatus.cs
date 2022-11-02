using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameStatus : MonoBehaviour
{
    public static GlobalGameStatus Instance { get; private set; }
    public static bool t_FirstStartGame = true;
    
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(this.gameObject);
    }


}
