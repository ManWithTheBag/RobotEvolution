using UnityEngine;
using UnityEngine.UI;

public class PlayerVeaponWheeledBotCannon : VeaponWheelBotCannon
{
    private Button _buttonShot;
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
    private void OnEnable()
    {
        _buttonShot.onClick.AddListener(ScanEnemy);
    }
    private void OnDisable()
    {
        _buttonShot.onClick.RemoveListener(ScanEnemy);
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
