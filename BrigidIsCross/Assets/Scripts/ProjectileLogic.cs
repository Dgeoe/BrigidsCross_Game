using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    private Vector3 direction;
    private Transform transform;
    
    public void Awake()
    {
        transform = GetComponent<Transform>();
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
    public void Initialize(Vector3 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
