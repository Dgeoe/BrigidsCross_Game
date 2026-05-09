using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    //Call in Projectile Logic Script
    public void Hit()
    {
        //add animation and sfx here
        Destroy(gameObject);
    }
}
