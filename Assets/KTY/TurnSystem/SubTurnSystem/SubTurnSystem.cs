using Cysharp.Threading.Tasks;
using System;
using System.ComponentModel;
using UnityEditor.PackageManager;
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
        while (!Local.TurnSystem.turnproress)
        {
            TurnAction?.Invoke();
            TurnAction = null;

            await UniTask.WaitUntil(() => TurnAction != null);
        }
        Debug.Log("��");
    }
    public void Register(Action ActionType)//�׼� �Ҵ�
    {
        TurnAction += ActionType.Invoke;
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
