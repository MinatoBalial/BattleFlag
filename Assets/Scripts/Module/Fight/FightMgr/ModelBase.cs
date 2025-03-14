  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id; //����id
    public Dictionary<string, string> data; //���ݱ�
    public int Step;   //�ж���
    public int Attack; //������
    public int Type;   //����
    public int MaxHp;  //���Ѫ��
    public int CurHp;  //��ǰѪ��

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp; //����ͼƬ��Ⱦ���
    public GameObject stopObj; //ֹͣ��ı�����
    public Animator ani;  //�������

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
