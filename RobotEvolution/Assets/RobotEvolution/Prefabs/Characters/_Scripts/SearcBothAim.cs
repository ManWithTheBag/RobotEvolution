using System.Collections.Generic;
using UnityEngine;

public class SearcBothAim : MonoBehaviour
{
    [SerializeField] private MapsController _mapsComtvroller;

    [SerializeField] private PoolPlayer _poolPlayer;
    [SerializeField] private PoolSimpleBots _poolSimpleBots;
    //[SerializeField] private PoolAttackPlayerBot _poolAttackPlayer;
    [SerializeField] private PoolGear _poolGear;
    //[SerializeField] private PoolHelmet _poolHelmet;
    //[SerializeField] private PollCuirass _pollCuirass;


    private Vector3 _mainPosition;
    private float _sqrDistance;
    private float _sqrCloseDistance;
    private Transform _aimTransform;
    //private StuffStatusController _stuffStatusController;
    private bool _isScaleLess = false;

    //Unity Events Region
    #region
    private void OnEnable()
    {
        GlobalEventManager.OnSearchNewAim.AddListener(SelectSearchingBotList);
    }
    private void OnDisable()
    {
        GlobalEventManager.OnSearchNewAim.RemoveListener(SelectSearchingBotList);
    }
    #endregion

    private void SelectSearchingBotList()
    {
        SelectElementIntoBotList<SimpleBot>(_poolSimpleBots.WholeSimpleBotList);
        //SelectElementIntoBotList<AttackPlayerBot>(_poolAttackPlayer.WholeAttackPlayerBotList);
    }
    private void SelectElementIntoBotList<T>(List<T> seardhingList) where T : AbsCharacter
    {
        foreach (AbsCharacter characterElement in seardhingList)
        {
            _mainPosition = characterElement.gameObject.transform.position;
            _sqrCloseDistance = _mapsComtvroller._sqrSizeFiteMap;
           // characterElement.gameObject.TryGetComponent(out StuffStatusController stuffStatusController);
            //_stuffStatusController = stuffStatusController;
            SelectSearchList(characterElement);

            CheckWhuIsBigger(characterElement);

            GetAimTransform(characterElement);
        }
    }

    private void SelectSearchList(AbsCharacter characterElement)
    {

        SearchAimInSelectedList<Gear>(_poolGear.WholeGearsList, characterElement);
        
        if (_isScaleLess == false)
        {
            SearchAimInSelectedList<Player>(ActiveCharactersInList.GetActiveCharactersInList<Player>(_poolPlayer.WholePlayerList), characterElement);
            SearchAimInSelectedList<SimpleBot>(ActiveCharactersInList.GetActiveCharactersInList<SimpleBot>(_poolSimpleBots.WholeSimpleBotList), characterElement);
            // SearchAimInSelectedList<AttackPlayerBot>(ValidCharacterLists.GetValidCharacterList<AttackPlayerBot>(_poolAttackPlayer.WholeAttackPlayerBotList), characterElement);
        }

        //if (!_stuffStatusController.HelmetMesh.activeInHierarchy)
        //    SearchAimInSelectedList<Helmet>(_poolHelmet.WholeHelmetList, characterElement);

        //if (!_stuffStatusController.CuirassMesh.activeInHierarchy)
        //    SearchAimInSelectedList<Cuirass>(_pollCuirass.WholeCuirasList, characterElement);
    }
    private void SearchAimInSelectedList<K>(List<K> list, AbsCharacter characterElement) where K : MonoBehaviour
    {
        foreach (var item in list)
        {
            _sqrDistance = CalculationSqrDistance(item.gameObject.transform.position);

            if (_sqrCloseDistance > _sqrDistance && characterElement != item)
            {
                _sqrCloseDistance = _sqrDistance;
                _aimTransform = item.gameObject.transform;
            }
        }
    }
    private float CalculationSqrDistance(Vector3 aimPosition)
    {
        Vector3 localOffset = aimPosition - _mainPosition;
        return localOffset.sqrMagnitude;
    }


    private void CheckWhuIsBigger(AbsCharacter characterElement)
    {
        //if (_aimTransform.gameObject.TryGetComponent(out ICharacter aimICharacter))
        //{
        //    if (characterElement.Scale <= aimICharacter.Scale)
        //    {
        //        _isScaleLess = true;
        //        _sqrCloseDistance = _mapsComtvroller._sqrSizeFiteMap;
        //        SelectSearchList(characterElement);
        //    }
        //}
    }


    private void GetAimTransform<G>(G element) where G : AbsCharacter
    {
        element.TryGetComponent(out AbsCharacterBehaviourController absMoveController);
        absMoveController.AimTransform = _aimTransform;
        _isScaleLess = false;
    }
}
