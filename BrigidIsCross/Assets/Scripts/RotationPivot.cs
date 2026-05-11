using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RotationPivot : MonoBehaviour
{
    // Player rotates their aim based off their mouse or touch screen location
    // As long as your mouse is over an object on the layer "Ground" the player will rotate towards it

    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask groundLayer;

    private void Start()
    {
        StartCoroutine(DelayInputAccess());
    }

    void Update()
    {
        Vector2 screenPosition;

        if (Application.isMobilePlatform)
        {
            if (Touchscreen.current == null || !Touchscreen.current.primaryTouch.press.isPressed)
            {
                return;
            }

            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        else
        {
            if (Mouse.current == null) return;

            screenPosition = Mouse.current.position.ReadValue();
        }

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 direction = hit.point - transform.position;

            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }
        }
    }

    private IEnumerator DelayInputAccess()
    {
        yield return new WaitForSeconds(2f);
        Throw.Instance.waitTime = true;
    }
}