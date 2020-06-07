using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVerticiesTestButton : MonoBehaviour
{
    public void AddVerticies(float countToAdd)
    {
        PlayerDataManager.AddVerticies(countToAdd);
    }
}
