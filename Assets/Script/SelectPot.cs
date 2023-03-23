using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPot : MonoBehaviour
{
    public GameObject potPrefab;
    public int potScene;
    public float offset = 0;
    private GameManager gameManager;
    private int plantomoIndex;
    public List<GameObject> plantomoList = new List<GameObject>();

    [SerializeField]
    private int plantomoScale = 25;
    [SerializeField]
    private int spacing = 250;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                GameObject pot = Instantiate(potPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
                pot.transform.localPosition = new Vector3(offset + j * spacing, i * -spacing, 0);
                pot.transform.localScale = new Vector3(1, 1, 1);

                int index = 3 * i + j;

                pot.GetComponent<Button>().onClick.AddListener(
                    ()=> PlantomoAssignButtonHandler(index)
                );

                if (index < gameManager.plantomoInventory.Count)
                {
                    string name = gameManager.plantomoInventory[index].GetName();
                    Plantomo plantomoData = StaticData.plantomoDict[name];
                    Plant plantData = plantomoData.GetPlant();

                    GameObject plantomo = plantomoList[plantomoData.GetID()];

                    GameObject pc = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, transform);
                    pc.transform.localPosition = new Vector3(offset + j * spacing, i * -spacing, 0);
                    pc.transform.localScale = new Vector3(plantomoScale, plantomoScale, 1);
                }
                
            }

        }
    }

    void PlantomoAssignButtonHandler(int idx)
    {
        Debug.Log(idx);
        if (idx >= gameManager.plantomoInventory.Count)
        {
            StaticData.SelectedPlantomo = null;
        } else
        {
            StaticData.SelectedPlantomo = gameManager.plantomoInventory[idx].GetName();
        }
        //StaticData.SelectedPlantomo = gameManager.plantomoInventory.Count > idx ? gameManager.plantomoInventory[idx].GetName() : null;
        //Debug.Log(StaticData.SelectedPlantomo);
        SceneManager.LoadScene(potScene);
    }
}
