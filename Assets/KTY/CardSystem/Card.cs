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
    private Image image;//�̹���
    private CardType type;//Ÿ��
    private CardState state = CardState.None;
    public string name { get; set; }
    public AbillityWrapper Ability { get; private set; }//�߰��ɷ�

    public Card(Image image, CardType type, AbillityWrapper ability, string name)
    {
        this.image = image;
        this.type = type;
        this.Ability = ability;
        this.name = name;
    }

    public Image Image()
    {
        return image;
    }

    public CardType Type()
    {
        return type;
    }


    public CardState State()
    {
        return state = (state == CardState.None) ? CardState.Drow : CardState.None;
    }
}

public class CardBuild
{
    private Image image;//�̹���
    private CardType type;//Ÿ��
    private AbillityWrapper ability = new();//�߰��ɷ�
    private string name;
    public CardBuild Image(Image image)
    {
        this.image = image;
        return this;
    }



    public CardBuild Type(int number)
    {
        switch (number)
        {
            case <= 12:
                type = CardType.Attack;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerAttack, ability);
                break;
            case <= 25:
                type = CardType.Defense;
                ability.AbilityStates = number;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerDefense, ability);
                break;
            case <= 38:
                type = CardType.Recovery;
                ability.AbilityStates = number;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerRecovery, ability);
                break;
            case <= 51:
                type = CardType.Buff;
                ability.AbilityStates = number;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerBuff, ability);
                break;
        }
        return this;
    }

    public Card Build()
    {
        return new Card(image, type, ability, name);
    }
}
