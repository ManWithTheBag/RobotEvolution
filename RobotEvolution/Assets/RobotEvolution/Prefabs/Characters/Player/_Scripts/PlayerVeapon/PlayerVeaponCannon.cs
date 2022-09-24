using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVeaponCannon : VeaponCannon
{
    private Button _buttonShot;
    private PlayerDetectedAim _playerDetectedAim;
    private ShotFiilB _shotFiilB;

    public override void Awake()
    {
        GetButtonLinks();

        base.Awake();
    }
    private void GetButtonLinks()
    {
        GameObject button = GameObject.Find("ShotB");
        _buttonShot = button.GetComponent<Button>();
        _shotFiilB = button.GetComponent<ShotFiilB>();
    }
    public override void OnEnable()
    {
        _buttonShot.onClick.AddListener(SearchPlayersEnemy);

        base.OnEnable();
    }
    public override void OnDisable()
    {
        _buttonShot.onClick.RemoveListener(SearchPlayersEnemy);
        base.OnDisable();
    }

    private void Start()
    {
        _thisTransform.TryGetComponent(out PlayerDetectedAim detectedAimForPlayer); _playerDetectedAim = detectedAimForPlayer;
    }

    public override void Update()
    {
    }

    private void SearchPlayersEnemy()
    {
        Transform enemyTransform = _playerDetectedAim.SearchPlayerEnemy(ViewAngleTurretAndVeapon, MaxShootDistance);

        if (enemyTransform != null)
            _charactersAims.NearestAimEnemy = enemyTransform;

        TryToShoot(enemyTransform);
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

    public override void FillButtonImage(float currentFillAmount)
    {
        _shotFiilB.SetCurrentValueFilled(currentFillAmount);
    }
}
