using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPathCommand : BaseCommand
{
    Collider2D pre; //鼠标之前检测到的2d碰撞盒
    Collider2D current; //鼠标当前检测到的2d碰撞盒
    AStar astar; //A星对象
    AStarPoint start; //开始点
    AStarPoint end; //终点
    List<AStarPoint> prePaths; //之前检测到的路径合集 用来清空用

    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePaths = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);

    }

    public override bool Update(float dt)  
    {
        //点击鼠标后 确定移动的位置
        if (Input.GetMouseButtonDown(0))
        {
            GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent); //执行未选中         
            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main,Input.mousePosition); //检测当前鼠标位置是否有2d碰撞盒

        if (current != null)
        {
            //之前的碰撞检测盒和当前的盒子不一致 才进行 路径检测
            if (current != pre)
            {
                pre = current;

                Block b = current.GetComponent<Block>();
                if (b != null)
                {
                    //检测到block脚本的为题 进行寻路
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end,updatePath);

                }
                else
                {
                    //没检测到 将之前的路径 清除
                    for (int i = 0; i < prePaths.Count; i++)
                    {
                        GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
                    }
                    prePaths.Clear();

                }
            }
        }
        return false;
    }

    private void updatePath(List<AStarPoint> paths)
    {
        //如果之前已经有路径了 要先清除
        if (prePaths.Count != 0)
        {
            for (int i = 0; i < prePaths.Count; i++)
            {
                GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
            }
        }


        for (int i = 0; i < paths.Count; i++)
        {
            BlockDirection dir = BlockDirection.down;
            GameApp.MapManager.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir, Color.yellow);
        }




    }



}
