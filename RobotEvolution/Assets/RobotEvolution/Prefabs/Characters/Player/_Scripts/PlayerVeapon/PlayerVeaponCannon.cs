using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVeaponCannon : VeaponCannon
{
    private Button _buttonShot;
    //private CharacterScanningAims _characterScanningAims;
    private ShotFiilB _shotFiilB;

    public override void Awake()
    {
        GetButtonLinks();

        //_thisTransform.TryGetComponent(out CharacterScanningAims characterScanningAims); _characterScanningAims = characterScanningAims;
        
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
        _buttonShot.onClick.AddListener(ScanEnemy);
        base.OnEnable();
    }
    public override void OnDisable()
    {
        _buttonShot.onClick.RemoveListener(ScanEnemy);
        base.OnDisable();
    }

    public override void Update()
    {
    }

    private void ScanEnemy()
    {
        _charactersAims.ScanningEnemyVisibleList();

        TryToShoot(_charactersAims.NearestEnemy);
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
