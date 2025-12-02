using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LazerBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private float LazerDistance = 8f;

    [SerializeField] private LayerMask ignoreMask;

    [SerializeField] private UnityEvent onHitEvent;

    private RaycastHit rayHit;
    private Ray ray;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out rayHit, LazerDistance, ~ignoreMask))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer .SetPosition(1, rayHit.point);
            if (rayHit.collider.TryGetComponent(out Target target))
            {
                target.Hit();
                onHitEvent?.Invoke();
            }
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * LazerDistance);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * LazerDistance);
    }
} 