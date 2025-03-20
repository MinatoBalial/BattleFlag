using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ս������
/// </summary>
public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.CommandManager.Clear(); //���ָ��

        //����˵���ﱾ��ʤ�������ʧ�ܽ���������һ���
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
