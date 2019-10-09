using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class NativePlugin
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int buttom;
    }

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern int FindWindow(String className, String windowName);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern int SetWindowPos(int hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
    public static extern int GetDesktopWindow();

    [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
    public static extern bool GetWindowRect(int hWnd, out RECT rect);

    
}

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
        Display.displays[0].Activate();
        Display.displays[1].Activate();
    }

    public void FixSecondaryWindow()
    {
        int firstWindowHandle;  //ウィンドウ
        int secondWindowHandle;  //ウィンドウ

        const String FIRST_WINDOW_NAME = "fes2019";
        const String SECOND_WINDOW_NAME = "Unity Secondary Display";

        //常に最前面に表示
        const int HWND_TOPMOST = -1;
        //ウィンドウを表示
        const int SWP_SHOWWINDOW = 0x0040;

        firstWindowHandle = NativePlugin.FindWindow(null, FIRST_WINDOW_NAME);
        secondWindowHandle = NativePlugin.FindWindow(null, SECOND_WINDOW_NAME);

        NativePlugin.SetWindowPos(firstWindowHandle, HWND_TOPMOST, 0, 0, 1920 * 2, 1080, SWP_SHOWWINDOW);
        NativePlugin.SetWindowPos(secondWindowHandle, HWND_TOPMOST, 1920 * 2, 0, 1920/2, 1080, SWP_SHOWWINDOW);
    }
}
