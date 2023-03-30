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

    public List<GameObject> plantomoList = new List<GameObject>();

    [SerializeField]
    private int plantomoScale = 25;
    [SerializeField]
    private int spacing = 250;

    // Start is called before the first frame update
    void Start()
    {
        
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

                Debug.Log(StaticData.plantomoInventory.Count);
                if (index < StaticData.plantomoInventory.Count)
                {
                    string name = StaticData.plantomoInventory[index].Name;
                    Plantomo plantomoData = StaticData.plantomoDict[name];
                    Plant plantData = StaticData.plantDict[plantomoData.PlantID];

                    GameObject plantomo = plantomoList[plantomoData.Id];

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
        if (idx >= StaticData.plantomoInventory.Count)
        {
            StaticData.SelectedPlantomo = null;
            StaticData.SelectedPlantomoIndex = -1;
        } else
        {
            StaticData.SelectedPlantomo = StaticData.plantomoInventory[idx].Name;
            StaticData.SelectedPlantomoIndex = idx;
        }
        //StaticData.SelectedPlantomo = gameManager.plantomoInventory.Count > idx ? gameManager.plantomoInventory[idx].GetName() : null;
        //Debug.Log(StaticData.SelectedPlantomo);
        SceneManager.LoadScene(potScene);
    }
}
