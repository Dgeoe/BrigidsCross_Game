using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Throw : MonoBehaviour
{
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public int shurikens;
    public static Throw Instance;

    private BrigidInputActions inputActions;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip throwSound;

    
    private void Awake()
    {
        inputActions = new BrigidInputActions();
        if (Instance == null)
        {
            Instance = this;
        }

        if (audioSource == null) gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        inputActions.PlayerBase.Touch.Enable();
        inputActions.PlayerBase.Touch.started += OnPress;
        inputActions.PlayerBase.Touch.canceled += OnRelease;
    }

    private void OnDisable()
    {
        inputActions.PlayerBase.Touch.started -= OnPress;
        inputActions.PlayerBase.Touch.canceled -= OnRelease;
        inputActions.PlayerBase.Touch.Disable();
    }

    private void OnPress(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        //if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Touchscreen.current.primaryTouch.touchId.ReadValue())) return;

        //Enable Throw Point
        if (shurikens > 0) firePoint.gameObject.SetActive(true);
    }

    private void OnRelease(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        //if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Touchscreen.current.primaryTouch.touchId.ReadValue())) return;

        //Fire Projectile
        if (shurikens > 0) Shoot();
    }

    private void Shoot()
    {
        shurikens --;
        Destroy(ProjectileCount.Instance.crosses[shurikens]);
        firePoint.gameObject.SetActive(false);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            targetPoint = hit.point;
        }
        else
        {
            return;
        }

        Vector3 direction = (targetPoint - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<ProjectileLogic>().Initialize(direction);

        //PLAY THROW SOUND (protein tubes *beep* with that white sauce *beep*)
        audioSource.volume = audioSource.volume - 0.3f;
        if (audioSource.volume <= 0) audioSource.volume = 0.3f;
        audioSource.PlayOneShot(throwSound);
    }
}