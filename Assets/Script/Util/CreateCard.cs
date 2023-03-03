using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCard : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject nameCard;
    public GameObject multiPurposeCard;
    public GameObject imageCard;
    public GameObject distrCard;
    public GameObject careCard;
    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        GameObject nc = Instantiate(nameCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        nc.transform.localPosition = new Vector3(0, 0, 0);
        nc.transform.localScale = new Vector3(1, 1, 1);

        GameObject mpc = Instantiate(multiPurposeCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        mpc.transform.localPosition = new Vector3(0, 0, 0);
        mpc.transform.localScale = new Vector3(1, 1, 1);

        GameObject ic = Instantiate(imageCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        ic.transform.localPosition = new Vector3(0, 0, 0);
        ic.transform.localScale = new Vector3(1, 1, 1);

        GameObject distr = Instantiate(distrCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        distr.transform.localPosition = new Vector3(0, 0, 0);
        distr.transform.localScale = new Vector3(1, 1, 1);

        GameObject cc = Instantiate(careCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        careCard.transform.localPosition = new Vector3(0, 0, 0);
        careCard.transform.localScale = new Vector3(1, 1, 1);


    }
}
