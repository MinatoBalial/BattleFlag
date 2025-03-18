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
        Debug.Log("����1");
        //��һغϣ�����ѡ�н�ɫ
        if(GameApp.FightManager.state == GameState.Player)
        {

            Debug.Log("����2");

            // ���ܲ���
            if (IsStop == true)
            {
                return;
            }


            if (GameApp.CommandManager.IsRunningCommand == true)
            {
                return;
            }
            Debug.Log("����3");
            //�����ʾ·��ָ��
            GameApp.CommandManager.AddCommand(new ShowPathCommand(this));

            base.OnSelectCallBack(arg);
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }

    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close((int)ViewType.HeroDesView);

    }
}
