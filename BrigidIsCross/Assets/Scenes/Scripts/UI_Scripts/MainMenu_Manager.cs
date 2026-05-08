using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject backing1, backing2;
    [SerializeField] private AudioClip HarpSfx;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(HarpSfx);
        if (backing1 != null) backing1.SetActive(false);
        if (backing2 != null) backing2.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (backing2 != null) backing2.SetActive(false);
        if (backing1 != null) backing1.SetActive(true);
    }
}
