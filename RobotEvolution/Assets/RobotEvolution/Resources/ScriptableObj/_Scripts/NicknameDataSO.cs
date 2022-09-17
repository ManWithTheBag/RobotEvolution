using UnityEngine;

[CreateAssetMenu(fileName = "NicknameDataSO", menuName = "Scriptable Object/NicknameDataSO", order = 54)]
public class NicknameDataSO : ScriptableObject
{
    private string _allNicknams = "DEATH MACHINE, Dead show, Psycho killer, Bad soldier, Agent47, Death gun, Terminator, Thunderbeast, Dark Warrior, Assassin, Brookie, Unlucky, SpaceKirby, ClashFlash, VoteMeUGae, fruitsnack, Rosa Parks, MrKilling, Kissers, Sangwoo, touchmypp, flextape, Chucky, Kenpachi, Lekrimenator, Jeneva, Willow, DEADWOOD, PREDATOR, Flex, EZA, Dzirt, Crematoria, Katsu, M1racleee, bloodhammer, babytape, awful, whitenova, takeo, 2dtyan, aomori, ladyheadshot, risto, eternalsadness, lexar, loki, psychooutrage, hikkimaru, darkvampire, XXXghoul, mimimi, monochrome, bestwest, blodrayne, nyankitty, waytoosexy, voldemortred, sweetylyx, astrobabe, badkarma, powerpuff, lilbitch, freakygirl, nastyone, rudeboy, venom, flameguard, IDK, babydoodle, heyyou, knuckles, pewpew, Quantum";
    public string AllNicknams
    {
        get { return _allNicknams; }
    }

    [field: SerializeField] public GameObject NicknamePrefab { get; private set; }
    [field: SerializeField] public int MaxCountChurInNIcknamne { get; private set; }
    [field: SerializeField] public float TimeLearpingScale { get; private set; }
    [field: SerializeField] public float MaxDistanceVisibleNickname { get; private set; }
}
