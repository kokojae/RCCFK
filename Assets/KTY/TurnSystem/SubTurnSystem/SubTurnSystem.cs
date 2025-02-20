using Cysharp.Threading.Tasks;
using System;
using System.ComponentModel;
using UnityEngine;

public class SubTurnSystem : MonoBehaviour, ITurnObj, IEvent
{
    protected Action TurnAction;//�� ���� ������ �׼�
    private bool turnEnd = false;
    public virtual void Start()
    {
    }

    public bool Invoke()//�׼� ����
    {
        Inside().Forget();
        return turnEnd;
    }

    public async UniTask Inside()
    {
        while (true)
        {
            TurnAction?.Invoke();
            TurnAction = null;
            await UniTask.WaitUntil(() => TurnAction != null || Local.TurnSystem.turnproress);
            if (Local.TurnSystem.turnproress)
                break;
            await UniTask.WaitUntil(() => TurnAction != null);
        }
        Debug.Log($"������{gameObject.name}");
        Local.EventHandler.Invoke<ResetCost>(EnumType.ResetCost, ResetCost.ResetCost);
    }
    public void Register(Action ActionType)//�׼� �Ҵ�
    {
        TurnAction = ActionType.Invoke;
    }

    public void UnRegister(Action ActionType)//�׼� ����
    {
        TurnAction -= ActionType.Invoke;
    }

    public void TurnEnd()
    {
        turnEnd = true;
    }
    public void Execute()
    {
        throw new NotImplementedException();
    }
}
