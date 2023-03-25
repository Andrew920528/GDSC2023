using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

using System.Linq;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string name;
        public string description;
    }

    [Header("Info")] public Info Information;

    [System.Serializable]
    public struct Stat
    {
        public int currency;
        public int XP;
    }

    [Header("Reward")] public Stat Reward = new Stat { currency = 10, XP = 10 };

    public bool Completed { get; set; }
    public QuestCompletedEvent QuestCompleted;

    public abstract class QuestGoal : ScriptableObject
    {
        protected string Description;
        public double CurrentAmount { get; set; }
        public double RequiredAmount = 1;

        public bool Completed { get; set; }
        [HideInInspector] public UnityEvent GoalCompleted;

        // Virtual means allowed to be overriden by classes inheriting from this
        public virtual string GetDescription()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if (CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }

        public void Complete()
        {
            //CurrentAmount = RequiredAmount;
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        public void Skip()
        {
            // charge the player some currency to skip the quest
        }
    }

    public List<QuestGoal> Goals;

    public void Initialize()
    {
        Completed = false;
        QuestCompleted = new QuestCompletedEvent();
        Debug.Log("quest initialize");
        foreach (var goal in Goals)
        {
            Debug.Log("goal");
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            // give reward
            QuestCompleted.Invoke(this);
            QuestCompleted.RemoveAllListeners();
        }
    }

    public class QuestCompletedEvent : UnityEvent<Quest> { }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Quest))]

    public class QuestEditor : Editor
    {
        SerializedProperty m_QuestInfoProperty;
        SerializedProperty m_QuestStatProperty;

        List<string> m_QuestGoalType;
        SerializedProperty m_QuestGoalListProperty;

        [MenuItem("Assets/Quest", priority = 0)]
        public static void CreateQuest()
        {
            var newQuest = CreateInstance<Quest>();

            ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
        }

        private void OnEnable()
        {
            m_QuestInfoProperty = serializedObject.FindProperty(nameof(Quest.Information));
            m_QuestStatProperty = serializedObject.FindProperty(nameof(Quest.Reward));

            m_QuestGoalListProperty = serializedObject.FindProperty(nameof(Quest.Goals));

            var lookup = typeof(Quest.QuestGoal);

            // Loads all the classes that inherit from our quest goal class
            m_QuestGoalType = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
                .Select(type => type.Name)
                .ToList();
        }

        public override void OnInspectorGUI()
        {
            var child = m_QuestInfoProperty.Copy();
            var depth = child.depth;
            child.NextVisible(true);

            EditorGUILayout.LabelField("Quest info", EditorStyles.boldLabel);
            while (child.depth > depth)
            {
                EditorGUILayout.PropertyField(child, true);
                child.NextVisible(false);
            }

            child = m_QuestStatProperty.Copy();
            depth = child.depth;
            child.NextVisible(true);

            EditorGUILayout.LabelField("Quest reward", EditorStyles.boldLabel);
            while (child.depth > depth)
            {
                EditorGUILayout.PropertyField(child, true);
                child.NextVisible(false);
            }

            // Dropdown menu with all goals we have 
            int choice = EditorGUILayout.Popup("Add new Quest Goal", -1,
                m_QuestGoalType.ToArray());


            // If choice is not empty 
            if (choice != -1)
            {
                var newInstance = ScriptableObject.CreateInstance(m_QuestGoalType[choice]);

                AssetDatabase.AddObjectToAsset(newInstance, target);

                m_QuestGoalListProperty.InsertArrayElementAtIndex(m_QuestGoalListProperty.arraySize);
                m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1)
                    .objectReferenceValue = newInstance;
            }

            Editor ed = null;
            int toDelete = -1;
            for (int i = 0; i < m_QuestGoalListProperty.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                var item = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
                SerializedObject obj = new SerializedObject(item.objectReferenceValue);

                Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

                ed.OnInspectorGUI();
                EditorGUILayout.EndVertical();

                if (GUILayout.Button("-", GUILayout.Width(32)))
                {
                    toDelete = i;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (toDelete != -1)
            {
                var item = m_QuestGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
                DestroyImmediate(item, true);

                // need to do it twice, first time just modify the entry, second actually remove it
                m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
                m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);     
            }

            serializedObject.ApplyModifiedProperties();
        }

        
    }

    #endif
}
