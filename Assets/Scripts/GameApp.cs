using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͳһ������Ϸ�еĹ��������ڴ�����г�ʼ��
/// </summary>
public class GameApp :Singleton<GameApp>
{
    public static SoundManager SoundManager; //��Ƶ����������

    public static ControllerManager ControllerManager; //������������

    public static ViewManager ViewManager;

    public static ConfigManager ConfigManager;

    public static CameraManager CameraManager;//�����

    public static MessageCenter MsgCenter; //��Ϣ����

    public static TimerManager TimerManager; //ʱ�����

    public static FightWorldManager FightManager;

    public static MapManager MapManager;

    public static GameDataManager GameDataManager;

    public static UserInputManager UserInputManager;

    public static CommandManager CommandManager;

    public static SkillManager SkillManager;
    public override void Init()
    {
        TimerManager = new TimerManager();
        MsgCenter = new MessageCenter();
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        FightManager = new FightWorldManager();
        CameraManager = new CameraManager();
        MapManager = new MapManager();
        GameDataManager = new GameDataManager();
        UserInputManager = new UserInputManager();
        CommandManager = new CommandManager();
        SkillManager = new SkillManager();
    }

    public override void Update(float dt)
    {
        UserInputManager.Update();
        TimerManager.OnUpdate(dt);
        FightManager.Update(dt);
        CommandManager.Update(dt);
        SkillManager.Update(dt);
    }
}
