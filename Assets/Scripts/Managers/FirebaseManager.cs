using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using System;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    private bool m_IsFirebaseReady = false;
    private FirebaseApp m_App = null;
    private DatabaseReference m_Database = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                m_App = FirebaseApp.DefaultInstance;
                m_Database = FirebaseDatabase.DefaultInstance.RootReference;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                m_IsFirebaseReady = true;

                Debug.Log("Firebase Initialized");
            }
            else
            {
                Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void SaveProgression(Progression progression)
    {
        string json = JsonConvert.SerializeObject(progression);
        Debug.Log(json);

        string progressionId = HashUtils.GenerateHash(json);

        m_Database.Child("progressions").Child(progressionId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                if (task.Result.Exists)
                {
                    Debug.Log("Progression already exists. Skipping save.");

                    ClipboardUtils.CopyToClipboard($"guitar-memos://share?Id={progressionId}");
                    UIManager.Instance.ShowNotification("Share link copied to clipboard");
                }
                else
                {
                    // Save the new progression
                    m_Database.Child("progressions").Child(progressionId).SetRawJsonValueAsync(json)
                        .ContinueWithOnMainThread(saveTask =>
                        {
                            if (saveTask.IsCompleted && !saveTask.IsFaulted)
                            {
                                Debug.Log("Chord progression saved to database!");

                                ClipboardUtils.CopyToClipboard($"guitar-memos://share?Id={progressionId}");
                                UIManager.Instance.ShowNotification("Share link copied to clipboard");
                            }
                            else
                            {
                                Debug.LogError("Error saving progression: " + saveTask.Exception?.Message);
                            }
                        });
                }
            }
            else
            {
                Debug.LogError("Error checking if progression exists: " + task.Exception?.Message);
            }
        });
    }

    public void LoadProgression(string id)
    {
        m_Database.Child("progressions").Child(id).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                if (task.Result.Exists)
                {
                    var progression = JsonConvert.DeserializeObject<Progression>(JsonConvert.SerializeObject(task.Result.Value)); // lol, not great
                    ProgressionsManager.Instance.AddProgression(progression);

                    UIManager.Instance.ShowMessage($"Added Progression: <b>{progression.Name}</b>", "Close");
                }
                else
                {
                    UIManager.Instance.ShowMessage("This link is expired.", "Close");
                }
            }
            else
            {
                Debug.LogError("Error checking if progression exists: " + task.Exception?.Message);
            }
        });
    }
}
