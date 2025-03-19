using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ModelBase
{
    public void Init(Dictionary<string,string>data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        this.Type = int.Parse(this.data["Type"]);
        this.Id = int.Parse(this.data["Id"]);
        this.Attack = int.Parse(this.data["Attack"]);
        this.Step = int.Parse(this.data["Step"]);
        this.MaxHp = int.Parse(this.data["Hp"]);
        this.CurHp = MaxHp;
    }

    protected override void OnSelectCallBack(object arg)
    {
        //Debug.Log("����1");
        //��һغϣ�����ѡ�н�ɫ
        if(GameApp.FightManager.state == GameState.Player)
        {

            //Debug.Log("����2");

            if (GameApp.CommandManager.IsRunningCommand == true)
            {
                return;
            }

            GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);

            if (IsStop == false) { 
                //��ʾ·��
                GameApp.MapManager.ShowStepGrid(this, Step);
                //Debug.Log("����3");
                //�����ʾ·��ָ��
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));
                //���ѡ���¼�
                addOptionEvents();

            }
            //base.OnSelectCallBack(arg);
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }

    }

    private void addOptionEvents()
    {
        GameApp.MsgCenter.AddTempEvent(Defines.OnAttackEvent,onAttackCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnIdleEvent,onIdelCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnCancelEvent, onCanCelCallBack);
        
    }

    //����
    private void onAttackCallBack(System.Object arg)
    {
        Debug.Log("attack");
    }
    //����
    private void onIdelCallBack(System.Object arg)
    {
        IsStop = true;
    }


    //ȡ�� �ƶ�
    private void onCanCelCallBack(System.Object arg)
    {
        GameApp.CommandManager.UnDo();
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close((int)ViewType.HeroDesView);

    }
}
