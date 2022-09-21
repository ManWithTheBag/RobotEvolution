using UnityEngine;

[RequireComponent(typeof(SphereCollider)), RequireComponent(typeof(Rigidbody))]
public class CharacterChangeScore : MonoBehaviour
{
    private ScoreCalculation _scoreCalculation;
    private AbsShield _shield;

    private void Awake()
    {
        _scoreCalculation = GetComponentInParent<ScoreCalculation>();
        _shield = transform.parent.GetComponentInChildren<AbsShield>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTriggerDamagingObj(other, out IDamaging iDamaging))
        {
            ScoreChangedLossScore(iDamaging.ScoreLossTarget());
            iDamaging.HitToSomeone();

            if (iDamaging.SouresCharacter() != null)
            {
                iDamaging.SouresCharacter().GetComponent<ScoreCalculation>().AddScore(iDamaging.ScoreAddSoures());
            }
        }

        if (IsTriggerAddScoreObj(other, out IAddScore iAddScore))
            ScoreChangedAddScore(iAddScore.ScoreAdd());

        if (IsTriggerChargableObj(other, out IChargeable iChargable))
            _shield.SetFullEnergy();

        if(IsTriggerRefreshibleObj(other, out IRefreshible iRefreshible))
            iRefreshible.TotalReshreshing();
            
    }


    private bool IsTriggerDamagingObj(Collider other, out IDamaging iDamaging)
    {
        iDamaging = other.GetComponent<IDamaging>();
        return iDamaging != null;
    }
    private bool IsTriggerAddScoreObj(Collider other, out IAddScore iAddScore)
    {
        iAddScore = other.GetComponent<IAddScore>();
        return iAddScore != null;
    }
    private bool IsTriggerChargableObj(Collider other, out IChargeable iChargable)
    {
        iChargable = other.GetComponentInParent<IChargeable>();
        return iChargable != null;
    }
    private bool IsTriggerRefreshibleObj(Collider other, out IRefreshible iRefreshible)
    {
        iRefreshible = other.GetComponent<IRefreshible>();
        return iRefreshible != null;
    }


    public void ScoreChangedAddScore(int score)
    {
       _scoreCalculation.AddScore(score);
    }

    public void ScoreChangedLossScore(int score)
    {
        _scoreCalculation.LossScore(score);
    }
}
