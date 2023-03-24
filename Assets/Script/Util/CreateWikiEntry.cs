using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateWikiEntry : MonoBehaviour
{
    // Script for generating wiki entries in the Plantomo Wiki page

    // list of plantomo models, add in inspector
    public List<GameObject> listPlantomos;
    public GameObject borderObject;
    public float spaceHorizontal = 250;
    public float spaceVertical = 250;
    public float offsetHorizontal = 100;
    public float offsetVertical = 200;
    public float scaleMultiplier = 40;
    public int numCol = 3;

    public int plantInfoScene;

    public Dictionary<int, string> plantomoDict = new Dictionary<int, string>()
    {
        {0,  "Northern Red Oak"},
        {1,  "Slash Pine"},
        {2,  "Southern Magnolia"},
        {3,  "Star Magnolia"},
        {4,  "Trident Maple"},
    };

    void Start()
    {
        for (int i = 0; i < listPlantomos.Count; ++i)
        {
            GameObject plantomo = listPlantomos[i];

            GameObject wikiEntry = new GameObject("WikiEntry");
            wikiEntry.transform.SetParent(this.gameObject.transform);
            wikiEntry.transform.localPosition = new Vector3(offsetHorizontal + spaceHorizontal * (i % numCol), offsetVertical - spaceVertical * (Mathf.Floor(i / numCol)), 0);
            wikiEntry.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);


            // Instantiate at appropriate position and zero rotation.
            GameObject model = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, wikiEntry.transform);
            model.transform.localPosition = new Vector3(0, 0, 0);
            model.transform.localScale = new Vector3(1, 1, 1);

            foreach (SpriteRenderer s in model.GetComponentsInChildren<SpriteRenderer>())
            {
                s.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }

            // Instantiate the border around each plantomo
            GameObject border = Instantiate(borderObject, new Vector3(0, 0, 0), Quaternion.identity, wikiEntry.transform);
            border.transform.localPosition = new Vector3(0, 0, 0);
            border.transform.localScale = new Vector3(1, 1, 1);


            model.transform.SetParent(wikiEntry.transform);
            border.transform.SetParent(wikiEntry.transform);

            wikiEntry.AddComponent<Button>();

            // Make each entry go to the Plant Info page when clicked

            Debug.Log("i: " + i);

            string name = plantomoDict[i];
            wikiEntry.GetComponent<Button>().onClick.AddListener(() => SelectPlantomo(name));

        }
    }

    public void SelectPlantomo(string name)
    {
        StaticData.SelectedPlantomo = name;
        Debug.Log(StaticData.SelectedPlantomo);
        SceneManager.LoadScene(plantInfoScene);
    }
}