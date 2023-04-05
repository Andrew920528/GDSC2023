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

    [SerializeField]
    private int plantomoScale = 25;
    [SerializeField]
    private int spacing = 250;

    public GameObject locationMarker;

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

                if (index < StaticData.plantomoInventory.Count)
                {

                    Plantomo plantomoData = StaticData.plantomoList[StaticData.plantomoInventory[index].Id];

                    string name = plantomoData.Name;

 
                    Plant plantData = StaticData.plantDict[plantomoData.PlantID];


                    GameObject plantomo = plantomoData.PlantomoPrefab;

                    GameObject pc = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, transform);
                    GameObject marker = Instantiate(locationMarker, new Vector3(0, 0, 0), Quaternion.identity, transform);
                    pc.transform.localPosition = new Vector3(offset + j * spacing, i * -spacing + 170, 0);
                    pc.transform.localScale = new Vector3(plantomoScale, plantomoScale, 1);

                    marker.transform.localPosition = new Vector3(offset + j * spacing, i * -spacing+ 130, 0);
                    marker.transform.localScale = new Vector3(30, 30, 1);

                    marker.AddComponent<Button>();
                    marker.GetComponent<Button>().onClick.AddListener(
                    () => PlantomoAssignButtonHandler(index)
                );
                }
                
            }

        }
    }

    void PlantomoAssignButtonHandler(int idx)
    {
        if (idx >= StaticData.plantomoInventory.Count)
        {
            StaticData.SelectedPlantomo = null;
            StaticData.SelectedPotIndex = -1;
        } else
        {
            StaticData.SelectedPlantomo = StaticData.plantomoInventory[idx];
            StaticData.SelectedPotIndex = idx;
        }
        //StaticData.SelectedPlantomo = gameManager.plantomoInventory.Count > idx ? gameManager.plantomoInventory[idx].GetName() : null;
        //Debug.Log(StaticData.SelectedPlantomo);
        SceneManager.LoadScene(potScene);
    }
}
