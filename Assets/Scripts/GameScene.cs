using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�̳�mono�Ľű� ��Ҫ������Ϸ���� ��ת����֮��ǰ�ű����岻ɾ��
public class GameScene : MonoBehaviour
{
    public Texture2D mouseTxt; //�����ʽ
    float dt;
    // Start is called before the first frame update
    private static bool isLoaded = false;

    private void Awake()
    {
        if(isLoaded == true)
        {
            Destroy(gameObject);

        }
        else
        {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
            GameApp.Instance.Init();
        }

    }
    void Start()  
    {
        //�������
        Cursor.SetCursor(mouseTxt,Vector2.zero,CursorMode.Auto);

        //���ű�������
        GameApp.SoundManager.PlayBGM("login");

        RegisterConfigs();
        GameApp.ConfigManager.LoadAllConfigs(); //��ʼ�������ñ�

        ConfigData tempData = GameApp.ConfigManager.GetConfigDdata("enemy");
        string name = tempData.GetDataById(10001)["Name"];
        Debug.Log(name);

        RegisterModule(); //ע����Ϸ�еĿ�����

        InitModule();


    }

    //ע�������
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game,new GameController());
        GameApp.ControllerManager.Register(ControllerType.LoadingScene, new LoadingController());
        GameApp.ControllerManager.Register(ControllerType.Level,new LevelController());
        GameApp.ControllerManager.Register(ControllerType.Fight, new FightController());

    }

    //ִ�����п�������ʼ��
    void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }
    
    //ע�����ñ�
    void RegisterConfigs()
    {
        GameApp.ConfigManager.Register("enemy", new ConfigData("enemy"));
        GameApp.ConfigManager.Register("level", new ConfigData("level"));
        GameApp.ConfigManager.Register("option", new ConfigData("option"));
        GameApp.ConfigManager.Register("player", new ConfigData("player"));
        GameApp.ConfigManager.Register("role", new ConfigData("role"));
        GameApp.ConfigManager.Register("skill", new ConfigData("skill"));

    }
    // Update is called once per frame
    void Update()
    {

        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}
