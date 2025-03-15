using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;



//地图管理器 存储地图网格的信息
public class MapManager 
{
    private Tilemap tileMap;

    public Block[,] mapArr;

    public int RowCount; //地图行
    public int ColCount; //地图列；

    public void Init()
    {
        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
        
        //地图大小 可以将这个信息写到配置表中进行设置
        RowCount = 12;
        ColCount = 20;
        
        mapArr =  new Block[RowCount, ColCount];
        List<Vector3Int> tempPosArr = new List<Vector3Int>(); //临时记录瓦片每个格子的位置
        //遍历瓦片地图
        foreach(var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tempPosArr.Add(pos);
            }
        }

        //将一维数组的位置转换成二维数组的Block 进行存储
        Object prefabObj = Resources.Load("Model/block");
        for (int i = 0; i< tempPosArr.Count; i++)
        {
            int row = i / ColCount;
            int col = i % ColCount;
            Block b = (Object.Instantiate(prefabObj) as GameObject).AddComponent<Block>();
            b.RowIndex = row;
            b.ColIndex = col;
            b.transform.position = tileMap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f, 0.5f, 0);
            mapArr[row, col] = b;
        }

    }

    public BlockType GetBlockType(int row, int col)
    {
        return mapArr[row, col].Type;

    }
    //显示移动区域
    public void ShowStepGrid(ModelBase model,int step)
    {
        _BFS bfs = new _BFS(RowCount,ColCount);
        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);
        for (int i=0;i <points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].ShowGrid(Color.blue);
        }
    }

    //隐藏移动的区域
    public void HideStepGrid(ModelBase model,int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);
        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);
        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].HideGrid();
        }
    }
}
