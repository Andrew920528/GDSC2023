using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    Animator anim;
    public GameObject mainPopUp;
    public GameObject[] otherProps;
    

    void Start()
    {
        anim = mainPopUp.GetComponent<Animator>();
        mainPopUp.SetActive(false);
        foreach (GameObject o in otherProps)
        {
            o.SetActive(false);
        }
    }



    public void PopUp()
    {
        mainPopUp.SetActive(true);
        anim.Play("popup");
        foreach (GameObject o in otherProps)
        {
            o.SetActive(true);
        }
    }
    public void PopOut()
    {
        StartCoroutine(HandleMainPopOut());
        foreach (GameObject o in otherProps)
        {
            o.SetActive(false);
        }
    }


    private IEnumerator HandleMainPopOut()
    {
        anim.Play("popout");
        yield return new WaitForSeconds(0.2f);
        mainPopUp.SetActive(false);
    }

}
