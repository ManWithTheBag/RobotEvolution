using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerApprovalWorlds : MonoBehaviour
{
    [SerializeField] private int _persentActivateApprovalWord;

    private ApprovelWordsFields _approvelWordsFields;
    private string _startingApprovalWord = "Go!";
    private string _allAprovalWords = "Wonderfull, COOL, Fantastic, Amazing";
    private ScoreCalculation _scoreCalculation;
    private bool _isStastIndicator = true;

    private void Awake()
    {
        _scoreCalculation = GetComponent<ScoreCalculation>();
        _approvelWordsFields = GameObject.Find("UiController").GetComponent<ApprovelWordsFields>();
    }

    private void OnEnable()
    {
        _scoreCalculation.AddScoreEvent += ApprovalWordAppear;
    }
    private void OnDisable()
    {
        _scoreCalculation.AddScoreEvent -= ApprovalWordAppear;
    }

    private void ApprovalWordAppear()
    {
        if(_isStastIndicator)
        {
            _isStastIndicator = false;
            _approvelWordsFields.ApprovelWordsText.text = _startingApprovalWord;
            _approvelWordsFields.ApprovalWordAnimator.PlayeApprovalWordAppear();
        }
            
        else if(RandomValue())
        {
            SetRandomApprovalWord();
            _approvelWordsFields.ApprovalWordAnimator.PlayeApprovalWordAppear();
        }
    }

    private bool RandomValue()
    {
        return Random.Range(0, 100) < _persentActivateApprovalWord ? true : false;
    }

    private void SetRandomApprovalWord()
    {
        List<string> approvalWordsList = _allAprovalWords.Split(',').ToList();
        int index = (int)Random.Range(0f, approvalWordsList.Count);
        string randomApprovalWord = approvalWordsList[index].TrimStart(' ');

        _approvelWordsFields.ApprovelWordsText.text = randomApprovalWord;
    }
}
