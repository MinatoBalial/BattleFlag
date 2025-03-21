using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Hero : ModelBase,ISkill
{
    public SkillProperty skillPro { get; set; }
    private Slider hpSlider;

    public void Init(Dictionary<string,string>data, int row, int col)
    {
        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();

        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        this.Type = int.Parse(this.data["Type"]);
        this.Id = int.Parse(this.data["Id"]);
        this.Attack = int.Parse(this.data["Attack"]);
        this.Step = int.Parse(this.data["Step"]);
        this.MaxHp = int.Parse(this.data["Hp"]);
        this.CurHp = MaxHp;
        skillPro = new SkillProperty(int.Parse(this.data["Skill"]));
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
        GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this));

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

    //显示技能区域
    public void ShowSkillArea()
    {
        GameApp.MapManager.ShowAttackStep(this, skillPro.AttackRange, Color.red);
    }

    public void HideSkillArea()
    {
        GameApp.MapManager.HideAttackStep(this, skillPro.AttackRange);
    }

    public override void GetHit(ISkill skill)
    {
        //播放受伤音效
        GameApp.SoundManager.PlayEffect("hit", transform.position);
        //扣血
        CurHp -= skill.skillPro.Attack;
        //显示伤害数字
        GameApp.ViewManager.ShowHitNum($"-{skill.skillPro.Attack}", Color.red, transform.position);
        //击中特效
        //Debug.Log(skill.skillPro.AttackEffect);
        PlayEffect(skill.skillPro.AttackEffect);

        if (CurHp <= 0)
        {
            CurHp = 0;
            PlayAni("die");

            Destroy(gameObject, 1.2f);

            //从敌人集合中移除
            GameApp.FightManager.RemoveHero(this);
        }
        StopAllCoroutines();
        StartCoroutine(ChangeColor());
        StartCoroutine(UpdateHpSlider());
    }
    private IEnumerator ChangeColor()
    {
        bodySp.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.25f);
        bodySp.material.SetFloat("_FlashAmount", 0);
    }

    private IEnumerator UpdateHpSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.DOValue((float)CurHp / (float)MaxHp, 0.25f);
        yield return new WaitForSeconds(0.75f);
        hpSlider.gameObject.SetActive(false);
    }

}
