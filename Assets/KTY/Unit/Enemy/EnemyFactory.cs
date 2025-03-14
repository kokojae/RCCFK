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
        Local.EventHandler.Register<int>(EnumType.EnemyDie, (enemyState) => { CreateEnum(); });
        CreateEnum();
    }
    public void CreateEnum()
    {

        if (IsInFirstSequence(Local.Stage))//중간보스
        {
            Instantiate(BossModel, CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
            CurrentGameObject = unit;
            SetStates();
        }
        else if (IsInSecondSequence(Local.Stage))//보스
        {
            Instantiate(MiddleBossModel, CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
            CurrentGameObject = unit;
            SetStates();
        }
        else//일반 몬스터
        {

            Instantiate(NormalEnemyModel[Random.Range(0, NormalEnemyModel.Count)], CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
            CurrentGameObject = unit;
            SetStates();
        }
    }

    public bool IsInFirstSequence(int x)//5,15,25,35,45 이 수열인지 획인
    {
        return (x + 5) % 10 == 0 && (x + 5) / 10 >= 1;
    }

    public bool IsInSecondSequence(int x)//10,20,30,40,50 이 수열인지 확인
    {
        return x % 10 == 0 && x / 10 >= 1;
    }

    public void SetStates()
    {
        CurrentGameObject.UnitStates.Power = CurrentGameObject.UnitStates.Power * Local.Stage;
        CurrentGameObject.UnitStates.Defense = CurrentGameObject.UnitStates.Defense * Local.Stage;
        CurrentGameObject.UnitStates.MaxHp = CurrentGameObject.UnitStates.MaxHp * Local.Stage;
        CurrentGameObject.UnitStates.SetExp += CurrentGameObject.UnitStates.SetExp * Local.Stage;


    }
}





