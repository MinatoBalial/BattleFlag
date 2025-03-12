using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// —°‘Ò”¢–€√Ê∞Â
/// </summary>
public class FightSelectHeroView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);

        ////

        for (int i = 0; i < GameApp.GameDataManager.heros.Count; i++) 
        { 
            Dictionary<string,string> data = GameApp.ConfigManager.GetConfigDdata("player").GetDataById(GameApp.GameDataManager.heros[i]);
        
        }
    }
}
