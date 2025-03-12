using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 选关界面人物简单的控制脚本
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();

        GameApp.CameraManager.SetPos(transform.position);//设置摄像机位置
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0)
        {
            ani.Play("idle");
        }
        else
        {
            if ((h >0 && transform.localScale.x<0) || (h < 0 && transform.localScale.x > 0))
            {
                Flip();  
            }
           
            Vector3 pos = transform.position + Vector3.right * h * moveSpeed *Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -32, 24); //限制在这个区间内行动
            transform.position = pos;


            GameApp.CameraManager.SetPos(transform.position);
            ani.Play("move");
        }
    }

    //转向
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;


    }
}
