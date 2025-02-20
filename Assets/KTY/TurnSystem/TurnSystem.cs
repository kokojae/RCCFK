using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TurnSystem
{
    private List<ITurnObj> turns = new(); //���� ������ �ִ� ����Ʈ
    public bool turnproress { get; set; } = true;

    public void Register(ITurnObj turn) //�� �Ҵ�
    {
        turns.Add(turn);
    }

    public void UnRegister() //�� ����
    {
        turns.Remove(turns.LastOrDefault<ITurnObj>());
    }

    public bool TurnStart(bool turnstart)//
    {
        return turnproress = turnstart;
    }

    public async UniTask Invoke() //�� ����
    {
        while (true)
        {
            for (int i = 0; i < turns.Count; i++)
            {
                await UniTask.WaitUntil(() => turnproress);
                TurnStart(false);
                turns[i].Invoke();
            }
            //turns.Clear();
            //TurnStart(false);
        }
    }

}
