using System;
using UnityEngine;

public class AppLinkHandler : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        HandleAndroidIntent();
#endif
    }

#if UNITY_ANDROID
    void HandleAndroidIntent()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayerActivity"))
        {
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");

            string action = intent.Call<string>("getAction");
            if (action == "android.intent.action.VIEW")
            {
                string url = intent.Call<string>("getDataString");

                if (!string.IsNullOrEmpty(url))
                {
                    if (url.Contains("guitar-memos://share"))
                    {
                        string progressionId = ExtractProgressionId(url);
                        FirebaseManager.Instance.LoadProgression(progressionId);
                    }
                }
            }
        }
    }
#endif

    string ExtractProgressionId(string url)
    {
        Uri uri = new Uri(url);
        var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
        return queryParams["Id"];
    }
}
