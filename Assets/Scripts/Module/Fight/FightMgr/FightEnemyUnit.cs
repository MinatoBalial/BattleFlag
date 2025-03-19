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


        //�ȴ�һ��ʱ�� �л�����һغ�
        GameApp.CommandManager.AddCommand(new WaitCommand(0.2f, delegate ()
        {
            GameApp.FightManager.ChangeState(GameState.Player);
        }));
    }
}
