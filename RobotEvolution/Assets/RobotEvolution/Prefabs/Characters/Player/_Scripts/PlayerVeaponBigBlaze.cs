using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVeaponBigBlaze : VeaponBigBlaze
{
    private Button _buttonShot;
    private DetectedAimForPlayer _detectedAimForPlayer;
    private ShotFiilB _shotFiilB;
    private Transform _missClickEnemy;

    private void Start()
    {
        GetButtonLinks();

        _thisTransform.TryGetComponent(out DetectedAimForPlayer detectedAimForPlayer); _detectedAimForPlayer = detectedAimForPlayer;
    }

    private void GetButtonLinks()
    {
        GameObject button = GameObject.Find("ShotB");
        _buttonShot = button.GetComponent<Button>();
        _shotFiilB = button.GetComponent<ShotFiilB>();

        _buttonShot.onClick.AddListener(SearchPlayersEnemy);

        _missClickEnemy = new GameObject("BigBlazeMissClickEnemy").transform;
        _missClickEnemy.SetParent(_turret);
        _missClickEnemy.localPosition = new Vector3(0, 0, MaxShootDistance);
    }

    public override void Update()
    {
    }

    private void SearchPlayersEnemy()
    {
        Transform enemyTransform = _detectedAimForPlayer.SearchPlayerEnemy(ViewAngleTurretAndVeapon, MaxShootDistance);
        if (enemyTransform != null)
        {
            _charactersAims.NearestAimEnemy = enemyTransform;

            TryToShoot(enemyTransform);
        }
        else
            TryToShoot(_missClickEnemy);
    }

    public override void TryToShoot(Transform enemyTransform)
    {
        if (_isRecharged)
        {
            FillButtonImage(0);

            _isRecharged = false;
            Shoot(enemyTransform);

            StartCoroutine(RechargingVeapon());
        }
    }

    public override void Shoot(Transform enemyTransform)
    {
        foreach (Transform item in _positionsVeaponStartLineRenderList)
        {
            VisualisateRayCast(item);
            ChangeScore(enemyTransform);
        }
    }

    public override void FillButtonImage(float currentFillAmount)
    {
        _shotFiilB.SetCurrentValueFilled(currentFillAmount);
    }
}
