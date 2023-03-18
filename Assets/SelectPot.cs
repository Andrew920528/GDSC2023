using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPot : MonoBehaviour
{
    public GameObject potPrefab;
    public int potScene;
    public float offset = -250;

    public List<Plantomo> plantomoList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                GameObject pot = Instantiate(potPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
                pot.transform.localPosition = new Vector3(offset + j * 250, i * 250, 0);
                pot.transform.localScale = new Vector3(1, 1, 1);

                pot.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(potScene));
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
