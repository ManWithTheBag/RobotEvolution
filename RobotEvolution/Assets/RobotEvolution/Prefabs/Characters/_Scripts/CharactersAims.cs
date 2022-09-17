using UnityEngine;

public class CharactersAims : MonoBehaviour
{
    [SerializeField] private Transform _nearestAimStuff;

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

    [SerializeField] private Transform _nearestAimEnemy;

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

    public float DistanceToEnemy { get; private set; }

    private SearchBotsAimStuff _searchBotsAimStuff;
    private SearchBotsAimEnemy _searchBotsAimEnemy;
    private Transform _thisTransform;
    


    private void OnEnable()
    {
        GlobalEventManager.SearchNewAimEvent.AddListener(OnGetBotAims);
    }
    private void OnDisable()
    {
        GlobalEventManager.SearchNewAimEvent.RemoveListener(OnGetBotAims);
    }
    private void Start()
    {
        _thisTransform = transform;
        _searchBotsAimStuff = GetLinkSearchBotAimClass<SearchBotsAimStuff>();
        _searchBotsAimEnemy = GetLinkSearchBotAimClass<SearchBotsAimEnemy>();

        OnGetBotAims();
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
                    Debug.LogError($"LoogError: CharacterController hevent scripts {typeof(SearchBotsAimEnemy)}; Add this script to CharacterController");
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
        _nearestAimStuff = _searchBotsAimStuff.GetNecessaryNearestAim(_thisTransform);
        _nearestAimEnemy = _searchBotsAimEnemy.GetNecessaryNearestAim(_thisTransform);
    }

    private void Update()
    {
        DistanceToEnemy = Vector3.Distance(_thisTransform.position, NearestAimEnemy.position);
    }
}
