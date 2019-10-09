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
        int handle;  //ウィンドウ

        const String WINDOW_NAME = "Unity Secondary Display";

        //常に最前面に表示
        const int HWND_TOPMOST = -1;
        //ウィンドウを表示
        const int SWP_SHOWWINDOW = 0x0040;

        int display = NativePlugin.GetDesktopWindow();

        NativePlugin.RECT rect = new NativePlugin.RECT();
        NativePlugin.GetWindowRect(display, out rect);

        int w = rect.right;
        int h = rect.buttom;

        handle = NativePlugin.FindWindow(null, WINDOW_NAME);

        NativePlugin.SetWindowPos(handle, HWND_TOPMOST, 1920 * 2, 0, 1920, 1080, SWP_SHOWWINDOW);
        Screen.SetResolution(1920, 1080, true);
    }
}
