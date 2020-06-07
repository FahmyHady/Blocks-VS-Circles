using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string audioName;
    public AudioClip audio;
}
public class AudioManager : MonoBehaviour
{
    static Dictionary<string, AudioClip> soundsDict = new Dictionary<string, AudioClip>();
    public Sounds[] mySounds;

    private void Start()
    {
        if (soundsDict.Count == 0)
        {
            for (int i = 0; i < mySounds.Length; i++)
            {
                soundsDict.Add(mySounds[i].audioName, mySounds[i].audio);
            }
        }

    }
    static public void PlaySound(string whatToPlay, Vector3 whereToPlay)
    {
        AudioSource.PlayClipAtPoint(soundsDict[whatToPlay], whereToPlay);
    }
}
