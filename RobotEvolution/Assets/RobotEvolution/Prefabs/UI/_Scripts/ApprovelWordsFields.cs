using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApprovelWordsFields : MonoBehaviour
{
    [field:SerializeField] public GameObject ApprovalWordObj { get; private set; }
    [field:SerializeField]public TextMeshProUGUI ApprovelWordsText { get; private set; } 
    [field:SerializeField]public ApprovalWordAnimator ApprovalWordAnimator { get; private set; }
}
