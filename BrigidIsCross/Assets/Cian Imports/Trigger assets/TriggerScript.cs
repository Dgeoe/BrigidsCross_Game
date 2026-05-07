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
    // On Trigger enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enteredTrigger.Invoke();
            Debug.Log("Entered Trigger");
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
}