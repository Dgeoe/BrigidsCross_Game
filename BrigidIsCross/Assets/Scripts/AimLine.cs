using UnityEngine;

public class AimLine : MonoBehaviour
{
    //Summary:
    //Draw LineRenderer from hit points to showcase aim line (kinda like in Angry Birds)
    //Colors change if you hit enemies
    //Line follows ricochet path 
    //Update Heavy: Enabled from rotation pivot script only when neccessary
    private LineRenderer lineRenderer;

    [SerializeField] private Transform ThrowPoint;

    [SerializeField] private int maxBounces = 10;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = maxBounces;
    }

    private void Update()
    {
        Vector3 currentPosition = ThrowPoint.position;
        Vector3 currentDirection = ThrowPoint.forward;

        lineRenderer.SetPosition(0, currentPosition);

        for (int i = 1; i < maxBounces; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(currentPosition, currentDirection, out hit, Mathf.Infinity))
            {
                lineRenderer.SetPosition(i, hit.point);

                //Color Set
                if (hit.transform.CompareTag("Enemy")) SetColor(Color.red);
                else if (hit.transform.CompareTag("Barrell")) SetColor(Color.blue);
                else SetColor(Color.white);

                //Stop Here if no bounce hit
                if (!hit.transform.CompareTag("Bounce"))
                {
                    FillRemaining(i, hit.point);
                    break;
                }

                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                currentPosition = hit.point + hit.normal * 0.05f;
            }
            else
            {
                Vector3 endPoint = currentPosition + currentDirection * 10f;
                lineRenderer.SetPosition(i, endPoint);

                FillRemaining(i, endPoint);
                break;
            }
        }
    }

    private void FillRemaining(int startIndex, Vector3 point)
    {
        //Set additional index positions to = last bounce hit
        for (int i = startIndex + 1; i < maxBounces; i++)
        {
            lineRenderer.SetPosition(i, point);
        }
    }

    private void SetColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}