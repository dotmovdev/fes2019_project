using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScreenControl : MonoBehaviour
{
    [SerializeField]
    private GameMaster gameMasterRef;

    public int DisplayCount
    {
        get
        {
            return Display.displays.Length;
        }
    }

    [System.Serializable]
    public struct ScreenSetting
    {
        public int Width;
        public int Height;
    }

    [Header("Screen Settings")]
    [SerializeField]
    private List<ScreenSetting> screenSettings = new List<ScreenSetting>();

    public void ActivateDisplays()
    {
        Debug.Log(DisplayCount);

        //複数ディスプレイを検知する
        //1: メインディスプレイ, 2~:複数のサブディスプレイで横長にする。
        //基本的にInspectorの設定に基づく

        for(int i = 0; i < DisplayCount; i++)
        {
            Display.displays[i].Activate();

            if (Display.displays[i].active && i <= screenSettings.Count)
            {
                Display.displays[i].SetRenderingResolution(screenSettings[i].Width, screenSettings[i].Height);
            }
        }
    }
}
