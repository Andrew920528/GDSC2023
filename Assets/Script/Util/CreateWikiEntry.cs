using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CreateWikiEntry : MonoBehaviour
{
    // Script for generating all plantomo entries in the Plantomo Wiki page

    // list of plantomo models, add in inspector
    // public List<GameObject> listPlantomos;
    public GameObject borderObject;
    public GameObject lockedSquare;
    public float spaceHorizontal = 250;
    public float spaceVertical = 250;
    public float offsetHorizontal = 100;
    public float offsetVertical = 100;
    public float scaleMultiplier = 40;
    public int numCol = 3;
    public int totalEntry;

    public int plantInfoScene;

    // StaticData.plantomoList[1];

    void Start()
    {
        for (int i = 0; i < StaticData.plantomoList.Count; ++i)
        {
            // Store plantomo prefab in static data too
            GameObject plantomo = StaticData.plantomoList[i].PlantomoPrefab;

            GameObject wikiEntry = new GameObject("WikiEntry");
            wikiEntry.transform.SetParent(this.gameObject.transform);
            wikiEntry.transform.localPosition = new Vector3(offsetHorizontal + spaceHorizontal * (i % numCol), offsetVertical - spaceVertical * (Mathf.Floor(i / numCol)), 0);
            wikiEntry.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);


            // Instantiate at appropriate position and zero rotation.
            GameObject model = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, wikiEntry.transform);
            model.transform.localPosition = new Vector3(0, -0.5f, 0);
            model.transform.localScale = new Vector3(1, 1, 1);
            model.GetComponent<Animator>().enabled = false;

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
            int ind = i;
            
            wikiEntry.GetComponent<Button>().onClick.AddListener(() => SelectPlantomo(ind));
        }

        GenerateLockedSquares();
    }

    private void GenerateLockedSquares()
    {
        for (int i = StaticData.plantomoList.Count; i < totalEntry; ++i)
        {
            
            GameObject wikiEntry = new GameObject("WikiEntry");
            wikiEntry.transform.SetParent(this.gameObject.transform);
            wikiEntry.transform.localPosition = new Vector3(offsetHorizontal + spaceHorizontal * (i % numCol), offsetVertical - spaceVertical * (Mathf.Floor(i / numCol)), 0);
            wikiEntry.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);

            // Instantiate the border around each plantomo
            GameObject border = Instantiate(lockedSquare, new Vector3(0, 0, 0), Quaternion.identity, wikiEntry.transform);
            border.transform.localPosition = new Vector3(0, 0, 0);
            border.transform.localScale = new Vector3(1, 1, 1);
            border.transform.SetParent(wikiEntry.transform);
        }
    }

    public void SelectPlantomo(int i)
    {
        StaticData.SelectedPlantomo = StaticData.plantomoList[i];
        Debug.Log(StaticData.SelectedPlantomo);
        SceneManager.LoadScene(plantInfoScene);
    }
}