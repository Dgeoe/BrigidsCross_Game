using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    // Create unity events for trigger actions
    public UnityEvent enteredTrigger, exitedTrigger, stayInTrigger, completedTimer; // Create unity events for trigger actions
    private float timer = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    // On Trigger enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Hit");
            enteredTrigger.Invoke();
            Debug.Log("Entered Trigger");
            audioSource.PlayOneShot(clips[0]);
            StartCoroutine(PlayPucaSound());
        }
    }

    // On Trigger stay
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stayInTrigger.Invoke();
            Debug.Log(timer);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                completedTimer.Invoke();
            }
        }
    }

    // On Trigger exit
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            exitedTrigger.Invoke();
            timer = 3;
            Debug.Log("Exited Trigger");
        }
    }

    private IEnumerator PlayPucaSound()
    {
        yield return new WaitForSeconds(0.7f);
        audioSource.pitch = 0.7f;
        audioSource.PlayOneShot(clips[1]);
    }
}