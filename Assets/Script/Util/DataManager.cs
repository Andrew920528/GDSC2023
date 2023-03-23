using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class DataManager : MonoBehaviour
{
    // Class for storing player data

    private Data gameData;
    private static string dataFilePath = Path.Combine(Application.persistentDataPath, "GameData.json");

    public DataManager(int level = 0, int currentExperience = 0, double totalDistance = 0,
        Dictionary<int, QuestData> questTracker = null, List<Plantomo> plantomoInventory = null, int plantomoID = 0
)
    {
        gameData = new Data();
        gameData.level = level;
        gameData.currentExperience = currentExperience;
        gameData.totalDistance = totalDistance;
        gameData.questTracker = questTracker;
        gameData.plantomoInventory = plantomoInventory;
        gameData.plantomoID = plantomoID;
    }


    // Here we set our level with some sort of GameManager
    public void SetLevel(int level)
    {
        if (gameData == null)
        {
            gameData = new Data();
        }

        gameData.level = level;
    }

    public void SetExperience(int currentExperience)
    {
        if (gameData == null)
        {
            gameData = new Data();
        }

        gameData.currentExperience = currentExperience;
    }

    public void SetQuests(List<Quest> quests)
    {
        if (gameData == null)
        {
            gameData = new Data();
        }
        gameData.questTracker = new Dictionary<int, QuestData>();
        for (int i = 0; i < quests.Count; ++i)
        {
            Quest quest = quests[i];
            gameData.questTracker[i] = new QuestData(i, quest.Goals[0].GetType().ToString(),
                quest.Goals[0].CurrentAmount, quest.Goals[0].RequiredAmount, quest.Goals[0].Completed);
        }
    }

    public void SetInventory(List<Plantomo> plantomos)
    {
        if (gameData == null)
        {
            gameData = new Data();
        }
        gameData.plantomoInventory = new List<Plantomo>();
        foreach (Plantomo plantomo in plantomos) if (plantomo.GetName() != null)
        {
         
            gameData.plantomoInventory.Add(plantomo);
            Debug.Log(plantomo.GetName());
        }
        Debug.Log(gameData.plantomoInventory[gameData.plantomoInventory.Count - 1].GetName());
        Debug.Log(gameData.plantomoInventory);
    }

    public void SetDistance(double distance)
    {
        if (gameData == null)
        {
            gameData = new Data();
        }

        gameData.totalDistance += distance;
    }

    public void SetPlantomoID()
    {
        if (gameData == null)
        {
            gameData = new Data();
        }
        gameData.plantomoID++;
    }

    // The method to return the loaded game data when needed
    public Data GetGameData()
    {
        return gameData;
    }

    public void Save()
    {
        Debug.Log("saved to " + dataFilePath);
        // This creates a new StreamWriter to write to a specific file path
        using (StreamWriter file = File.CreateText(dataFilePath))
        {
            // This will convert our Data object into a string of JSON
            string dataToWrite = JsonConvert.SerializeObject(gameData.plantomoInventory);

            //string dataToWrite = JsonConvert.SerializeObject(gameData, Formatting.Indented);

            Debug.Log(dataToWrite);

            JsonSerializer serializer = new JsonSerializer();

            // This is where we actually write to the file
            serializer.Serialize(file, gameData);
            file.Close();
        }
    }

    public void Load()
    {
        Debug.Log("load called");

        // This creates a StreamReader, which allows us to read the data from the specified file path
        using (StreamReader reader = new StreamReader(dataFilePath))
        {
            // We read in the file as a string
            string dataToLoad = reader.ReadToEnd();

            // if the json file is empty
            if (dataToLoad.Length == 0)
            {
                gameData = new Data();
            }
            else
            {
                //// Here we convert the JSON formatted string into an actual Object in memory
                ///
                var jsonResult = JsonConvert.DeserializeObject(dataToLoad).ToString();

                gameData = JsonConvert.DeserializeObject<Data>(jsonResult);

                Debug.Log("quest: " + gameData.questTracker);
                Debug.Log("total distance: " + gameData.totalDistance);
            }

            reader.Close();


        }
        //using (StreamReader file = File.OpenText(dataFilePath))
        //{
        //    // We read in the file as a string
        //    JsonSerializer serializer = new JsonSerializer();

        //    // Here we convert the JSON formatted string into an actual Object in memory
        //    gameData = (Data) serializer.Deserialize(file, typeof(Data));
        //    Debug.Log("quest: " + gameData.questTracker);
        //    Debug.Log("total distance: " + gameData.totalDistance);
        //    file.Close();
        //}
    }


    [System.Serializable]
    public class Data
    {
        // The actual data we want to save goes here, for this example we'll only use an integer to represent the level
        public int level = 0;
        public int currentExperience = 0;
        public Dictionary<int, QuestData> questTracker = null;
        public double totalDistance = 0;
        public List<Plantomo> plantomoInventory = null;
        public int plantomoID = 0;

        public Data()
        {
            level = 0;
            currentExperience = 0;
            questTracker = new Dictionary<int, QuestData>();
            totalDistance = 0;
            plantomoInventory = new List<Plantomo>();
            plantomoID = 0;

        }
    }

    [System.Serializable]
    public class QuestData
    {
        public int index = 0;
        public string questType = null;
        public double currentAmount = 0;
        public double requiredAmount = 1;
        public bool completed = false;

        public QuestData(int index, string questType, double currentAmount, double requiredAmount, bool completed)
        {
            this.index = index;
            this.questType = questType;
            this.currentAmount = currentAmount;
            this.requiredAmount = requiredAmount;
            this.completed = completed;

        }
    }

}