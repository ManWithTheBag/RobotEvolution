using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_SearchAim : MonoBehaviour
{
    public void OnSearchAim()
    {
        GlobalEventManager.OnSearchNewAim.Invoke();
    }
}
