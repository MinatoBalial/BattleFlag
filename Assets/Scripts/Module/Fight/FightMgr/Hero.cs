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
        GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this));

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

    //��ʾ��������
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
        //����������Ч
        GameApp.SoundManager.PlayEffect("hit", transform.position);
        //��Ѫ
        CurHp -= skill.skillPro.Attack;
        //��ʾ�˺�����
        GameApp.ViewManager.ShowHitNum($"-{skill.skillPro.Attack}", Color.red, transform.position);
        //������Ч
        //Debug.Log(skill.skillPro.AttackEffect);
        PlayEffect(skill.skillPro.AttackEffect);

        if (CurHp <= 0)
        {
            CurHp = 0;
            PlayAni("die");

            Destroy(gameObject, 1.2f);

            //�ӵ��˼������Ƴ�
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
