using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Animator transitionPanelAnimator;
    public Text currentVerticiesCount;
    public Button upgradeTapperButton;
    public Text upgradeTapperCostText;
    public Text buyACirlceCostText;
    public Text idleVerticiesCount;
    public Text idleTimeAway;
    private void OnEnable()
    {
        EventManager.StartListening("Gameplay Scene Loaded", GameSceneLoadedProcedure);
        EventManager.StartListening("Verticies Updated", UpdateVerticiesCount);
        EventManager.StartListening("Circle Cost Updated", UpdateCircleCost);
        EventManager.StartListening("Idle Gain Verticies", UpdateIdleVerticies);
        EventManager.StartListening("Idle Gain Time", UpdateIdleTime);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Gameplay Scene Loaded", GameSceneLoadedProcedure);
        EventManager.StopListening("Verticies Updated", UpdateVerticiesCount);
        EventManager.StopListening("Circle Cost Updated", UpdateCircleCost);
        EventManager.StopListening("Idle Gain Verticies", UpdateIdleVerticies);
        EventManager.StopListening("Idle Gain Time", UpdateIdleTime);
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
}
