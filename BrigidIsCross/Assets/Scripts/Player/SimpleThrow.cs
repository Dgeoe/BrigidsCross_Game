using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleThrow : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject projectilePrefab;

    [Header("Settings")]
    public float throwForce = 20f;
    public float throwUpwardForce = 2f;
    public float throwCooldown = 0.5f;

    private bool readyToThrow = true;

    private PlayerInput playerInput;
    private InputAction shootAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];

        shootAction.performed += ctx => Throw();
    }

    private void Throw()
    {
        if (!readyToThrow) return;
        readyToThrow = false;

        // Spawn projectile
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, cam.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Aim toward crosshair (center of screen)
        Vector3 direction = cam.forward;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f)) direction = (hit.point - attackPoint.position).normalized;

        // Apply force
        Vector3 force = direction * throwForce + transform.up * throwUpwardForce;
        rb.AddForce(force, ForceMode.Impulse);

        Destroy(projectile, 3f);
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    private void OnEnable()
    {
        shootAction.performed += OnShoot;
    }

    private void OnDisable()
    {
        shootAction.performed -= OnShoot;
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        Throw();
    }
}