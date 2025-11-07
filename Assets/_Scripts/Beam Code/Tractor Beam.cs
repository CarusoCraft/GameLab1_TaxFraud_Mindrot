using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TractorBeam : MonoBehaviour
{
    [Tooltip("How strong the pull is (acceleration).")]
    [SerializeField] private float pullStrength = 20f;

    [Tooltip("Maximum distance where pull is applied. Larger = weaker near edge.")]
    [SerializeField] private float maxDistance = 10f;

    [Tooltip("If true, the target's gravity will be disabled while being pulled.")]
    [SerializeField] private bool disableGravityWhilePulling = true;

    [Tooltip("Tag used to identify the player Rigidbody.")]
    [SerializeField] private string targetTag = "Player";

    private Rigidbody currentTargetRb;

    void Reset()
    {
        // ensure collider is trigger so OnTrigger events fire
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    // Called when a Rigidbody enters the beam trigger
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        if (other.attachedRigidbody != null)
        {
            currentTargetRb = other.attachedRigidbody;
            if (disableGravityWhilePulling) currentTargetRb.useGravity = false;
        }
    }

    // While inside the beam, apply pulling force (physics-friendly)
    void OnTriggerStay(Collider other)
    {
        if (currentTargetRb == null) return;
        if (other.attachedRigidbody != currentTargetRb) return;

        Vector3 toBeam = transform.position - currentTargetRb.position;
        float distance = toBeam.magnitude;
        if (distance < 0.01f) return;

        // strength falloff so object is pulled smoothly
        float t = Mathf.Clamp01(1f - (distance / maxDistance));
        Vector3 force = toBeam.normalized * pullStrength * t;

        // Use acceleration so mass is accounted for naturally
        currentTargetRb.AddForce(force, ForceMode.Acceleration);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == currentTargetRb)
        {
            if (disableGravityWhilePulling) currentTargetRb.useGravity = true;
            currentTargetRb = null;
        }
    }
}
