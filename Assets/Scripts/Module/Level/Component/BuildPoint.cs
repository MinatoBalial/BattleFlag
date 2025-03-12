using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelId; //…Ë÷√πÿø®id
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameApp.MsgCenter.PostEvent(Defines.ShowLevelDesEvent,LevelId);
        //Debug.Log("enter" + collision.gameObject.name);    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameApp.MsgCenter.PostEvent(Defines.HideLevelDesEvent, LevelId);

    }

}
