using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersAims : MonoBehaviour
{
    private Transform _nearestAimStuff;

    public Transform NearestAimStuff
    {
        get { return _nearestAimStuff; }
        set 
        {
            if (_nearestAimStuff != null)
                _nearestAimStuff = value;
            else
                _nearestAimStuff = _thisTransform.root;
    }
    }

    private Transform _nearestAimEnemy;

    public Transform NearestAimEnemy
    {
        get { return _nearestAimEnemy; }
        set 
        {
            if (_nearestAimEnemy != null)
                _nearestAimEnemy = value;
            else
                _nearestAimEnemy = _thisTransform.root;
        }
    }
        

    private SearchBotsAimStuff _searchBotsAimStuff;
    private SearchBotsAimEnemy _searchBotsAimEnemy;
    private Transform _thisTransform;

    private void OnEnable()
    {
        GlobalEventManager.OnSearchNewAim.AddListener(OnGetBotAims);
    }
    private void OnDisable()
    {
        GlobalEventManager.OnSearchNewAim.RemoveListener(OnGetBotAims);
    }
    private void Start()
    {
        _thisTransform = transform;
        _searchBotsAimStuff = GetLinkSearchBotAimClass<SearchBotsAimStuff>();
        _searchBotsAimEnemy = GetLinkSearchBotAimClass<SearchBotsAimEnemy>();
    }

    private T GetLinkSearchBotAimClass<T>() where T : AbsSearcBotshAim
    {
        Transform perent = _thisTransform.parent;
        while (true)
        {
            if (!perent.TryGetComponent(out T searchBotsAimClass))
            {
                perent = perent.parent;
                if (perent == _thisTransform.root)
                {
                    Debug.LogError($"CharacterController hevent scripts {typeof(SearchBotsAimEnemy)}; Add this script to CharacterController");
                }
            }
            else
            {
                return searchBotsAimClass;
            }
        }
    }

    private void OnGetBotAims()
    {
        NearestAimStuff = _searchBotsAimStuff.GetNecessaryNearestAim(_thisTransform);
        NearestAimEnemy = _searchBotsAimEnemy.GetNecessaryNearestAim(_thisTransform);
    }
}
