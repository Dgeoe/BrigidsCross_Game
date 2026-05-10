using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationPivot : MonoBehaviour
{
    // Player rotates their aim based off their mouse or touch screen location
    // As long as your mouse is over an object on the layer "Ground" the player will rotate towards it

    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask groundLayer;

    void Update()
    {
        {
            Vector2 screenPosition;

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            {
                screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            }
            else if (Mouse.current != null)
            {
                screenPosition = Mouse.current.position.ReadValue();
            }
            else
            {
                Debug.Log("Touch fail");
                return;
            }

            Ray ray = mainCamera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
            {
                Vector3 targetPosition = hit.point;
                Vector3 direction = targetPosition - transform.position;
                direction.y = 0f;

                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}