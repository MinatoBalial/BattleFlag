using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗中的状态枚举
public enum GameState 
{
    Idle,
    Enter
}


/// <summary>
/// 战斗管理器（用于管理战斗相关的实体（敌人 英雄 地图 格子 等))
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current; //当前所处的战斗单元

    public FightUnitBase Current
    {
        get
        {
            return current;
        }
    }

    public FightWorldManager()
    {
        ChangeState(GameState.Idle);

    }


    public void Update(float dt)
    {
        if (current != null && current.Update(dt) == true)
        {
            //to do
        }
        else
        {
            current = null;
        }
    }

    public void ChangeState(GameState state)
    {
        FightUnitBase _current = current;
        this.state = state;
        switch (this.state)
        { 
       
            case GameState.Idle:
                _current = new FightIdle();
                break;
            case GameState.Enter:
                _current = new FightEnter();
                break;


        }
        _current.Init();
    }



}
