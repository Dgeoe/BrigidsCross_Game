using UnityEditor.ShaderGraph;
using UnityEngine;

public class AimLine : MonoBehaviour
{
   private LineRenderer lineRenderer;

   [SerializeField] private Transform ThrowPoint; 
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, ThrowPoint.position);
        RaycastHit hit;

        //if Hit Object set object position as line renderer position
        //if not set line renderer to 100 (max distance) 
        if (Physics.Raycast(ThrowPoint.position, ThrowPoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            lineRenderer.SetPosition(1, hit.point);
            if (hit.transform.gameObject.CompareTag("Enemy") == true)
            {
                //set to red
            }
            if (hit.transform.gameObject.CompareTag("Bounce"))
            {
                //Create a richochet path with 3rd position 
            }
            if (hit.transform.gameObject.CompareTag("Barrell"))
            {
                //set to blue 
            }
        }
        else
        {
            return;
        }
    }
}