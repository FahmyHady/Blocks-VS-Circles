using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += GameplaySceneLoaded;
    }
    private void OnEnable()
    {
        EventManager.StartListening("Finished Fetching Equations", LoadGameplay);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Finished Fetching Equations", LoadGameplay);
    }
    void LoadGameplay()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
    void GameplaySceneLoaded(Scene gameplay, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(gameplay);
        EventManager.TriggerEvent("Gameplay Scene Loaded");
    }
}
