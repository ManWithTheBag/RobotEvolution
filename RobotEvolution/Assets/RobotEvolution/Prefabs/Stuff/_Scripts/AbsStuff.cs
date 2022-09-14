using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AbsStuff : MonoBehaviour, IDetertor, IRefreshible
{
    [field: SerializeField] public ScoreDataSO ScoreDataSO { get; private set; }

    public Transform Soures { get ; set ; }

    protected Rigidbody _rb;
    protected Transform _thisTransform;

    private void Start()
    {
        _thisTransform = transform;
        TryGetComponent(out Rigidbody rigidbody); _rb = rigidbody;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsColliderDetactableObj(other, out var iDetectable))
        {
            SetScore(iDetectable);
        }

        TotalReshreshing();
    }

    private void OnCollisionEnter(Collision collision)
    {
        TotalReshreshing();
    }

    public abstract void SetScore(IDetectable iDetectable);


    private bool IsColliderDetactableObj(Collider collision, out IDetectable iDetectable)
    {
        iDetectable = collision.gameObject.GetComponentInParent<IDetectable>();
        return iDetectable != null;
    }

    public virtual void TotalReshreshing()
    {
        gameObject.SetActive(false);
        GlobalEventManager.OnSearchNewAim.Invoke();
        gameObject.SetActive(true);
    }
}
