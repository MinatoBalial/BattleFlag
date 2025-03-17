using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//�򵥵�tip����
public class TipView : BaseView
{

    public override void Open(params object[] args)
    {
        base.Open(args);
        Debug.Log("������ʾ");
        Find<Text>("content/txt").text = args[0].ToString();
        Sequence seq = DOTween.Sequence();
        seq.Append(Find("content").transform.DOScaleY(1, 0.15f)).SetEase(Ease.OutBack);
        seq.AppendInterval(0.75f);
        seq.Append(Find("content").transform.DOScaleY(0, 0.15f)).SetEase(Ease.Linear);
        seq.AppendCallback(delegate ()
        {
            GameApp.ViewManager.Close(ViewId);
        });
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
