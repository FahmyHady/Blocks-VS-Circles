using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPurchaseManager : MonoBehaviour
{
    // static public int numberOfCirclesBought;
    List<Circle> availableCirclesToBuy = new List<Circle>();
    double currentCircleCost = 100;
    private void OnEnable()
    {
        EventManager.StartListening("Data Loaded", PopulateCirclesList);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Data Loaded", PopulateCirclesList);
    }

    private void PopulateCirclesList()
    {
        availableCirclesToBuy.AddRange(FindObjectsOfType<Circle>());
        for (int i = 0; i < availableCirclesToBuy.Count; i++)
        {
            if (availableCirclesToBuy[i].currentLevel == 0)
            {
                availableCirclesToBuy[i].gameObject.SetActive(false);
            }
            else
            {
                UpdateCirclePrice();
                availableCirclesToBuy[i].Appear();
                availableCirclesToBuy.Remove(availableCirclesToBuy[i]);
                i--;
            }
        }
        CheckIfThereAreMoreCirclesToBuy();
    }


    public void BuyCircle()
    {
        if (PlayerDataManager.GetVerticies() >= currentCircleCost)
        {
            PlayerDataManager.AddVerticies(-currentCircleCost);
            UpdateCirclePrice();
            SuccessfulCirclePurchase();
        }
        else
        {
            //play sound
        }

    }
    void SuccessfulCirclePurchase()
    {
        Circle circleBought = availableCirclesToBuy[0];
        availableCirclesToBuy.Remove(circleBought);
        circleBought.currentLevel = 1;
        circleBought.CalculateValues();
        circleBought.gameObject.SetActive(true);
        circleBought.Appear();
        CheckIfThereAreMoreCirclesToBuy();
    }
    void CheckIfThereAreMoreCirclesToBuy()
    {
        if (availableCirclesToBuy.Count==0)
        {
            EventManager.TriggerEvent("Max Circles Bought");
        }
    }
    void UpdateCirclePrice()
    {
        currentCircleCost *= 10;
        EventManager.TriggerEvent("Circle Cost Updated", currentCircleCost);
    }
}
