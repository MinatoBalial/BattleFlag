using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗中的状态枚举
public enum GameState 
{
    Idle,
    Enter,
    Player,
    Enemy
}


/// <summary>
/// 战斗管理器（用于管理战斗相关的实体（敌人 英雄 地图 格子 等))
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current; //当前所处的战斗单元

    public List<Hero> heros; //战斗中的英雄集合

    public List<Enemy> enemys; //战斗中的敌人集合  

    public int RoundCount; //回合数
    public FightUnitBase Current
    {
        get
        {
            return current;
        }
    }
    public FightWorldManager()
    {
        heros = new List<Hero>();
        enemys = new List<Enemy>();
        ChangeState(GameState.Idle);

    }


    public void Update(float dt)
    {
        if (current != null && current.Update(dt) == true)
        {
            //to do
        }
        else
        {
            current = null;
        }
    }

    public void ChangeState(GameState state)
    {
        FightUnitBase _current = current;
        this.state = state;
        switch (this.state)
        { 
       
            case GameState.Idle:
                _current = new FightIdle();
                break;
            case GameState.Enter:
                _current = new FightEnter();
                break;
            case GameState.Player:
                _current = new FightPlayerUnit();
                break;
            case GameState.Enemy:
                _current = new FightEnemyUnit();
                break;

        }
        _current.Init();
    }

    //进入战斗 初始化 一些信息 敌人信息 回合数等
    public void EnterFight()
    {
        RoundCount = 1;
        heros = new List<Hero>();
        enemys = new List<Enemy>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy"); //给怪物添加Enemy标签
        Debug.Log("enemy:" + objs.Length);  
        for(int i=0; i < objs.Length; i++)
        {
            Enemy enemy = objs[i].GetComponent<Enemy>();
            //当前位置被占用了 要把对应方块类型设置为障碍物     
            GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
            enemys.Add(objs[i].GetComponent<Enemy>());
        }
    }

    //添加英雄
    public void AddHero(Block b,Dictionary<string,string> data)
    {
        GameObject obj = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        b.Type = BlockType.Obstacle;
        heros.Add(hero);
    }

    //移除怪物
    public void RemoveEnemy(Enemy enemy)
    {
        enemys.Remove(enemy);
    }

    //重置英雄行动
    public void ResetHeros()
    {
        for (int i = 0; i < heros.Count; i++)
        {
            heros[i].IsStop = false;
        }
    }

    public void ResetEnemys()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].IsStop = false;
        }
        
    }
    /// <summary>
    /// 获得离目标最近的英雄
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ModelBase GetMinDisHero(ModelBase model)
    {
        if (heros.Count == 0)
        {
            return null;
        }
        Hero hero = heros[0];
        float min_dis = hero.GetDis(model);
        for (int i = 0; i < heros.Count; i++)
        {
            float dis = heros[i].GetDis(model);
            if (dis < min_dis)
            {
                min_dis = dis;
                hero = heros[i];
            }
        }
        return hero;
    }
}
