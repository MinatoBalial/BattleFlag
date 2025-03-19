using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;



public class _BFS
{
    public class Point
    {
        public int RowIndex; // ������
        public int ColIndex; // ������
        public Point Father; // ���ڵ� ��������·����

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

    public int RowCount; //������
    public int ColCount; //������

    public Dictionary<string, Point> finds;//�洢���ҵ��ĵ���ֵ�(key:�������ƴ���ַ��� value;������)


    public _BFS(int row,int col)
    {
        finds = new Dictionary<string, Point>();
        this.RowCount = row;
        this.ColCount = col;

    }



    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="row">��ʼ���������</param>
    /// <param name="col">��ʼ���������</param>
    /// <param name="step">����</param>
    /// <returns></returns>
    
    public List<Point> Search(int row,int col,int step)
    {
        //������������
        List<Point> searchs = new List<Point>();
        //��ʼ��
        Point startPoint = new Point(row, col);
        //����ʼ��洢����������
        searchs.Add(startPoint);
        //��ʼ��Ĭ�Ͽ�ʼ�Ѿ��ҵ��� �洢�����ҵ����ֵ�
        finds.Add($"{row}_{col}", startPoint);

        //�������� �൱�ڿ������Ĵ���
        for(int i=0;i < step; i++)
        {
            //����һ����ʱ�ļ��� ���ڴ洢Ŀǰ�ҵ�����ĵ�
            List<Point> temps = new List<Point>();
            for (int j = 0; j < searchs.Count;j++)
            {
                Point current = searchs[j];
                //���ҵ�ǰ�����ܵĵ�
                FindAroundPoints(current, temps);
            }
            if (temps.Count == 0)
            {
                //��ʱ����һ���㶼�Ҳ��� �൱����·�ˣ�����ֹͣ
                break;
            }
            //�����ļ���Ҫ���
            searchs.Clear();
            //����ʱ���ϵĵ���ӵ���������
            searchs.AddRange(temps);
        }

        //�����ҵ��ĵ�װ���ɼ��� ����
        return finds.Values.ToList();

    }

    //����Χ�ĵ㣬��������(������չ����б����ĵ㣩
    public void FindAroundPoints(Point current,List<Point> temps)
    {
        //��
        if (current.RowIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex-1, current.ColIndex, current, temps);
        }
        //��
        if (current.RowIndex + 1 < RowCount)
        {
            AddFinds(current.RowIndex+1, current.ColIndex, current, temps);

        }
        //��
        if (current.ColIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex, current.ColIndex-1, current, temps);

        }
        //��
        if (current.ColIndex + 1 < ColCount)
        {
            AddFinds(current.RowIndex, current.ColIndex+1, current, temps);

        }
    }
    //��ӵ㵽���ҵ��ֵ�
    public void AddFinds(int row, int col, Point father, List<Point> temps)
    {
        //���ڲ��ҵĽڵ� �� ��Ӧ��ͼ���ӵ����Ʋ����ϰ��� ���ܼ��� �����ֵ�
        if(finds.ContainsKey($"{row}_{col}") == false && GameApp.MapManager.GetBlockType(row,col) != BlockType.Obstacle)
        {
            Point p = new Point(row, col, father);
            finds.Add($"{row}_{col}", p);
            //��ӵ���ʱ���� �����´β���
            temps.Add(p);
        }
    }
    //Ѱ�ҿ��ƶ��ĵ� ���յ�����ĵ��·������
    public List<Point> FindMinPath(ModelBase model,int step,int endRowIndex,int endColIndex)
    {
        List<Point> result = Search(model.RowIndex, model.ColIndex, step); //������ƶ��ĵ�ļ���
        if (result.Count == 0)
        {
            return null;
        }
        else
        {
            Point minPoint = result[0]; //Ĭ��һ����Ϊ��Ŀ������
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
