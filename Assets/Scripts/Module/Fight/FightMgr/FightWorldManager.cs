using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս���е�״̬ö��
public enum GameState 
{
    Idle,
    Enter
}


/// <summary>
/// ս�������������ڹ���ս����ص�ʵ�壨���� Ӣ�� ��ͼ ���� ��))
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current; //��ǰ������ս����Ԫ

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
