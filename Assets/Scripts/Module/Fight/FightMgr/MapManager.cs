using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;



//��ͼ������ �洢��ͼ�������Ϣ
public class MapManager 
{
    private Tilemap tileMap;

    public Block[,] mapArr;

    public int RowCount; //��ͼ��
    public int ColCount; //��ͼ�У�

    public void Init()
    {
        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
        
        //��ͼ��С ���Խ������Ϣд�����ñ��н�������
        RowCount = 12;
        ColCount = 20;
        
        mapArr =  new Block[RowCount, ColCount];
        List<Vector3Int> tempPosArr = new List<Vector3Int>(); //��ʱ��¼��Ƭÿ�����ӵ�λ��
        //������Ƭ��ͼ
        foreach(var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tempPosArr.Add(pos);
            }
        }

        //��һά�����λ��ת���ɶ�ά�����Block ���д洢
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


}
