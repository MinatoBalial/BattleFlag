using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 战斗结束
/// </summary>
public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.CommandManager.Clear(); //清除指令

        //作者说这里本来胜利界面和失败界面想做在一起的
        if (GameApp.FightManager.heros.Count == 0)
        {
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, delegate ()
            {
                GameApp.ViewManager.Open(ViewType.LossView);
            }));
        }
        else if(GameApp.FightManager.enemys.Count == 0)
        {
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, delegate ()
            {
                GameApp.ViewManager.Open(ViewType.WinView);
            }));
        }
        else
        {

        }

    } 

    public override bool Update(float dt)
    {

        return true;
    }






}
