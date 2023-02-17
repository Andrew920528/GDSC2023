using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlantomoFactory : Singleton<PlantomoFactory>
{

    [SerializeField] private Plantomo[] availablePlantomos;
    [SerializeField] private Player player;
    [SerializeField] private float waitTime = 60.0f;
    [SerializeField] private float startingPlantomos = 5;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;

    private void Awake()
    {
        Assert.IsNotNull(availablePlantomos);
        Assert.IsNotNull(player);
    }

    private void InstantiatePlantomos()
    {
        int index = Random.Range(0, availablePlantomos.Length);
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = player.transform.position.z;
        Instantiate(availablePlantomos[index], new Vector3(x, y, z), Quaternion.identity);
    }
}
