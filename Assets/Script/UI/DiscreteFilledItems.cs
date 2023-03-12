using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscreteFilledItems : MonoBehaviour
{

    public Image[] items;
    public Sprite filledItem;
    public Sprite emptyItem;

    public int initFilled;
    private int currFilled;

    private void Start()
    {
        currFilled = 0;
        FillNext(initFilled);
    }

    public void FillNext(int num)
    {
        if (currFilled + num > items.Length)
        {
            return;
        }
        for (int i = currFilled; i < currFilled+num; i++)
        {
            items[i].sprite = filledItem;
            
        }
        currFilled += num;
    }

    public void FillNext()
    {
        FillNext(1);
    }

    public void UnfillPrev(int num)
    {
        if (currFilled - num < 0)
        {
            return;
        }
        for (int i = currFilled-1; i > currFilled - 1 - num; i--)
        {
            items[i].sprite = emptyItem;
        }
        currFilled -= num;
    }

    public void UnfillPrev()
    {
        UnfillPrev(1);
    }

    public void FillAtIndex(int i)
    {
        items[i].sprite = filledItem;
    }

    public void UnFillAtIndex(int i)
    {
        items[i].sprite = emptyItem;
    }

}
