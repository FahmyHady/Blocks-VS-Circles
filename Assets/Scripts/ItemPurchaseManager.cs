using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ItemPurchaseManager : MonoBehaviour
{
    // static public int numberOfCirclesBought;
    List<Circle> availableCirclesToBuy = new List<Circle>();
    double currentCircleCost;
    [SerializeField] Sprite[] circlesPrestigeSprites;
    private void OnEnable()
    {
        EventManager.StartListening("Data Loaded", PopulateCirclesList);
        EventManager.StartListening("Prestiged", ResetCircles);
        EventManager.StartListening("Prestige Process Over", AssignSprites);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Data Loaded", PopulateCirclesList);
        EventManager.StopListening("Prestiged", ResetCircles);
        EventManager.StopListening("Prestige Process Over", AssignSprites);

    }

    private void PopulateCirclesList()
    {
        currentCircleCost = Mathf.Pow(10, 1 + PlayerDataManager.prestigeLevel); //Intialize circle cost
        if (PlayerDataManager.prestigeLevel > 0)
        {
            UpdateCirclePrice();
        }
        availableCirclesToBuy.AddRange(FindObjectsOfType<Circle>());
        AssignSprites();
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

    private void AssignSprites()
    {
        for (int i = 0; i < availableCirclesToBuy.Count; i++)
        {
            availableCirclesToBuy[i].spriteRenderer.sprite = circlesPrestigeSprites[PlayerDataManager.prestigeLevel % circlesPrestigeSprites.Length];
        }
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
            AudioManager.PlaySound("Not Enough");
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
        if (availableCirclesToBuy.Count == 0)
        {
            EventManager.TriggerEvent("Max Circles Bought");
        }
    }
    void UpdateCirclePrice()
    {
        currentCircleCost *= 10;
        EventManager.TriggerEvent("Circle Cost Updated", currentCircleCost);
    }
    void ResetCircles()
    {
        currentCircleCost = 100 * Mathf.Pow(10, PlayerDataManager.prestigeLevel); //Intialize circle cost
        availableCirclesToBuy.AddRange(FindObjectsOfType<Circle>());
        for (int i = 0; i < availableCirclesToBuy.Count; i++)
        {
            availableCirclesToBuy[i].PlayerPrestiged();
        }
        EventManager.TriggerEvent("Circle Cost Updated", currentCircleCost);
    }
}
