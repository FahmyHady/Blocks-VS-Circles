using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject idleGainPanel;
    public Animator transitionPanelAnimator;
    public Text currentVerticiesCount;
    public Button upgradeTapperButton;
    public Text upgradeTapperCostText;
    public Text buyACirlceCostText;
    public Button buyACirlceButton;
    public Button prestigeButton;
    public Text idleVerticiesCount;
    public Text idleTimeAway;
    public Text prestigeCostText;
    private void OnEnable()
    {
        EventManager.StartListening("Gameplay Scene Loaded", GameSceneLoadedProcedure);
        EventManager.StartListening("Verticies Updated", UpdateVerticiesCount);
        EventManager.StartListening("Circle Cost Updated", UpdateCircleCost);
        EventManager.StartListening("Prestige Cost Updated", UpdatePrestigeCost);
        EventManager.StartListening("No Idle Gain", DisableIdleGainPanel);
        EventManager.StartListening("Idle Gain Verticies", UpdateIdleVerticies);
        EventManager.StartListening("Idle Gain Time", UpdateIdleTime);
        EventManager.StartListening("Max Circles Bought", NoMoreCircles);
        EventManager.StartListening("Prestiged", PrestigeRoutine);
        EventManager.StartListening("Prestige Process Over", EnablePrestigeButton);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Gameplay Scene Loaded", GameSceneLoadedProcedure);
        EventManager.StopListening("Verticies Updated", UpdateVerticiesCount);
        EventManager.StopListening("Circle Cost Updated", UpdateCircleCost);
        EventManager.StopListening("Prestige Cost Updated", UpdatePrestigeCost);
        EventManager.StopListening("No Idle Gain", DisableIdleGainPanel);
        EventManager.StopListening("Idle Gain Verticies", UpdateIdleVerticies);
        EventManager.StopListening("Idle Gain Time", UpdateIdleTime);
        EventManager.StopListening("Max Circles Bought", NoMoreCircles);
        EventManager.StopListening("Prestiged", PrestigeRoutine);
        EventManager.StopListening("Prestige Process Over", EnablePrestigeButton);

    }

    private void EnablePrestigeButton()
    {
        prestigeButton.interactable = true;
    }

    private void PrestigeRoutine()
    {
        prestigeButton.interactable = false;
        buyACirlceButton.interactable = true;
    }

    private void DisableIdleGainPanel()
    {
        idleGainPanel.SetActive(false);
    }

    private void NoMoreCircles()
    {
        buyACirlceCostText.text = "Max Circles Bought";
        buyACirlceButton.interactable = false;
    }

    private void UpdateIdleTime(string arg0)
    {
        idleTimeAway.text = arg0;
    }

    private void UpdateIdleVerticies(string arg0)
    {
        idleVerticiesCount.text = arg0;
    }

    void GameSceneLoadedProcedure()
    {
        Tapper theTapper = FindObjectOfType<Tapper>();
        theTapper.myPriceText = upgradeTapperCostText;
        upgradeTapperButton.onClick.AddListener(() => theTapper.Upgrade());
        RemoveTransitionPanel();
    }
    void RemoveTransitionPanel()
    {
        StartCoroutine(TransitionPanelDisappear());
    }
    IEnumerator TransitionPanelDisappear()
    {
        transitionPanelAnimator.SetTrigger("Disappear");
        yield return new WaitForSeconds(1);
        transitionPanelAnimator.gameObject.SetActive(false);
    }

    void UpdateVerticiesCount(double currentCount)
    {
        currentVerticiesCount.text = AbbrevationUtility.AbbreviateNumber(currentCount);
    }
    void UpdateCircleCost(double currentCost)
    {
        buyACirlceCostText.text = AbbrevationUtility.AbbreviateNumber(currentCost);
    }
    private void UpdatePrestigeCost(double currentCost)
    {
        prestigeCostText.text = AbbrevationUtility.AbbreviateNumber(currentCost);
    }
}
