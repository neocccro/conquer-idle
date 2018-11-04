using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    TextMesh text1;
    [SerializeField]
    TextMesh text2;
    [SerializeField]
    Buildings building;

    private Resource cash;

    float workers = 1;
    float workSpeed = 1;

    // Use this for initialization
    void Start()
    {
        cash = ResourceHandler.Instance.GetResource(building.costResource);
        text1.text = "Workers: " + workers.ToString() + "\n Next workers costs: " + CalcCost(building.costBase, building.costIncrease, workers).ToString();
        text2.text = "Work speed: " + workSpeed + "\n Next work speed costs: " + CalcCost(building.costBase, building.costIncrease, workSpeed);
    }
	
	// Update is called once per frame
	void Update()
    {
        cash.Amount += workers * workSpeed;
	}

    public void LeftClick()
    {
        double cost = CalcCost(building.costBase, building.costIncrease, workers);
        if(cost < cash.Amount)
        {
            cash.Amount -= cost;
            workers++;
            text1.text = "Workers: " + workers.ToString() + "\n Next workers costs: " + CalcCost(building.costBase, building.costIncrease, workers).ToString();
        }
    }

    public void RightClick()
    {
        double cost = CalcCost(building.costBase, building.costIncrease, workSpeed);
        if (cost < cash.Amount)
        {
            cash.Amount -= cost;
            workSpeed++;
            text2.text = "Work speed: " + workSpeed + "\n Next work speed costs: " + CalcCost(building.costBase, building.costIncrease, workSpeed);
        }
    }

    public double CalcCost(double _base, double multiplier, double level)
    {
        return _base * Mathf.Pow((float) multiplier, (float) level);
    }
}
