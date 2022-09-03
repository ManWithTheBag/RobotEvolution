using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjMovement), typeof(Rigidbody))]
public abstract class AbsCharacterBehaviourController : MonoBehaviour
{
    [field: SerializeField] public CharacterMovementDataSO CharacterMovementDataSO { get; private set; }
    [field: SerializeField] public AnimationCurve AnimaCurvePushEffect { get; private set; }

    [SerializeField] protected Transform _aimTransform; // TODO: delet SerializeField after debaging

    public ObjMovement ObjMovement { get; private set; }
    public Transform AimTransform
    {
        get { return _aimTransform; }
        set
        {
            if (value != null)
                _aimTransform = value;
            else
                _aimTransform = transform.parent;
        }
    }
    public Transform ThisCharacterTranshorm { get; private set; }
    public ICharacter ICharacter { get; private set; }

    protected Dictionary<Type, AbsCharacterBaseBehaviour> _charactersBehaviourDicionary;
    private AbsCharacterBaseBehaviour _currentCharacterBehaviour;

    public virtual void Start()
    {
        TryGetComponent(out ICharacter iCharacter); ICharacter = iCharacter;
        TryGetComponent(out ObjMovement objMovement); ObjMovement = objMovement;
        ThisCharacterTranshorm = transform;

        InitCharactersBehaviour();

        SetDefoltBehaviour();
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
        _charactersBehaviourDicionary[typeof(CharacterBehaviourPullUp)] = new CharacterBehaviourPullUp(this);
        _charactersBehaviourDicionary[typeof(CharacterBehaviourDeath)] = new CharacterBehaviourDeath(this);
    }
    public virtual void InitSpeñialBehaviours()
    {
        _charactersBehaviourDicionary[typeof(BotBehaviourRun)] = new BotBehaviourRun(this);
    }


    private void SetDefoltBehaviour()
    {
        SetBehaviourRun();
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
    public void SetBehaviourPoolUp()
    {
        AbsCharacterBaseBehaviour characterBehaviourPullUp = GetBehaviourFromDictionary<CharacterBehaviourPullUp>();
        SetNewCharacterBehaviour(characterBehaviourPullUp);
    }
    public void SetBehaviourDeath()
    {
        AbsCharacterBaseBehaviour characterBehaviourDeath = GetBehaviourFromDictionary<CharacterBehaviourDeath>();
        SetNewCharacterBehaviour(characterBehaviourDeath);
    }
}
