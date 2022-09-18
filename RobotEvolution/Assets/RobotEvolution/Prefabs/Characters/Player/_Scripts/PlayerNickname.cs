using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerNickname : AbsCharacterNickname
{
    [SerializeField] private string _defoltNickname;
    private TMP_InputField _nicknameInputField;

    public override void Awake()
    {
        SetupNicknameInputField();
        _nicknameInputField.onEndEdit.AddListener(CheckInputFieldOnTheNewNickname);

        base.Awake();
    }

    private void SetupNicknameInputField()
    {
        _nicknameInputField = GameObject.Find("PlayerNicknameIF").GetComponent<TMP_InputField>();

        _nicknameInputField.textComponent.text = "Player"; // TODO: change on the saving player's nickname..not working
        _nicknameInputField.characterLimit = _nicknameDataSO.MaxCountChurInNIcknamne;
    }

    private void CheckInputFieldOnTheNewNickname(string newNickname)
    {
        _thisNickname = newNickname;
    }

    public override void SetNickname()
    {
        _thisNickname = _nicknameInputField.textComponent.text;
        _absCharacter.Nickname = _thisNickname;
        _textNicknameScore.text = _thisNickname;
    }

    public override void OnRefreshCharacterScore(int oldScoreValue, int newScoreValue)
    {
    }
}
