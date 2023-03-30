using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using Newtonsoft.Json;

public class DataBaseManager : MonoBehaviour
{

    private string userID;

    private DatabaseReference dbReference;

    private FirebaseFirestore firestoreDb;

    private DataManager dataManager;

    private FirebaseUser currentUser;

    private static GameObject dbInstance;

    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (dbInstance == null)
        {
            dbInstance = gameObject;
        } else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        firestoreDb = FirebaseFirestore.DefaultInstance;

        dataManager = GetComponent<DataManager>();
        dataManager.Load();
    }


    public IEnumerator SaveData()
    {
        currentUser = FirebaseAuth.GetAuth(app: FirebaseDatabase.DefaultInstance.App).CurrentUser;
        if (currentUser == null) yield return null;
        DataManager.Data gameData = dataManager.GetGameData();
        userID = currentUser.UserId;
        string data = JsonConvert.SerializeObject(gameData);
        Debug.Log(data);

        Debug.Log("================== PI ====================");
        Debug.Log(gameData.plantomoInventory[gameData.plantomoInventory.Count - 1].Name);

        //string plantomoData = JsonUtility.ToJson(gameData.plantomoInventory);
        //dbReference.Child("users").Child(userID).Child("plantomo_inventory").SetRawJsonValueAsync(plantomoData);

        var writeTask = dbReference.Child("users").Child(userID).SetRawJsonValueAsync(data);

        yield return new WaitUntil(() => writeTask.IsCompleted);

        if (writeTask.Exception != null)
        {
            // error writing to firebase
            Debug.LogWarning(message: $"Failed to register task with {writeTask.Exception}");
         
        } else
        {
            Debug.Log("written to Firebase successfully");
        }
        
    }

    public void OnApplicationFocus(bool focus)
    {
        if (!focus)
        StartCoroutine(SaveData());
    }

    public void OnApplicationQuit()
    {
        StartCoroutine(SaveData());
    }
}
