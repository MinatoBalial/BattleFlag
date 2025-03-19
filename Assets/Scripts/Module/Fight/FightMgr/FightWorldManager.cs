using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս���е�״̬ö��
public enum GameState 
{
    Idle,
    Enter,
    Player,
    Enemy
}


/// <summary>
/// ս�������������ڹ���ս����ص�ʵ�壨���� Ӣ�� ��ͼ ���� ��))
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current; //��ǰ������ս����Ԫ

    public List<Hero> heros; //ս���е�Ӣ�ۼ���

    public List<Enemy> enemys; //ս���еĵ��˼���  

    public int RoundCount; //�غ���
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

    //����ս�� ��ʼ�� һЩ��Ϣ ������Ϣ �غ�����
    public void EnterFight()
    {
        RoundCount = 1;
        heros = new List<Hero>();
        enemys = new List<Enemy>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy"); //���������Enemy��ǩ
        Debug.Log("enemy:" + objs.Length);  
        for(int i=0; i < objs.Length; i++)
        {
            Enemy enemy = objs[i].GetComponent<Enemy>();
            //��ǰλ�ñ�ռ���� Ҫ�Ѷ�Ӧ������������Ϊ�ϰ���     
            GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
            enemys.Add(objs[i].GetComponent<Enemy>());
        }
    }

    //���Ӣ��
    public void AddHero(Block b,Dictionary<string,string> data)
    {
        GameObject obj = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        b.Type = BlockType.Obstacle;
        heros.Add(hero);
    }

    //�Ƴ�����
    public void RemoveEnemy(Enemy enemy)
    {
        enemys.Remove(enemy);
    }

    //����Ӣ���ж�
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
    /// �����Ŀ�������Ӣ��
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
