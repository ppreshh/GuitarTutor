using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ClipboardUtils
{
    // Method to copy text to clipboard
    public static void CopyToClipboard(string text)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android-specific clipboard handling
            CopyToClipboardAndroid(text);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // iOS-specific clipboard handling
            CopyToClipboardIOS(text);
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
        {
            // Desktop (Windows, macOS, Linux) clipboard handling
            CopyToClipboardDesktop(text);
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.LinuxEditor)
        {
            // Unity Editor clipboard handling
            CopyToClipboardEditor(text);
        }
        else
        {
            Debug.LogWarning("Clipboard access is not supported on this platform.");
        }
    }

    // Android-specific clipboard copy
    private static void CopyToClipboardAndroid(string text)
    {
        using (AndroidJavaClass clipboardClass = new AndroidJavaClass("android.content.ClipboardManager"))
        {
            using (AndroidJavaObject context = GetAndroidContext())
            {
                AndroidJavaObject clipboardManager = context.Call<AndroidJavaObject>("getSystemService", "clipboard");

                using (AndroidJavaClass clipDataClass = new AndroidJavaClass("android.content.ClipData"))
                {
                    AndroidJavaObject clipData = clipDataClass.CallStatic<AndroidJavaObject>("newPlainText", "CopiedText", text);
                    clipboardManager.Call("setPrimaryClip", clipData);
                }
            }
        }
    }

    // iOS-specific clipboard copy
    private static void CopyToClipboardIOS(string text)
    {
        // iOS clipboard access requires a native plugin (not available by default in Unity)
        Debug.Log("Copy to clipboard on iOS: " + text);
        // Example iOS native code (using a plugin) would go here
    }

    // Desktop-specific clipboard copy (Windows, macOS, Linux)
    private static void CopyToClipboardDesktop(string text)
    {
#if UNITY_STANDALONE_WIN
        // Windows-specific clipboard handling (using System.Windows.Forms)
        System.Windows.Forms.Clipboard.SetText(text);
#elif UNITY_STANDALONE_OSX
        // macOS-specific clipboard handling
        var p = new System.Diagnostics.Process();
        p.StartInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "osascript",
            Arguments = $"set the clipboard to \"{text}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        p.Start();
#elif UNITY_STANDALONE_LINUX
        // Linux-specific clipboard handling (using xclip or xsel)
        var p = new System.Diagnostics.Process();
        p.StartInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "xclip",
            Arguments = "-selection clipboard",
            RedirectStandardInput = true,
            UseShellExecute = false
        };
        p.Start();
        p.StandardInput.WriteLine(text);
        p.StandardInput.Close();
        p.WaitForExit();
#endif
    }

    // Unity Editor-specific clipboard copy
    private static void CopyToClipboardEditor(string text)
    {
#if UNITY_EDITOR
        // Use UnityEditor functionality for clipboard in the Editor
        EditorGUIUtility.systemCopyBuffer = text;
#endif
    }

    // Helper method to get the Android context
    private static AndroidJavaObject GetAndroidContext()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}
