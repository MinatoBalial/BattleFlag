using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �򵥵�һ��ȫ���¼���ʱ��������
/// </summary>
public class TimerManager
{
    GameTimer timer;

    public TimerManager()
    {
        timer = new GameTimer();
    }
    public void Regiter(float time,System.Action callback)
    {
        timer.Register(time,callback);
    }

    public void OnUpdate(float dt)
    {
        timer.OnUpdate(dt);

    }
}
