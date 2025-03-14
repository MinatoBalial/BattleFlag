  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id; //物体id
    public Dictionary<string, string> data; //数据表
    public int Step;   //行动力
    public int Attack; //攻击力
    public int Type;   //类型
    public int MaxHp;  //最大血量
    public int CurHp;  //当前血量

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp; //身体图片渲染组件
    public GameObject stopObj; //停止活动的标记组件
    public Animator ani;  //动画组件

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();

    }
    private void Start()
    {
        
    }

}
