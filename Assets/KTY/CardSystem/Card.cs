using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { Attack, Defense, Recovery, Buff, Special }
public enum CardState { None, Drow }
[System.Serializable]
public class Card
{
    //Ư��ī�尰����� Ư���ɷ� ���� Ŭ������ �����  ������ enum���� ����� Ư��ī�� �ɷ� ����� CardŬ������ ���¸� Ư���ɷ� Ŭ���� �Լ��� �Ѱ��־� Ÿ�Կ� ���� �ɷ½���ǰ� 
    [SerializeField] private Sprite sprite;//�̹���
    [SerializeField] private CardType type;//Ÿ��
    public string name { get; set; }
    public AbillityWrapper Ability;//�߰��ɷ�

    public Card(Sprite image, CardType type, AbillityWrapper ability, string name)
    {
        this.sprite = image;
        this.type = type;
        this.Ability = ability;
        this.name = name;
    }

    public Sprite Sprite()
    {
        return sprite;
    }

    public CardType Type()
    {
        return type;
    }

    public void SelectAction()
    {

        switch (type)
        {
            case <= CardType.Attack:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerAttack, Ability);
                break;
            case <= CardType.Defense:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerDefense, Ability);
                break;
            case <= CardType.Recovery:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerRecovery, Ability);
                break;
            case <= CardType.Buff:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerBuff, Ability);
                break;
        }
    }

}

[System.Serializable]
public class CardBuild
{
    [SerializeField] private Sprite sprite;//�̹���
    [SerializeField] private CardType type;//Ÿ��
    [SerializeField] private AbillityWrapper ability = new();//�߰��ɷ�
    [SerializeField] private string name;
    public CardBuild Image(Sprite sprite)
    {
        this.sprite = sprite;
        return this;
    }



    public CardBuild Type(int number)
    {
        switch (number)
        {
            case <= 12:
                type = CardType.Attack;
                break;
            case <= 25:
                type = CardType.Defense;
                ability.AbilityStates = number;
                break;
            case <= 38:
                type = CardType.Recovery;
                ability.AbilityStates = number;
                break;
            case <= 51:
                type = CardType.Buff;
                ability.AbilityStates = number;
                break;
        }
        return this;
    }

    public Card Build()
    {
        return new Card(sprite, type, ability, name);
    }
}
