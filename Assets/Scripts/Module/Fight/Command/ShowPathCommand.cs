using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPathCommand : BaseCommand
{
    Collider2D pre; //���֮ǰ��⵽��2d��ײ��
    Collider2D current; //��굱ǰ��⵽��2d��ײ��
    AStar astar; //A�Ƕ���
    AStarPoint start; //��ʼ��
    AStarPoint end; //�յ�
    List<AStarPoint> prePaths; //֮ǰ��⵽��·���ϼ� ���������

    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePaths = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);

    }

    public override bool Update(float dt)  
    {
        //������� ȷ���ƶ���λ��
        if (Input.GetMouseButtonDown(0))
        {
            if (prePaths.Count != 0 && this.model.Step >= prePaths.Count - 1)
            {
                GameApp.CommandManager.AddCommand(new MoveCommand(this.model, prePaths)); //�ƶ�
            }
            else
            {
                GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
                //���ƶ�ֱ����ʾ����ѡ��
                GameApp.ViewManager.Open(ViewType.SelectOptionView, this.model.data["Event"], (Vector2)this.model.transform.position);
                

            }
            //GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent); //ִ��δѡ��         
            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main,Input.mousePosition); //��⵱ǰ���λ���Ƿ���2d��ײ��

        if (current != null)
        {
            //֮ǰ����ײ���к͵�ǰ�ĺ��Ӳ�һ�� �Ž��� ·�����
            if (current != pre)
            {
                pre = current;

                Block b = current.GetComponent<Block>();
                if (b != null)
                {
                    //��⵽block�ű���Ϊ�� ����Ѱ·
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end,updatePath);

                }
                else
                {
                    //û��⵽ ��֮ǰ��·�� ���
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
        //���֮ǰ�Ѿ���·���� Ҫ�����
        if (prePaths.Count != 0)
        {
            for (int i = 0; i < prePaths.Count; i++)
            {
                GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
            }
        }
        
        if (paths.Count >= 2 && model.Step >= paths.Count - 1)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                BlockDirection dir = BlockDirection.down;

                if (i == 0)
                {
                    dir = GameApp.MapManager.GetDirection(paths[i],paths[i + 1]);

                }else if(i == paths.Count - 1)
                {
                    dir = GameApp.MapManager.GetDirection2(paths[i], paths[i - 1]);
                }
                else
                {
                    dir = GameApp.MapManager.GetDirection3(paths[i - 1], paths[i], paths[i + 1]);
                }

                GameApp.MapManager.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir, Color.yellow);
            }
        }
        prePaths = paths;

    }



}
