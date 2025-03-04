using System;
using UnityEngine;

public class UnitStatesUiPresenter : MonoBehaviour
{
    public UnitStatesUiView UnitStatesUiView;
    public Action<States> PlayerFunc;
    public Action<States> EnemyFunc;

    public void Awake()
    {
        Subscribe();
    }

    public void Subscribe()
    {
        PlayerFunc = UnitStatesUiView.PlayerUiSet;
        EnemyFunc = UnitStatesUiView.EnemyUiSet;
        Local.EventHandler.Register<States>(EnumType.PlayerStatesUi, PlayerFunc);
        Local.EventHandler.Register<States>(EnumType.EnemyStatesUi, EnemyFunc);
    }
}
