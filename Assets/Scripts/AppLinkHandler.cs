using System;
using UnityEngine;

public class AppLinkHandler : MonoBehaviour
{
    void Start()
    {
        // Capture the URL when the app is launched
        string url = Application.absoluteURL;
        if (!string.IsNullOrEmpty(url) && url.Contains("guitar-memos://share"))
        {
            string progressionId = ExtractProgressionId(url);
            FirebaseManager.Instance.LoadProgression(progressionId);
        }
    }

    string ExtractProgressionId(string url)
    {
        // Example: your-app://share?Id=<unique_id>
        Uri uri = new Uri(url);
        var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
        return queryParams["Id"];
    }
}
