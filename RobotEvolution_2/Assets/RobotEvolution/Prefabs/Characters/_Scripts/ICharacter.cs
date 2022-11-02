using System;
using UnityEngine;

public interface ICharacter
{
    public int Score { get; set; }
    public int Level { get; set; }
    public string Nickname { get; set; }
    public int IndexPositionInLIderBoard { get; set; }

    public event Action CharacterRefreshedEvent;
}


