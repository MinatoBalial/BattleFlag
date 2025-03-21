using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人回合
/// </summary>
public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightManager.ResetHeros(); //重置英雄行动
        GameApp.ViewManager.Open(ViewType.TipView, "敌人回合");

        GameApp.CommandManager.AddCommand(new WaitCommand(2.25f));

        //敌人移动 使用技能等

        for (int i = 0; i < GameApp.FightManager.enemys.Count; i++)
        {
            Enemy enemy = GameApp.FightManager.enemys[i];
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //等待
            GameApp.CommandManager.AddCommand(new AiMoveCommand(enemy));
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //等待
            GameApp.CommandManager.AddCommand(new SkillCommand(enemy));//使用技能
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //等待

        }

        //等待一段时间 切换回玩家回合
        GameApp.CommandManager.AddCommand(new WaitCommand(0.2f, delegate ()
        {
            GameApp.FightManager.ChangeState(GameState.Player);
        }));
    }
}
