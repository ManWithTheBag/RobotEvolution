using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ObjMovement), typeof(Rigidbody))]
public abstract class AbsCharacterBehaviourController : MonoBehaviour
{
public AnimationCurve AnimaCurvePushEffect { get; private set; }
    public ObjMovement ObjMovement { get; private set; }
    public float CurrentSpeedMove { get; private set; }
    public float CurrentSpeedRotation { get; private set; }
    public Vector3 CurrentDirectionalView { get; set; }
    public Vector3 CurrentDerectionalMove { get; set; }
    public CharacterModelStatsDataSO CharactersModelStatsDataSO { get; set; }

    protected Dictionary<Type, AbsCharacterBaseBehaviour> _charactersBehaviourDicionary;
    private AbsCharacterBaseBehaviour _currentCharacterBehaviour;

    public virtual void Awake()
    {
        TryGetComponent(out ObjMovement objMovement); ObjMovement = objMovement;
        StartInitCharacterBehaviours();
    }

    public void StartInitCharacterBehaviours()
    {
        InitCharactersBehaviour();
    }

    private void InitCharactersBehaviour()
    {
        _charactersBehaviourDicionary = new Dictionary<Type, AbsCharacterBaseBehaviour>();

        InitCommonBehaviours();
        InitSpeñialBehaviours();
    }
    private void InitCommonBehaviours()
    {
        _charactersBehaviourDicionary[typeof(CharacterBehaviourIdle)] = new CharacterBehaviourIdle(this);
        _charactersBehaviourDicionary[typeof(CharacterBehaviourBoost)] = new CharacterBehaviourBoost(this);
        _charactersBehaviourDicionary[typeof(CharacterBehaviourDeath)] = new CharacterBehaviourDeath(this);
    }
    public virtual void InitSpeñialBehaviours()
    {
        _charactersBehaviourDicionary[typeof(BotBehaviourRun)] = new BotBehaviourRun(this);
    }

    public void SetCurrentBehaviourControllerSetup(CharacterModelStatsDataSO characterModelStatsDataSO, Vector3 currentDirectionView, Vector3 currentDirectionMove)
    {
        CurrentSpeedMove = characterModelStatsDataSO.SpeedMovement;
        CurrentSpeedRotation = characterModelStatsDataSO.SpeedBodyRotation;
        CurrentDirectionalView = currentDirectionView;
        CurrentDerectionalMove = currentDirectionMove;
}
    
    public void SetBehaviourIdle()
    {
        AbsCharacterBaseBehaviour characterBehaviourIdle = GetBehaviourFromDictionary<CharacterBehaviourIdle>();
        SetNewCharacterBehaviour(characterBehaviourIdle);
    }
    public virtual void SetBehaviourRun()
    {
        AbsCharacterBaseBehaviour defoltState = GetBehaviourFromDictionary<BotBehaviourRun>();
        SetNewCharacterBehaviour(defoltState);
    }
    public void SetBehaviourBoost()
    {
        AbsCharacterBaseBehaviour characterBehaviourPullUp = GetBehaviourFromDictionary<CharacterBehaviourBoost>();
        SetNewCharacterBehaviour(characterBehaviourPullUp);
    }
    public void SetBehaviourDeath()
    {
        AbsCharacterBaseBehaviour characterBehaviourDeath = GetBehaviourFromDictionary<CharacterBehaviourDeath>();
        SetNewCharacterBehaviour(characterBehaviourDeath);
    }

    protected void SetNewCharacterBehaviour(AbsCharacterBaseBehaviour newCharacterState)
    {
        if (_currentCharacterBehaviour != null)
            _currentCharacterBehaviour.Exit();

        _currentCharacterBehaviour = newCharacterState;
        _currentCharacterBehaviour.Enter();
    }

    protected AbsCharacterBaseBehaviour GetBehaviourFromDictionary<T>() where T : AbsCharacterBaseBehaviour
    {
        Type type = typeof(T);
        return _charactersBehaviourDicionary[type];
    }

    private void Update()
    {
        if (_currentCharacterBehaviour == null)
            return;

        _currentCharacterBehaviour.Raning();
    }
}
