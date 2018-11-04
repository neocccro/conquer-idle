using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    TextMesh text1;
    [SerializeField]
    TextMesh text2;
    
    private Resource cash, mana;

    float workers = 0;
    float workSpeed = 1;

    // Use this for initialization
    void Start()
    {
        ResourceHandler resourceHandler = ResourceHandler.Instance; 
        cash = resourceHandler.GetResource(ResourceType.Wood);
        text1.text = "Workers: " + workers.ToString() + "\n Next workers costs: " + CalcCost(1, 10f, workers).ToString();
        text2.text = "Work speed: " + workSpeed + "\n Next work speed costs: " + CalcCost(10, 10f, workSpeed);
    }
	
	// Update is called once per frame
	void Update()
    {
        cash.Amount += workers * workSpeed;
	}

    public void LeftClick()
    {
        double cost = CalcCost(1f, 10f, workers);
        if(cost < cash.Amount)
        {
            cash.Amount -= cost;
            workers++;
            text1.text = "Workers: " + workers.ToString() + "\n Next workers costs: " + CalcCost(1, 10f, workers).ToString();
        }
    }

    public void RightClick()
    {
        double cost = CalcCost(10f, 10f, workSpeed);
        if (cost < cash.Amount)
        {
            cash.Amount -= cost;
            workSpeed++;
            text2.text = "Work speed: " + workSpeed + "\n Next work speed costs: " + CalcCost(10, 10f, workSpeed);
        }
    }

    public double CalcCost(float _base, float multiplier, float level)
    {
        return _base * Mathf.Pow(multiplier, level);
    }
}
