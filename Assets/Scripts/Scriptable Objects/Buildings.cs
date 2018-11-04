using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Buildings : ScriptableObject
{
    public string tileName;
    public Color tileColor;
    public double costBase;
    public double costIncrease;
    public ResourceType costResource;
    public ResourceType productionResource;
    public ResourceType buildResource;
    public double buildCost;
}
