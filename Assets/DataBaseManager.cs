using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DataBaseManager : MonoBehaviour
{
    private string name;
    private string email;

    private string userID;

    private DatabaseReference dbReference;
    // Start is called before the first frame update
    void Start()
    {
        name = "Martin Luther Kim";
        email = "mlkim72@gatech.edu";

        userID = SystemInfo.deviceUniqueIdentifier;
        
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUser()
    {
        User newUser = new User(name, email);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
}
