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
        //Debug.Log("特殊1");
        //玩家回合，才能选中角色
        if(GameApp.FightManager.state == GameState.Player)
        {

            //Debug.Log("特殊2");

            if (GameApp.CommandManager.IsRunningCommand == true)
            {
                return;
            }

            GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);

            if (IsStop == false) { 
                //显示路径
                GameApp.MapManager.ShowStepGrid(this, Step);
                //Debug.Log("特殊3");
                //添加显示路径指令
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));
                //添加选项事件
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

    //攻击
    private void onAttackCallBack(System.Object arg)
    {
        Debug.Log("attack");
    }
    //待机
    private void onIdelCallBack(System.Object arg)
    {
        IsStop = true;
    }


    //取消 移动
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
