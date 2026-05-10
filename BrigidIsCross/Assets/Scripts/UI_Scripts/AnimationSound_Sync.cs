using UnityEngine;

public class AnimationSound_Sync : MonoBehaviour
{
    private AudioClip[] clips = new AudioClip[6];
    private AudioSource source;
    void Start()
    {
        clips = EndGame_Manager.Instance.sounds; 
        source = EndGame_Manager.Instance.scoreSounds;
    }

    public void PlayFail()
    {
        source.PlayOneShot(clips[0]);
    }

    public void PlayOne()
    {
        source.PlayOneShot(clips[1]);
    }

    public void PlayTwo()
    {
        source.PlayOneShot(clips[2]);
    }

    public void PlayThree()
    {
        source.PlayOneShot(clips[3]);
    }

    public void PlayFour()
    {
        source.PlayOneShot(clips[4]);
    }

    public void PlayGold()
    {
        source.PlayOneShot(clips[5]);
    }
}
