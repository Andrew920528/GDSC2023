using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using Newtonsoft.Json;
using System.Linq;

public class DataBaseManager : MonoBehaviour
{

    private string userID;

    private DatabaseReference dbReference;

    private FirebaseFirestore firestoreDb;

    private FirebaseUser currentUser;

    public static DataBaseManager instance;

    private bool hasLoad;

    // Start is called before the first frame update

    private void Awake()
    {

        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //} else 
        //{
        //    Destroy(gameObject);
        //}
        hasLoad = false;
    }


    void Start()
    {

        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        firestoreDb = FirebaseFirestore.DefaultInstance;

        // Don't need user data for loading firestore
        LoadFirestoreData();
    }

    // Wrapper Method for loading firebase data for user
    public void Load(string uid)
    {
        StartCoroutine(LoadRealtimeData(uid));
    }

    // Load Firebase Data into session storage, the StaticData file.
    private IEnumerator LoadRealtimeData(string userID)
    {
        var readTask = dbReference.Child("users").Child(userID).GetValueAsync();

        yield return new WaitUntil(() => readTask.IsCompleted);

        if (readTask.Exception != null)
        {
            Debug.LogError("Firebase load failed with exception: " + readTask.Exception);
        }
        else
        {
            Debug.Log("Reatime Database Loaded to Static Data file");
            DataSnapshot snapShot = readTask.Result;
            string json = snapShot.GetRawJsonValue();
            FirebaseData gameData = JsonConvert.DeserializeObject<FirebaseData>(json);

            if (gameData.PlantomoInventory != null)
                StaticData.plantomoInventory = gameData.PlantomoInventory;
            if (gameData.ItemInventory != null)
                StaticData.itemInventory = gameData.ItemInventory;
            if (gameData.PlayerStats != null)
                StaticData.PlayerStats = gameData.PlayerStats;
            if (gameData.QuestTracker != null)
                StaticData.QuestTracker = gameData.QuestTracker;

            GetComponent<LevelSystem>().SetupLeveling();

            hasLoad = true;
        }
    }

    private void LoadFirestoreData()
    {
        StartCoroutine(LoadPlantsDatabase());
        StartCoroutine(LoadQuestionsDatabase());
    }

    private IEnumerator LoadPlantsDatabase()
    {
        var plantsReference = firestoreDb.Collection("PlantsDatabase");
        var readTask = plantsReference.GetSnapshotAsync();

        yield return new WaitUntil(() => readTask.IsCompleted);

        if (readTask.Exception != null)
        {
            Debug.LogError("Firestore read failed with exception: " + readTask.Exception);
        }
        else
        {
            QuerySnapshot snapshot = readTask.Result;

            for (int i = 0; i < snapshot.Count; ++i)
            {
                DocumentSnapshot doc = snapshot[i];

                //string description = JsonConvert.SerializeObject(doc.GetValue<string>(new FieldPath("Description")));
                string json = JsonConvert.SerializeObject(doc);
                int index = doc.GetValue<int>("ID");
                string commonName = doc.GetValue<string>("CommonName");
                string scientificName = doc.GetValue<string>("SpeciesName");
                string description = doc.GetValue<string>("Description");
                string[] distribution = doc.GetValue<string[]>("Nativity");
                string[] climates = doc.GetValue<string[]>("Climate");
                string[] images = doc.GetValue<string[]>("Gallery");
                CareGuide careGuide = doc.GetValue<CareGuide>("CareGuide");
                bool edible = doc.GetValue<bool>("Edible");
                string edibleClarification = doc.GetValue<string>("EdibleClariffication");
                string lifespan = doc.GetValue<string>("LifeSpan");
                string growthRate = doc.GetValue<string>("GrowthRate");
                bool poisonous = doc.GetValue<bool>("Poisonous");
                string[] reproductionMethod = doc.GetValue<string[]>("ReproductionMethod");
                string season = doc.GetValue<string>("Season");

                Plant plant = new Plant(index, commonName, scientificName, distribution,
        climates, description, images, careGuide,
        edible, edibleClarification, lifespan, growthRate,
        poisonous, reproductionMethod, season);

                StaticData.plantDict[plant.Index] = plant;
            }
        }
    }

    private IEnumerator LoadQuestionsDatabase()
    {
        var quetionsReference = firestoreDb.Collection("QuestionsDatabase");
        var readTask = quetionsReference.GetSnapshotAsync();

        yield return new WaitUntil(() => readTask.IsCompleted);

        if (readTask.Exception != null)
        {
            Debug.LogError("Firestore read failed with exception: " + readTask.Exception);
        }
        else
        {
            QuerySnapshot snapshot = readTask.Result;
            StaticData.questionList = new List<QuizQuestion>();

            for (int i = 0; i < snapshot.Count; ++i)
            {
                DocumentSnapshot doc = snapshot[i];

                //string description = JsonConvert.SerializeObject(doc.GetValue<string>(new FieldPath("Description")));
                string json = JsonConvert.SerializeObject(doc);
                string question = doc.GetValue<string>("question");
                List<string> answerChoices = doc.GetValue<List<string>>("answer_choices");
                int answerIndex = doc.GetValue<int>("answer_index");

                QuizQuestion q = new(question, answerChoices, answerIndex);

                StaticData.questionList.Add(q);
            }
        }
    }


    public IEnumerator  SaveData()
    {
        // Don't save before login
        if (SceneManager.GetActiveScene().buildIndex < 2 || !hasLoad)
        {
            yield break;
        }
        currentUser = FirebaseAuth.GetAuth(app: FirebaseDatabase.DefaultInstance.App).CurrentUser;
        if (currentUser == null) yield break;

        FirebaseData gameData = new FirebaseData(
                stats: StaticData.PlayerStats,
                quests: StaticData.QuestTracker,
                plantomoInv: StaticData.plantomoInventory,
                itemInv: StaticData.itemInventory
            );
        userID = currentUser.UserId;
        string data = JsonConvert.SerializeObject(gameData);

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
