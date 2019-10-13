using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NativePlugin : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern int FindWindow(String className, String windowName);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern int SetWindowPos(int hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
    public static extern int GetDesktopWindow();

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    public static extern uint GetWindowLong(int hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
    public static extern uint SetWindowLong(int hWnd, int nIndex, uint dwNewLong);

    //ウィンドウのスタイル変更
    public static readonly int GWL_STYLE = -16;
    //境界線を持つウィンドウの作成
    public static readonly int WS_BORDER = 0x00800000;
    //ダイアログボックスのスタイル
    public static readonly int WS_DLGFRAME = 0x00400000;
    //タイトルバーを持つウィンドウを作成
    public static readonly int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
}
