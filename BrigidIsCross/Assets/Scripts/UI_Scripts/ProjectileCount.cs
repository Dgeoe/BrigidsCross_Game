using UnityEngine;
using UnityEngine.UI;

public class ProjectileCount : MonoBehaviour
{
    //on start display amount of shurikens + animation
    //On Button Press display again 
    private GameObject[] crosses;
    private void Start()
    {
        //Starting Message
    }

    private void SpawnUI()
    {
        for(int i = 0; i < Throw.Instance.shurikens; i++)
        {
            //Instantiate in New UI 

        }
    }
    private void AnimationBuffer()
    {

    }
}
