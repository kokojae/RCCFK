using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //�� Ŭ������ MVP������ �Ⱦ����� ui������ �Է����� �� �ϱ� ������ �� ������ Presenter�� Model�� ��������ٺκ��� ���� CardUi�� Mvp������ ������� �ʾҴ�.
    public CardsDataBase CardsData;
    public InGameData InGameData;
    public GameObject cards;
    public GameObject cardPrefab;
    public float Spaceing;
    private Vector3[] CardsPos = new Vector3[8];
    private int cardIndex;

    public void Start()
    {
        InGameData.SettingDack();
        CardsPosSet();
        CreateCards(CardsData.CardDeck.Count);
        CardDrow(CardsPos.Length);
        Local.EventHandler.Register<UnitDead>(EnumType.EnemyDie, (unitDead) => {  InGameData.AllDeckReMove(); CreateCards(CardsData.CardDeck.Count);  CardDrow(CardsPos.Length); });
    }

    public void CardDrow(int count)//������ƮǮ������ ���ľ��� //ī�嵥���� ������ �����ִ� ī�带 �̴� �Լ�
    {
        Card card;
        for (int i = 0; i < count; i++)
        {
            card = InGameData.battleDeck.Dequeue();
            GameObject cardObject = Instantiate(cardPrefab, cards.transform);
            cardObject.transform.GetChild(0).GetComponent<Image>().sprite = card.Sprite(); //ĸ��ȭ �ؾ���***
            InGameData.DeckAdd(cardObject);
            InGameData.DrowAdd(card);
        }
        CardsSort();
    }

    public void CardsSort()
    {
        for (int i = 0; i < InGameData.deckUi.Count; i++)
        {
            InGameData.deckUi[i].transform.position = CardsPos[i];
        }
    }

    public void CardsPosSet()
    {
        for (int i = 0; i < CardsPos.Length; i++)
        {
            CardsPos[i] = new(cards.transform.position.x + (Spaceing * i), cards.transform.position.y);

        }
    }

    public async UniTask CardAni(GameObject card)
    {
        Vector3 targetPos = new(Screen.width / 2, (Screen.height / 2), 0);
        await SetAni(card.gameObject, targetPos, 1);
        targetPos = new Vector3(Screen.width, 0, 0);
        await UniTask.WaitForSeconds(0.5f);
        await SetAni(card.gameObject, targetPos, 0.1f);
    }

    private async UniTask SetAni(GameObject card, Vector3 targetPos, float minScale)
    {
        float time = 0;
        Vector3 pos = card.transform.position;
        while (time < 1)
        {
            card.transform.position = Vector3.Lerp(pos, targetPos, time);
            card.transform.localScale = new(Mathf.Clamp(card.transform.localScale.x - 0.01f, minScale, 1), Mathf.Clamp(card.transform.localScale.y - 0.01f, minScale, 1), 0);

            time += Time.deltaTime;
            await UniTask.Yield();
        }

    }

    public void CreateCards(int count)//��Ʋ�� �� ī��� ����
    {
        InGameData.battleDeck.Clear();
        InGameData.drowCards.Clear();
        for (int i = 0; i < count; i++)
        {
            CardsData.CardDeck[i].SelectAction();
            InGameData.battleDeck.Enqueue(CardsData.CardDeck[i]);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Card card;
        if (clickedObject.transform.GetChild(0).TryGetComponent(out Image image))
        {
            card = InGameData.FindCard(image.sprite);
            Debug.Log(card.Sprite());
            CardAni(clickedObject).Forget();
            Local.EventHandler.Invoke<Action>(EnumType.PlayerTurnAdd, card.Ability.AbillityFunc);
            InGameData.DeckReMove(clickedObject);
            CardDrow(1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one * 1.1f;//�ϵ��ڵ�***
    }
}
