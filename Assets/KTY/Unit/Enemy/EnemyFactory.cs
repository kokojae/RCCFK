using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public List<GameObject> NormalEnemyModel;
    public GameObject MiddleBossModel;
    public GameObject BossModel;
    public Transform CenterPos;
    public static Unit CurrentGameObject;
    public void Awake()
    {
        Local.EventHandler.Register<States>(EnumType.EnemyDie, (enemyState) => { CreateEnum(); });
        CreateEnum();
    }
    public void CreateEnum()
    {

        if (IsInFirstSequence(Local.Stage))//�߰�����
        {
            Instantiate(BossModel, CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
            CurrentGameObject = unit;
        }
        else if (IsInSecondSequence(Local.Stage))//����
        {
            Instantiate(MiddleBossModel, CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
            CurrentGameObject = unit;
        }
        else//�Ϲ� ����
        {

            Instantiate(NormalEnemyModel[Random.Range(0, NormalEnemyModel.Count)], CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
            CurrentGameObject = unit;
        }
    }

    public bool IsInFirstSequence(int x)//5,15,25,35,45 �� �������� ȹ��
    {
        return (x + 5) % 10 == 0 && (x + 5) / 10 >= 1;
    }

    public bool IsInSecondSequence(int x)//10,20,30,40,50 �� �������� Ȯ��
    {
        return x % 10 == 0 && x / 10 >= 1;
    }
}





