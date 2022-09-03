using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsStuff : MonoBehaviour
{
    [Min(0)] [SerializeField] private float _spawnPositionY;

    private RandomPosition _randomPosition;
    //private bool _isCatched = false;

    private void OnEnable()
    {
        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>(); // TODO: Check perfomence upon complition of development

        GetRandopPosition();
    }

    private void GetRandopPosition()
    {
        transform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
    }

    //public void TotalRefreshing()
    //{
    //    SetIsCatched(false);
    //    gameObject.SetActive(false);
    //    gameObject.SetActive(true);
    //}

    //public void SetIsCatched(bool turn)
    //{
    //    _isCatched = turn;
    //}

    //public bool GetIsChatched()
    //{
    //    return _isCatched;
    //}
}
