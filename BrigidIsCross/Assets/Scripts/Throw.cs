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
    [SerializeField] private Animator animator;
    
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
        if (IsTouchOverUI()) return;

        //Enable Throw Point
        if (shurikens > 0) firePoint.gameObject.SetActive(true);
        animator.SetBool("Aiming", true);

        if (shurikens == 0) EndGame_Manager.Instance.ThrowCheck();
    }

    private void OnRelease(InputAction.CallbackContext ctx)
    {
        animator.SetBool("Aiming", false);
        if (IsTouchOverUI()) return;

        //Fire Projectile
        if (shurikens > 0)
        {
            Shoot();
            EndGame_Manager.Instance.ThrowCheck();
        }
    }

    private void Shoot()
    {
        animator.SetBool("Aiming", true);
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
        animator.SetTrigger("Throw");
        animator.SetBool("Aiming", false);
    }


    private bool IsTouchOverUI()
    {
        if (EventSystem.current == null)
            return false;

        // Mobile touch
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            return EventSystem.current.IsPointerOverGameObject(
                Touchscreen.current.primaryTouch.touchId.ReadValue()
            );
        }

        // Mouse/Desktop
        return EventSystem.current.IsPointerOverGameObject();
    }
}