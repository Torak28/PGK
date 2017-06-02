using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBlueprint
{

    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public bool isUpgr = false;

    public int GetSellAmount()
    {
        if (isUpgr)
        {
            return (cost + upgradeCost) / 2;
        }
        else
        {
            return cost / 2;
        }
    }

}