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
}
