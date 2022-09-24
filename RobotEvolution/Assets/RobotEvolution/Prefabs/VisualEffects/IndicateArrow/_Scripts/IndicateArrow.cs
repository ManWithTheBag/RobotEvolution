using UnityEngine;

public class IndicateArrow : MonoBehaviour
{
    [SerializeField] public Transform IndicateArrowSpriteTransform { get; private set; }
    [SerializeField] private Transform _arrowTransform;
    [SerializeField] private GameObject _indicatiArrowSpritePrefab;
    [SerializeField] private float _speedArrowRotation;
    [SerializeField] private float _maxDistanceIndicateArrowDeteted;

    private IndicateArrowColorController _indicateArrowColorController;
    private Transform _indicateArrowContainer;
    private Transform _currentEnemyTransform;
    private Transform _thisTransform;
    private float _currentDistanceToEnemy;

    private void Awake()
    {
        _thisTransform = transform;
        _indicateArrowContainer = GameObject.Find("IndicateArrowContainer").transform;
    }

    #region Create and setup ArrowSprite
    private void Start()
    {
        CreateIndicateArrowSpprite();
    }

    private void CreateIndicateArrowSpprite()
    {
        IndicateArrowSpriteTransform = Instantiate(_indicatiArrowSpritePrefab, _indicateArrowContainer).transform;
        _indicateArrowColorController = IndicateArrowSpriteTransform.GetComponent<IndicateArrowColorController>();
    }

    public void SetDistanceModelToArrow(float distance)
    {
        _arrowTransform.localPosition = new Vector3(0f, 0f, distance);
    }
    #endregion

    #region Setup ArrowSprite movement
    public void SetCurrentEnemy(Transform enemyTransform)
    {
        _currentEnemyTransform = enemyTransform;
    }

    private Quaternion SetCurrentArrowVieww()
    {
        Vector3 targetDirection = _currentEnemyTransform.position - _thisTransform.position;
        return CulculatQuaternArrowView(targetDirection);
    }

    private Quaternion CulculatQuaternArrowView(Vector3 targetDirection)
    {
        float relativeAngle = Mathf.Atan2(targetDirection.normalized.x, targetDirection.normalized.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, relativeAngle, 0f);
    }
    #endregion

    #region ArrowSprite Update controling

    private void Update()
    {


        if (_currentEnemyTransform == null)
            return;

        CheckCurrentDistanceToEnemy();

        if (_currentDistanceToEnemy <= _maxDistanceIndicateArrowDeteted)
        {
            SetupArrowSoritePosition();
            SetIndicateArrowColorValue();
            MoveArrow();
        }

    }
    private void SetupArrowSoritePosition()
    {
        IndicateArrowSpriteTransform.position = _arrowTransform.position;
        IndicateArrowSpriteTransform.rotation = _arrowTransform.rotation;
    }

    private void CheckCurrentDistanceToEnemy()
    {
        _currentDistanceToEnemy = Vector3.Distance(_thisTransform.position, _currentEnemyTransform.position);
    }


    private void MoveArrow()
    {
        _thisTransform.rotation = Quaternion.Lerp(_thisTransform.rotation, SetCurrentArrowVieww(), Time.deltaTime * _speedArrowRotation);
    }

    private void SetIndicateArrowColorValue()
    {
        _indicateArrowColorController.SetCurrentColorDistance(_currentDistanceToEnemy / _maxDistanceIndicateArrowDeteted);
    }
    #endregion
}