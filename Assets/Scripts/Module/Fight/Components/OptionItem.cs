using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//选项
public class OptionItem : MonoBehaviour
{
    OptionData op_data;

    public void Init(OptionData data)
    {
        op_data = data;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate ()
        {
            //if (CompareStrings(op_data.EventName, Defines.OnAttackEvent))
            //{
            //    Debug.Log("字符串匹配！");
            //}
            //else
            //{
            //    Debug.Log("字符串不匹配！");
            //}
            GameApp.MsgCenter.PostTempEvent(op_data.EventName);//执行配置表中设置的Event事件
            GameApp.ViewManager.Close((int)ViewType.SelectOptionView);//关闭选项界面
        });
        transform.Find("txt").GetComponent<Text>().text = op_data.Name;


    }

    public bool CompareStrings(string str1, string str2)
    {
        // 如果两个字符串的长度不同，直接返回 false
        if (str1.Length != str2.Length)
        {
            Debug.Log(str1.Length +" "+ str2.Length);
            for (int i = 0; i < str1.Length; i++)
            {
                    Debug.Log(str1[i]);
                    //return false; // 如果有任何一个字符不匹配，返回 false
               
            }
            for (int i = 0; i < str2.Length; i++)
            {
                Debug.Log(str2[i]);
                //return false; // 如果有任何一个字符不匹配，返回 false

            }
            return false;
        }

        // 逐个字符比较
        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                Debug.Log(str1[i] + str2[i]);
                return false; // 如果有任何一个字符不匹配，返回 false
            }
        }

        return true; // 所有字符都匹配，返回 true
    }
}
