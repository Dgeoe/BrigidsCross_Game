using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Throw : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] public int shurikens;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private Animator animator;
    public static Throw Instance;

    private bool wasPressing;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        bool isPressing = false;

        // Mobile
        if (Touchscreen.current != null)
        {
            isPressing =
                Touchscreen.current.primaryTouch.press.isPressed;
        }
        // Desktop
        else if (Mouse.current != null)
        {
            isPressing = Mouse.current.leftButton.isPressed;
        }

        // PRESS START
        if (isPressing && !wasPressing)
        {
            BeginAim();
        }

        // RELEASE
        if (!isPressing && wasPressing)
        {
            ReleaseThrow();
        }

        wasPressing = isPressing;
    }

    private void BeginAim()
    {
        if (IsTouchOverUI()) return;

        if (shurikens > 0)
        {
            firePoint.gameObject.SetActive(true);
        }

        animator.SetBool("Aiming", true);

        if (shurikens == 0)
        {
            EndGame_Manager.Instance.ThrowCheck();
        }
    }

    private void ReleaseThrow()
    {
        animator.SetBool("Aiming", false);

        if (IsTouchOverUI()) return;

        if (shurikens > 0)
        {
            Shoot();
            EndGame_Manager.Instance.ThrowCheck();
        }
    }

    private void Shoot()
    {
        shurikens--;

        Destroy(ProjectileCount.Instance.crosses[shurikens]);

        firePoint.gameObject.SetActive(false);

        RaycastHit hit;

        if (Physics.Raycast(
            firePoint.position,
            firePoint.forward,
            out hit,
            Mathf.Infinity))
        {
            Vector3 direction =
                (hit.point - firePoint.position).normalized;

            GameObject projectile = Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.identity);

            projectile.GetComponent<ProjectileLogic>()
                .Initialize(direction);
        }

        audioSource.volume -= 0.3f;

        if (audioSource.volume <= 0)
            audioSource.volume = 0.3f;

        audioSource.PlayOneShot(throwSound);

        animator.SetTrigger("Throw");
    }

    private bool IsTouchOverUI()
    {
        if (EventSystem.current == null)
            return false;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            return EventSystem.current.IsPointerOverGameObject(
                Touchscreen.current.primaryTouch.touchId.ReadValue()
            );
        }

        return EventSystem.current.IsPointerOverGameObject();
    }
}