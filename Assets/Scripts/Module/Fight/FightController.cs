using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 战斗控制器（战斗相关的界面，事件等）
/// </summary>
public class FightController : BaseController 
{

    public FightController() : base()
    {
        GameApp.ViewManager.Register(ViewType.FightView, new ViewInfo()
        {
            PrefabName = "FightView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });
        GameApp.ViewManager.Register(ViewType.FightSelectHeroView, new ViewInfo()
        {
            PrefabName = "FightSelectHeroView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 1
        });
        GameApp.ViewManager.Register(ViewType.DragHeroView, new ViewInfo()
        {
            PrefabName = "DragHeroView",
            controller = this,
            parentTf = GameApp.ViewManager.worldCanvasTf,//设置到世界画布
            Sorting_Order = 2
        });
        InitModuleEvent();
            
    }
    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.BeginFight, onBeginFightCallback);

    }

    private void onBeginFightCallback(System.Object[] arg)
    {
        GameApp.FightManager.ChangeState(GameState.Enter);
        GameApp.ViewManager.Open(ViewType.FightView);
        GameApp.ViewManager.Open(ViewType.FightSelectHeroView);
    }
}
