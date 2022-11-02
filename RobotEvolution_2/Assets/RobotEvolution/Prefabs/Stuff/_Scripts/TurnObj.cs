using UnityEngine;

public class TurnObj : MonoBehaviour
{
    private Vector3 _turnVector = Vector3.up;
    void Update()
    {
        transform.eulerAngles += _turnVector;
    }
}
