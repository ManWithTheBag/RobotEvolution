using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectable
{
    public void DetectedAddScore(int score);
    public void DetectedLossScore(int score);
}
