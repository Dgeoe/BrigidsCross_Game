using UnityEngine;

public class IsMobile : MonoBehaviour
{
    [SerializeField] private GameObject mobileText;

    private void Start()
    {
        bool isMobile = Application.isMobilePlatform;
        if (isMobile) mobileText.SetActive(true);
    }
}
