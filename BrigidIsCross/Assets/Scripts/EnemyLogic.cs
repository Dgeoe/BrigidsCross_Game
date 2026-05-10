using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool HitOnce = true;

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
        if (HitOnce) EndGame_Manager.Instance.Kill();
        HitOnce = false;
    }

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool("Delay", true);
    }
}
