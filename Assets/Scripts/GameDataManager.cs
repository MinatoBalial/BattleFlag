using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//��Ϸ���ݹ��������洢��һ�������Ϸ��Ϣ)
public class GameDataManager : MonoBehaviour
{
    public List<int> heros;//Ӣ�ۼ���

    public int Money; //���?

    public GameDataManager()
    {
        heros = new List<int>();

        //Ĭ������Ӣ�۵�id Ԥ�ȴ�����
        heros.Add(10001);
        heros.Add(10002);
        heros.Add(10003);

    }


}
