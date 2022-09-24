using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerApprovalWorlds : MonoBehaviour
{
    [SerializeField] private float _timeActiveApprovalWord;

    private ApprovelWordsFields _approvelWordsFields;
    private string _allAprovalWords = "Wonderfull, COOL, Fantastic, Amazing";
    private void Awake()
    {
        _approvelWordsFields = GameObject.Find("UiController").GetComponent<ApprovelWordsFields>();
    }

    private void OnEnable()
    {
        //TODO: Add event which will by activate approval word
    }
    private void OnDisable()
    {
        //TODO: Remove event which will by activate approval word
    }

    private void ApprovalWordAppear()
    {
        SetRandomApprovalWord();
        _approvelWordsFields.ApprovalWordAnimator.PlayeApprovalWordAppear();
    }

    private void SetRandomApprovalWord()
    {
        List<string> approvalWordsList = _allAprovalWords.Split(',').ToList();
        int index = (int)Random.Range(0f, approvalWordsList.Count);
        string randomApprovalWord = approvalWordsList[index].TrimStart(' ');

        _approvelWordsFields.ApprovelWordsText.text = randomApprovalWord;
    }

    private IEnumerator TimerActiveApprovaWord()
    {
        yield return new WaitForSeconds(_timeActiveApprovalWord);
        ApprovalWordHide();
    }

    private void ApprovalWordHide()
    {
        _approvelWordsFields.ApprovalWordAnimator.PlayApprovalWordHide();
    }
}
