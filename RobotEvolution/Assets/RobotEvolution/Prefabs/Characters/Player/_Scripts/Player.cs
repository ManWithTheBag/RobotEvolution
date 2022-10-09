using UnityEngine;
using System.Collections;

public class Player : AbsCharacter
{
    private void Start()
    {
        StartCoroutine(DeactivatePlayerForStart());
    }

    private IEnumerator DeactivatePlayerForStart()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
