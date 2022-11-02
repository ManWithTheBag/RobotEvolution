using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisibleInvesibleCharacter : MonoBehaviour
{
    private Transform _thisTransform;

    private List<IVisibleInvisible> _iVisibleInvisibleCharacterList = new();
    private List<IVisibleInvisible> _temparoryList = new();
    private void Start()
    {
        _thisTransform = transform;

        GetLinkForClasslist();
    }

    private void GetLinkForClasslist()
    {
        Transform perent = _thisTransform.parent;
        while (true)
        {
            _temparoryList = perent.GetComponents<IVisibleInvisible>().ToList();

            if (_temparoryList.Count > 0)
            {
                _iVisibleInvisibleCharacterList.AddRange(_temparoryList);
                perent = perent.parent;
            }
            else
            {
                perent = perent.parent;
                if (perent == _thisTransform.root)
                    break;
            }

            _temparoryList.Clear();
        }
    }

    public void OnBecameVisible()
    {
        foreach (var item in _iVisibleInvisibleCharacterList)
        {
            item.SetVisibleStatusObj(true);
        }
    }
    public void OnBecameInvisible()
    {
        foreach (var item in _iVisibleInvisibleCharacterList)
        {
            item.SetVisibleStatusObj(false);
        }
    }
}
