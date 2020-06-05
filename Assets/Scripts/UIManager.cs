using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Animator transitionPanelAnimator;
    public Text currentVerticiesCount;
    public Button upgradeTapperButton;
    private void OnEnable()
    {
        EventManager.StartListening("Gameplay Scene Loaded", GameSceneLoadedProcedure);
        EventManager.StartListening("Verticies Updated", UpdateVerticiesCount);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Gameplay Scene Loaded", GameSceneLoadedProcedure);
        EventManager.StopListening("Verticies Updated", UpdateVerticiesCount);

    }
    void GameSceneLoadedProcedure()
    {
        upgradeTapperButton.onClick.AddListener(() => FindObjectOfType<Tapper>().Upgrade());
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
}
