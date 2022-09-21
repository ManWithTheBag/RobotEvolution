using UnityEngine;

public abstract class AbsStuff : MonoBehaviour, IRefreshible
{
    [Min(0)] [SerializeField] private float _spawnPositionY;
    
    protected Transform _thisTransform;

    private RandomPosition _randomPosition;

    public virtual void Awake()
    {
        _randomPosition = GameObject.Find("ObjController").GetComponent<RandomPosition>();
        _thisTransform = transform;
        _thisTransform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
    }

    private void Start()
    {
        //_thisTransform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
    }

    public virtual void TotalReshreshing()
    {
        gameObject.SetActive(false);
        transform.position = _randomPosition.GetRandomPosition(_spawnPositionY);
        GlobalEventManager.SearchNewAimEvent.Invoke();
        gameObject.SetActive(true);
    }
}
