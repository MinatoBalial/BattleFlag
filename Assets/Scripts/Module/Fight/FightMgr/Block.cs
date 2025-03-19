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
        GameApp.MsgCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    private void OnDestroy()
    {
        GameApp.MsgCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);

    }

    //��ʾ����
    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }


    //���ظ���
    public void HideGrid()
    {
        gridSp.enabled = false;
    }

    void OnSelectCallBack(System.Object arg)
    {
        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
    }

    void OnUnSelectCallBack(System.Object arg)
    {
        dirSp.sprite = null;
    }

    private void OnMouseEnter()
    {
        selectSp.enabled = true;

    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    void Start()
    {

    }

    //���ü�ͷ�����ͼƬ��Դ �� ��ɫ
    public void SetDirSp(Sprite sp,Color col)
    {
        dirSp.sprite = sp;
        dirSp.color = col;
    }



}
