using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//ѡ��
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
            //    Debug.Log("�ַ���ƥ�䣡");
            //}
            //else
            //{
            //    Debug.Log("�ַ�����ƥ�䣡");
            //}
            GameApp.MsgCenter.PostTempEvent(op_data.EventName);//ִ�����ñ������õ�Event�¼�
            GameApp.ViewManager.Close((int)ViewType.SelectOptionView);//�ر�ѡ�����
        });
        transform.Find("txt").GetComponent<Text>().text = op_data.Name;


    }

    public bool CompareStrings(string str1, string str2)
    {
        // ��������ַ����ĳ��Ȳ�ͬ��ֱ�ӷ��� false
        if (str1.Length != str2.Length)
        {
            Debug.Log(str1.Length +" "+ str2.Length);
            for (int i = 0; i < str1.Length; i++)
            {
                    Debug.Log(str1[i]);
                    //return false; // ������κ�һ���ַ���ƥ�䣬���� false
               
            }
            for (int i = 0; i < str2.Length; i++)
            {
                Debug.Log(str2[i]);
                //return false; // ������κ�һ���ַ���ƥ�䣬���� false

            }
            return false;
        }

        // ����ַ��Ƚ�
        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                Debug.Log(str1[i] + str2[i]);
                return false; // ������κ�һ���ַ���ƥ�䣬���� false
            }
        }

        return true; // �����ַ���ƥ�䣬���� true
    }
}
