using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    TextMesh titleText;
    [SerializeField]
    TextMesh workersText;
    [SerializeField]
    Buildings building;
    

    private Resource resource;

    float level = 1;

    // Use this for initialization
    void Start()
    {
        resource = ResourceHandler.Instance.GetResource(building.costResource);
        workersText.text = "Workers: " + level.ToString() + "\n Next workers costs: " + CalcCost(building.costBase, building.costIncrease, level).ToString();
    }
	

    public void BuyOne()
    {
        double cost = CalcCost(building.costBase, building.costIncrease, level);
        if(cost < resource.Amount)
        {
            resource.Amount -= cost;
            level++;
            workersText.text = "Workers: " + level.ToString() + "\n Next workers costs: " + CalcCost(building.costBase, building.costIncrease, level).ToString();
        }
    }

    public double CalcCost(double _base, double multiplier, double _level)
    {
        return _base * Mathf.Pow((float) multiplier, (float) _level);
    }
}
