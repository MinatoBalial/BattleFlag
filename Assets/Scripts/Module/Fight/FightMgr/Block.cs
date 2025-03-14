using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    Null, //��
    Obstacle//�ϰ���
}

//��ͼ�ĵ�Ԫ����
public class Block : MonoBehaviour
{
    public int RowIndex; //���±�
    public int ColIndex; //���±�
    public BlockType Type; //��������
    private SpriteRenderer selectSp; //ѡ�еĸ���ͼƬ
    private SpriteRenderer gridSp; //����ͼƬ
    private SpriteRenderer dirSp; //�ƶ�����ͼƬ

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

        GameApp.MsgCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);

    }

    private void OnDestroy()
    {
        GameApp.MsgCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);

    }

    void OnSelectCallBack(System.Object arg)
    {
        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
    }

    void Start()
    {

    }

    private void OnMouseEnter()
    {
        selectSp.enabled = true;

    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }






}
