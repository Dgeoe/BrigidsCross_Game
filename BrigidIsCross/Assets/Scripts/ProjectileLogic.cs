using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public float speed = 20f;
    private Vector3 direction;
    private Transform localTransform;
    
    public void Awake()
    {
        localTransform = GetComponent<Transform>();
        localTransform.rotation = Quaternion.Euler(90f, 0f, 0f);
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
            Destroy();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Activate Enemy Death Func
            collision.gameObject.GetComponent<EnemyLogic>().Hit();
            //Shuriken should pass through enemies for multi-hits  
        }
        else if (collision.gameObject.CompareTag("Barrell"))
        {
            //Activate Barrell water push Func
            Destroy();   
        }
        else if (collision.gameObject.CompareTag("Bounce"))
        {
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

    }

    private void Destroy()
    {
        //add animations here
        Destroy(gameObject);
    }
}