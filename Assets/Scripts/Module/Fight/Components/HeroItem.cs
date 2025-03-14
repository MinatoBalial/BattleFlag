using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//������קӢ��ͼ��Ľű�
public class HeroItem : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    Dictionary<string, string> data;
    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
            
    }


    //��ʼ��ק
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    public void OnDrag(PointerEventData eventData)
    { 
        
    }

    //������ק
    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Close((int)ViewType.DragHeroView);
        //�����ק���λ���Ƿ���block�ű�
        Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, delegate (Collider2D col) { 
            if(col != null)
            {
                Block b = col.GetComponent<Block>();
                if(b!= null)
                {
                    //�з��� 
                    Debug.Log(b);
                    Destroy(gameObject);
                    GameApp.FightManager.AddHero(b, data);
                }
            }
        
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
