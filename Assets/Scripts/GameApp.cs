using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统一定义游戏中的管理器，在此类进行初始化
/// </summary>
public class GameApp :Singleton<GameApp>
{
    public static SoundManager SoundManager; //音频管理器定义

    public static ControllerManager ControllerManager; //控制器管理器

    public static ViewManager ViewManager;

    public static ConfigManager ConfigManager;

    public static CameraManager CameraManager;//摄像机

    public static MessageCenter MsgCenter; //消息监听

    public static TimerManager TimerManager; //时间管理

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
