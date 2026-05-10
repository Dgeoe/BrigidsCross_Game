using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        //add delay to the idle so kick is not triggered at same time by all enemies
        float index = Random.Range(0.1f, 2f);
        StartCoroutine(Delay(index));
    }

    //Call in Projectile Logic Script
    public void Hit()
    {
        //add animation and sfx here
        animator.SetTrigger("Hit");
    }

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool("Delay", true);
    }
}
