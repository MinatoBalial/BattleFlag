using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;



public class _BFS
{
    public class Point
    {
        public int RowIndex; // 行坐标
        public int ColIndex; // 列坐标
        public Point Father; // 父节点 用来查找路径用

        public Point(int row, int col)
        {
            this.RowIndex = row;
            this.ColIndex = col;
        }

        public Point(int row, int col, Point Father)
        {
            this.RowIndex = row;
            this.ColIndex = col;
            this.Father = Father;
        }
    }

    public int RowCount; //行总数
    public int ColCount; //列总数

    public Dictionary<string, Point> finds;//存储查找到的点的字典(key:点的行列拼的字符串 value;搜索点)


    public _BFS(int row,int col)
    {
        finds = new Dictionary<string, Point>();
        this.RowCount = row;
        this.ColCount = col;

    }



    /// <summary>
    /// 搜索行走区域
    /// </summary>
    /// <param name="row">开始点的行坐标</param>
    /// <param name="col">开始点的列坐标</param>
    /// <param name="step">步数</param>
    /// <returns></returns>
    
    public List<Point> Search(int row,int col,int step)
    {
        //定义搜索集合
        List<Point> searchs = new List<Point>();
        //开始点
        Point startPoint = new Point(row, col);
        //将开始点存储到搜索集合
        searchs.Add(startPoint);
        //开始点默认开始已经找到了 存储到已找到的字典
        finds.Add($"{row}_{col}", startPoint);

        //遍历步数 相当于可搜索的次数
        for(int i=0;i < step; i++)
        {
            //定义一个临时的几何 用于存储目前找到满足的点
            List<Point> temps = new List<Point>();
            for (int j = 0; j < searchs.Count;j++)
            {
                Point current = searchs[j];
                //查找当前点四周的点
                FindAroundPoints(current, temps);
            }
            if (temps.Count == 0)
            {
                //临时集合一个点都找不到 相当于死路了，进行停止
                break;
            }
            //搜索的几何要清空
            searchs.Clear();
            //将临时集合的点添加到搜索集合
            searchs.AddRange(temps);
        }

        //将查找到的点装换成集合 返回
        return finds.Values.ToList();

    }

    //找周围的点，上下左右(可以扩展查找斜方向的点）
    public void FindAroundPoints(Point current,List<Point> temps)
    {
        //上
        if (current.RowIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex-1, current.ColIndex, current, temps);
        }
        //下
        if (current.RowIndex + 1 < RowCount)
        {
            AddFinds(current.RowIndex+1, current.ColIndex, current, temps);

        }
        //左
        if (current.ColIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex, current.ColIndex-1, current, temps);

        }
        //右
        if (current.ColIndex + 1 < ColCount)
        {
            AddFinds(current.RowIndex, current.ColIndex+1, current, temps);

        }
    }
    //添加点到查找的字典
    public void AddFinds(int row, int col, Point father, List<Point> temps)
    {
        //不在查找的节点 且 对应地图格子的类似不是障碍物 才能加入 查找字典
        if(finds.ContainsKey($"{row}_{col}") == false && GameApp.MapManager.GetBlockType(row,col) != BlockType.Obstacle)
        {
            Point p = new Point(row, col, father);
            finds.Add($"{row}_{col}", p);
            //添加到临时集合 用于下次查找
            temps.Add(p);
        }
    }
    //寻找可移动的点 离终点最近的点的路径集合
    public List<Point> FindMinPath(ModelBase model,int step,int endRowIndex,int endColIndex)
    {
        List<Point> result = Search(model.RowIndex, model.ColIndex, step); //获得能移动的点的集合
        if (result.Count == 0)
        {
            return null;
        }
        else
        {
            Point minPoint = result[0]; //默认一个点为离目标点最近
            int min_dis = Mathf.Abs(minPoint.RowIndex - endRowIndex) + Mathf.Abs(minPoint.ColIndex - endColIndex);
            for (int i = 1; i < result.Count; i++)
            {
                int temp_dis = Mathf.Abs(result[i].RowIndex - endRowIndex) + Mathf.Abs(result[i].ColIndex - endColIndex);
                if (temp_dis > min_dis)
                {
                    min_dis = temp_dis;
                    minPoint = result[i];

                }
            }
            List<Point> paths = new List<Point>();
            Point current = minPoint.Father;
            paths.Add(minPoint);
            while (current != null)
            {    
                paths.Add(current);
                current = current.Father;
            }
        }

    }
}
