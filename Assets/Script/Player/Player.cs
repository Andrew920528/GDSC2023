using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int xp = 0;
    [SerializeField] private int requiredxp = 100;
    [SerializeField] private int levelBase = 100;
    [SerializeField] private List<GameObject> plantomos = new List<GameObject>();
    private int level = 1;

    public int Xp {
        get { return xp; }
    }

    public int RequiredXp
    {
        get { return requiredxp; }
    }

    public int LevelBase
    {
        get { return levelBase; }
    }

    public List<GameObject> Plantomos
    {
        get { return plantomos; }
    }

    private void AddXp(int xp)
    {
        this.xp = Mathf.Max(0, xp);
    }

    public void AddPlantomo(GameObject plantomo)
    {
        plantomos.Add(plantomo);
    }

    private void InitLevelData()
    {
        level = (xp / levelBase) + 1;
        requiredxp = levelBase * level;
    }
}
