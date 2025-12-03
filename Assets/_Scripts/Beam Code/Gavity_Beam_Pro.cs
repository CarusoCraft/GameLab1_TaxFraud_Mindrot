using System.Collections;
using UnityEngine;

public class Gavity_Beam_Pro : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float LazerDistance = 8f;
    [SerializeField] private LayerMask ignoreMask;

    [Header("Pull")]
    [SerializeField] private float pullSpeed = 5f;
    [SerializeField] private float stopDistance = 0.25f;

    private RaycastHit rayHit;
    private Ray ray;
    private Coroutine pullCoroutine;
    private Transform currentPulled;
    private float pulledInitialY;

    private void Start()
    {
        if (lineRenderer != null) lineRenderer.positionCount = 2;
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out rayHit, LazerDistance, ~ignoreMask))
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, rayHit.point);
            }

            if (rayHit.collider.TryGetComponent(out Target _))
            {
                StartPull(rayHit.collider.transform);
            }
            else
            {
                StopPull();
            }
        }
        else
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * LazerDistance);
            }

            StopPull();
        }
    }

    private void StartPull(Transform target)
    {
        if (target == null) return;
        if (currentPulled == target) return;

        StopPull();
        currentPulled = target;
        pulledInitialY = target.position.y; // lock Y to this value while pulling
        pullCoroutine = StartCoroutine(PullCoroutine(target));
    }

    private void StopPull()
    {
        if (pullCoroutine != null)
        {
            StopCoroutine(pullCoroutine);
            pullCoroutine = null;
        }
        currentPulled = null;
        pulledInitialY = 0f;
    }

    private IEnumerator PullCoroutine(Transform target)
    {
        var rb = target.GetComponent<Rigidbody>();
        bool madeKinematic = false;
        if (rb != null && !rb.isKinematic)
        {
            rb.isKinematic = true;
            madeKinematic = true;
        }

        while (target != null)
        {
            // compute horizontal (XZ) distance only to respect locked Y
            Vector3 targetXZ = new Vector3(target.position.x, 0f, target.position.z);
            Vector3 originXZ = new Vector3(transform.position.x, 0f, transform.position.z);
            float dist = Vector3.Distance(targetXZ, originXZ);
            if (dist <= stopDistance) break;

            Vector3 newXZ = Vector3.MoveTowards(targetXZ, originXZ, pullSpeed * Time.deltaTime);
            target.position = new Vector3(newXZ.x, pulledInitialY, newXZ.z);

            yield return null;
        }

        if (madeKinematic && rb != null)
            rb.isKinematic = false;

        StopPull();
    }

    private void OnDisable()
    {
        StopPull();
        if (lineRenderer != null) lineRenderer.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * LazerDistance);
    }
}