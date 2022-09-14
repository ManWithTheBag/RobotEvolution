using System.Collections.Generic;
using UnityEngine;

public class FiringRangeVisualisate : MonoBehaviour
{
    [SerializeField] private float _positionY;
    [SerializeField] private int _vertexCount = 12;

    private CharacterModelStatsDataSO _characterModelStatsDataSO;
    private LineRenderer _lineRenderer;
    private List<Vector3> _pointsVectorList = new();

    private Transform _pointsContainer, _pointMiddle, _pointLeft, _pointRight;
    private Transform _thisTransform;
    private void Awake()
    {
        _thisTransform = transform;
        TryGetComponent(out LineRenderer lineRenderer); _lineRenderer = lineRenderer;
        CreatePoints();
    }

    private void CreatePoints()
    {
        _pointsContainer = new GameObject("PointsContainer").transform;
        _pointMiddle = new GameObject("PointMiddle").transform;
        _pointMiddle.SetParent(_pointsContainer);
        _pointLeft = new GameObject("PointLeft").transform;
        _pointLeft.SetParent(_pointsContainer);
        _pointRight = new GameObject("PointRight").transform;
        _pointRight.SetParent(_pointsContainer);
        _pointsContainer.SetParent(transform);
        _pointsContainer.localPosition = Vector3.zero;
    }

    public void SetCharacterModelDataSO(CharacterModelStatsDataSO characterModelStatsDataSO)
    {
        _characterModelStatsDataSO = characterModelStatsDataSO;
        SetupPointsPosition();
    }

    private void Update()
    {
        CreateVertexCurve();
        RenderFiringRange();
    }

    private void SetupPointsPosition()
    {
        Vector3 vector3 = new Vector3(0f, _positionY, _characterModelStatsDataSO.ShotDistance);
        _pointMiddle.localPosition = vector3;
        float posotionX = (Mathf.Sin((_characterModelStatsDataSO.ViewAngle / 2) * Mathf.Deg2Rad)) * _characterModelStatsDataSO.ShotDistance;
        float positionZ = (Mathf.Cos(_characterModelStatsDataSO.ViewAngle / 2 * Mathf.Deg2Rad)) * _characterModelStatsDataSO.ShotDistance;
        _pointRight.localPosition = new Vector3(posotionX, _positionY, positionZ);
        _pointLeft.localPosition = new Vector3(-posotionX, _positionY, positionZ);
    }

    private void CreateVertexCurve()
    {
        _pointsVectorList.Clear();

        _pointsVectorList.Add(_thisTransform.position);
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / _vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(_pointLeft.position, _pointMiddle.position, ratio);
            var tangentLineVertex2 = Vector3.Lerp(_pointMiddle.position, _pointRight.position, ratio);
            var bezierpoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            _pointsVectorList.Add(bezierpoint);
        }
        _pointsVectorList.Add(_thisTransform.position);
    }
    private void RenderFiringRange()
    {
        _lineRenderer.positionCount = _pointsVectorList.Count;
        _lineRenderer.SetPositions(_pointsVectorList.ToArray());
    }
}
