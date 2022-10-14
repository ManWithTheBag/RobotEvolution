using UnityEngine;
using UnityEngine.UI;

public class PlayerVeaponBigBlaze : VeaponBigBlaze
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
        _buttonShot.onClick.AddListener(SearchPlayersEnemy);

    }
    private void OnDisable()
    {
        _buttonShot.onClick.RemoveListener(SearchPlayersEnemy);
    }

    public override void Update()
    {
    }

    private void SearchPlayersEnemy()
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

    public override void Shoot(Transform enemyTransform)
    {
        if (CheckRayCast())
        {
            SetupVisualisatePosition(_hit.point, _hit.transform);
            ChangeScore(_hit.transform);
        }
        else
        {
            SetupVisualisatePosition(_missShotTransform.position, _blazeHitTransform);
        }

        PlayShotAnimation();
    }


    public override void FillButtonImage(float currentFillAmount)
    {
        _shotFiilB.SetCurrentValueFilled(currentFillAmount);
    }
}
