using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    public void ActivateDisplays()
    {
        //for(int i = 0; i < DisplayCount && i < 2; i++)
        //{
        //    Display.displays[i].Activate();
        //}
    }

    public void FixSecondaryWindow()
    {
        int windowHandle;  //ウィンドウ

        const String WINDOW_NAME = "tsumugu.mov";

        //常に最前面に表示
        const int HWND_TOPMOST = -1;
        //ウィンドウを表示
        const int SWP_SHOWWINDOW = 0x0040;
        //ウィンドウの名称から取得
        windowHandle = NativePlugin.FindWindow(null, WINDOW_NAME);

        //タイトルバーの非表示
        uint style = NativePlugin.GetWindowLong(windowHandle, NativePlugin.GWL_STYLE);
        NativePlugin.SetWindowLong(windowHandle, NativePlugin.GWL_STYLE, (uint)(style ^ NativePlugin.WS_CAPTION));

        NativePlugin.SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, 1920 * 2, 1080, SWP_SHOWWINDOW);
    }
}
