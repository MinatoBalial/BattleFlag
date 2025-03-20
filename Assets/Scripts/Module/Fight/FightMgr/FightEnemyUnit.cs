using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˻غ�
/// </summary>
public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightManager.ResetHeros(); //����Ӣ���ж�
        GameApp.ViewManager.Open(ViewType.TipView, "���˻غ�");

        GameApp.CommandManager.AddCommand(new WaitCommand(2.25f));

        //�����ƶ� ʹ�ü��ܵ�

        for (int i = 0; i < GameApp.FightManager.enemys.Count; i++)
        {
            Enemy enemy = GameApp.FightManager.enemys[i];
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //�ȴ�
            GameApp.CommandManager.AddCommand(new AiMoveCommand(enemy));
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //�ȴ�
            GameApp.CommandManager.AddCommand(new SkillCommand(enemy));//ʹ�ü���
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f)); //�ȴ�

        }

        //�ȴ�һ��ʱ�� �л�����һغ�
        GameApp.CommandManager.AddCommand(new WaitCommand(0.2f, delegate ()
        {
            GameApp.FightManager.ChangeState(GameState.Player);
        }));
    }
}
