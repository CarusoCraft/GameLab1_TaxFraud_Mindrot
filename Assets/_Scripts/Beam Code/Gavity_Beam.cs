using System.Collections.Generic;
using UnityEngine;

public class Gavity_Beam : MonoBehaviour
{
    [Header("Beam Settings")]
    public string targetTag = "Player";           // Tag used to identify the character
    public float pullStrength = 40f;             // Base pull strength (acceleration)
    public float maxPullDistance = 12f;          // Distance at which pull falls to zero
    public float stopDistance = 1f;              // When closer than this, stop pulling
    public bool active = false;                  // Toggle the beam on/off

    [Header("Optional Visuals")]
    public LineRenderer lineRenderer;            // Optional: draw a line from origin to target
    public Transform beamOrigin;                 // point to pull toward (defaults to this.transform)

    // Internals
    readonly HashSet<Rigidbody> targets = new HashSet<Rigidbody>();

    void Reset()
    {
        beamOrigin = transform;
    }

    void Awake()
    {
        if (beamOrigin == null) beamOrigin = transform;
        if (lineRenderer != null) lineRenderer.positionCount = 2;
    }

    void FixedUpdate()
    {
        if (!active) return;

        if (targets.Count == 0)
        {
            if (lineRenderer != null) lineRenderer.enabled = false;
            return;
        }

        foreach (var rb in targets)
        {
            if (rb == null) continue;

            Vector3 dir = beamOrigin.position - rb.position;
            float distance = dir.magnitude;

            if (distance <= stopDistance)
            {
                // Close enough � remove velocity component toward the beam origin
                Vector3 toTargetDir = dir.normalized;
                float approachSpeed = Vector3.Dot(rb.linearVelocity, toTargetDir);
                if (approachSpeed > 0f)
                {
                    rb.linearVelocity -= toTargetDir * approachSpeed;
                }

                continue;
            }

            if (distance > maxPullDistance) continue;

            float falloff = 1f - Mathf.Clamp01(distance / maxPullDistance); // 0..1
            float force = pullStrength * falloff;

            // Apply as acceleration so heavier objects still move predictably
            rb.AddForce(dir.normalized * force, ForceMode.Acceleration);

            // Optional: draw line to this target
            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, beamOrigin.position);
                lineRenderer.SetPosition(1, rb.position);
            }
        }
    }

    // Use a trigger collider (SphereCollider recommended) to detect when the player enters the beam area.
    // Make sure the collider is set as "Is Trigger" and its radius >= maxPullDistance.
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;
        var rb = other.attachedRigidbody;
        if (rb != null) targets.Add(rb);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;
        var rb = other.attachedRigidbody;
        if (rb != null) targets.Remove(rb);
    }

    public void ActivateBeam() => active = true;
    public void DeactivateBeam() => active = false;

    void OnDisable()
    {
        targets.Clear();
        if (lineRenderer != null) lineRenderer.enabled = false;
    }
}
