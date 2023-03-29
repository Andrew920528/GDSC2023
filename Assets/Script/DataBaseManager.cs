using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DataBaseManager : MonoBehaviour
{

    private string userID;

    private DatabaseReference dbReference;

    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        dataManager = GetComponent<DataManager>();
    }


    public void SaveData(DataManager.Data data)
    {
        string json = JsonUtility.ToJson(data);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    public FirebaseDatabase GetData()
    {
        return dbReference.Database;
    }
}
