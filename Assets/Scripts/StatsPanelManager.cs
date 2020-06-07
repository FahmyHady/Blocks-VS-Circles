using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsPanelManager : MonoBehaviour
{
    Upgradable[] upgradables;
    [SerializeField] Text tapperText;
    [SerializeField] Text prestigeText;
    [SerializeField] GameObject[] circleItem;
    [SerializeField] Text[] circlesTexts;
    private void OnEnable()
    {
        EventManager.StartListening("Gameplay Scene Loaded", GetUpgradables);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Gameplay Scene Loaded", GetUpgradables);
    }
    public void IntialiseState()
    {
        prestigeText.text = Mathf.Pow(10, PlayerDataManager.prestigeLevel).ToString() + "x";
        int j = 0;
        for (int i = 0; i < upgradables.Length; i++)
        {
            if (upgradables[i] is Tapper)
            {
                tapperText.text = AbbrevationUtility.AbbreviateNumber(upgradables[i].goldPerTap);
            }
            else
            {
                if (upgradables[i].gameObject.activeSelf)
                {
                    circleItem[j].SetActive(true);
                    circlesTexts[j].text = AbbrevationUtility.AbbreviateNumber(upgradables[i].goldPerTap);
                }
                else
                {
                    circleItem[j].SetActive(false);
                }
                j++;
            }
        }
    }
    void GetUpgradables()
    {
        upgradables = FindObjectsOfType<Upgradable>();
    }
}
