using Unity.VisualScripting;
using UnityEngine;
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

    private void Awake()
    {
        inputActions = new BrigidInputActions();
        if (Instance == null)
        {
            Instance = this;
        }
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
        //Enable Throw Point
        if (shurikens > 0) firePoint.gameObject.SetActive(true);
    }
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        //Fire Projectile
        if (shurikens > 0) Shoot();
    }

    private void Shoot()
    {
        shurikens --;
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
    }
}