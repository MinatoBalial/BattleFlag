using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//处理拖拽英雄图标的脚本
public class HeroItem : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    Dictionary<string, string> data;
    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
            
    }


    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    public void OnDrag(PointerEventData eventData)
    { 
        
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Close((int)ViewType.DragHeroView);
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
