using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ModelBase
{
    public void Init(Dictionary<string,string>data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        this.Type = int.Parse(this.data["Type"]);
        this.Id = int.Parse(this.data["Id"]);
        this.Attack = int.Parse(this.data["Attack"]);
        this.Step = int.Parse(this.data["Step"]);
        this.MaxHp = int.Parse(this.data["Hp"]);
        this.CurHp = MaxHp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
