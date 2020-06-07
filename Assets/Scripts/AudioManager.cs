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
    static bool muteAudio;
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
    public void MuteAudio()
    {
        muteAudio = true;
    }
    public void UnMuteAudio()
    {
        muteAudio = false;
    }
    static public void PlaySound(string whatToPlay)
    {
        if (!muteAudio)
        {
        AudioSource.PlayClipAtPoint(soundsDict[whatToPlay], Vector3.zero);
        }
    }
}
