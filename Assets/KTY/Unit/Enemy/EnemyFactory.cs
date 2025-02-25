using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject EnemyModel;
    public Transform CenterPos;
    public static Unit CurrentGameObject;
    public void Awake()
    {
        Local.EventHandler.Register<States>(EnumType.EnemyDie, (enemyState) => { CreateEnum(); });
        CreateEnum();
    }
    public void CreateEnum()
    {
        Instantiate(EnemyModel, CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
        CurrentGameObject = unit;
        //if(IsInFirstSequence(Local.Stage))//�߰�����
        //{
        //}
        //else if (IsInSecondSequence(Local.Stage))//����
        //{

        //}
        //else//�Ϲ� ����
        //{

        //}
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





