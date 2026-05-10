using UnityEngine;
using System.Collections;

public class ProjectileLogic : MonoBehaviour
{
    public float speed = 20f;
    private Vector3 direction;
    private bool inOne = false;
    private Transform localTransform;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds; //0 = enemy, everything after is a randomized bounce sound so we can add more later if we need

    public void Awake()
    {
        localTransform = GetComponent<Transform>();
        localTransform.rotation = Quaternion.Euler(90f, 0f, 0f);
        audioSource = GetComponent<AudioSource>();
    }
    public void Initialize(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void FixedUpdate()
    {
        localTransform.position += direction * speed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayParticle(collision);
            RandomSound();
            Destroy();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Activate Enemy Death Func
            audioSource.PlayOneShot(sounds[0]);
            collision.gameObject.GetComponent<EnemyLogic>().Hit();
            //Shuriken should pass through enemies for multi-hits  
        }
        else if (collision.gameObject.CompareTag("Barrell"))
        {
            PlayParticle(collision);
            RandomSound();
            //Activate Barrell water push Func
            Destroy();   
        }
        else if (collision.gameObject.CompareTag("Bounce"))
        {
            PlayParticle(collision);
            RandomSound();
            // Get the normal of the surface hit
            Vector3 normal = collision.contacts[0].normal;
            direction = Vector3.Reflect(direction, normal).normalized;
        }  
        else if (collision.gameObject.CompareTag("Bend"))
        {
            //set many paths
            //set new path  
        }
        else
        {
            Destroy();
        }

        //if (inOne == false) EndGame_Manager.Instance.ThrowCheck();
        //inOne = true;

    }

    private void Destroy()
    {
        //add animations here
        Destroy(gameObject);
    }

    private void PlayParticle(Collision collision)
    {
        //For bounce debris
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPoint = contact.point;
        Quaternion rotation = Quaternion.LookRotation(contact.normal);

        GameObject x = Instantiate(particleSystem, hitPoint, rotation);
        StartCoroutine(DestroyDebris(x));
    }

    private void RandomSound()
    {
        int index = Random.Range(1, sounds.Length);

        audioSource.PlayOneShot(sounds[index]);
    }

    private IEnumerator DestroyDebris(GameObject g)
    {
        yield return new WaitForSeconds(1f);
        {
            Destroy(g);
        }
    }
}