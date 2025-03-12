using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�ؿ�������
public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);
    }

    private void onCloseBtn()
    {
        GameApp.ViewManager.Close(ViewId);

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callback = delegate ()
        {
            //�򿪿�ʼ����
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.LoadingScene, Defines.LoadingScene, loadingModel);

    }


    //��ʾ�ؿ��������
    public void ShowLevelDes()
    {


        Find("level").SetActive(true);
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Des;
    
    }


    //���عؿ��������
    public void HideLevelDes()
    {
        Find("level").SetActive(false);

    }

    //�л���ս������
    private void onFightBtn()
    {
        //�رյ�ǰ����
        GameApp.ViewManager.Close(ViewId);
        //���������λ��
        GameApp.CameraManager.ResetPos();

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().current.SceneName; //��ת��ս������
        loadingModel.callback = delegate ()
        {
            //���سɹ�����ʾս�������
            Controller.ApplyControllerFunc(ControllerType.Fight, Defines.BeginFight);
        };
        Controller.ApplyControllerFunc(ControllerType.LoadingScene, Defines.LoadingScene, loadingModel);

    }

}

