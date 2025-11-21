using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Gavity_Beam : MonoBehaviour
{
    [Tooltip("Acceleration applied to Rigidbodies (or speed for CharacterController).")]
    [SerializeField] private float pullStrength = 12f;
    [Tooltip("Stop pulling when player is this close to beam origin.")]
    [SerializeField] private float stopDistance = 0.5f;
    [Tooltip("If true, PlayerMovement will be disabled while pulled.")]
    [SerializeField] private bool disablePlayerMovement = true;
    [Tooltip("Set to a tag (e.g. \"Player\") to only affect that tagged object. Leave empty to accept any object.")]
    [SerializeField] private string playerTag = "Player";

    private Rigidbody pulledRb;
    private CharacterController pulledCc;
    private PlayerMovement pulledMovement;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(playerTag) && !other.CompareTag(playerTag)) return;

        if (other.attachedRigidbody != null)
        {
            pulledRb = other.attachedRigidbody;
        }
        else
        {
            pulledCc = other.GetComponent<CharacterController>() ?? other.GetComponentInParent<CharacterController>();
        }

        if ((pulledRb != null || pulledCc != null) && disablePlayerMovement)
        {
            var t = (pulledRb != null) ? pulledRb.transform : pulledCc.transform;
            pulledMovement = t.GetComponent<PlayerMovement>();
            if (pulledMovement != null) pulledMovement.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Only operate on the object we recorded in OnTriggerEnter
        if (pulledRb == null && pulledCc == null) return;
        if (pulledRb != null && other.attachedRigidbody != pulledRb) return;
        if (pulledCc != null && other.GetComponentInParent<CharacterController>() != pulledCc) return;

        Vector3 origin = transform.position;
        if (pulledRb != null)
        {
            Vector3 toOrigin = origin - pulledRb.position;
            float dist = toOrigin.magnitude;
            if (dist <= stopDistance) return;
            Vector3 accel = toOrigin.normalized * pullStrength;
            pulledRb.AddForce(accel, ForceMode.Acceleration);
        }
        else if (pulledCc != null)
        {
            Vector3 toOrigin = origin - pulledCc.transform.position;
            float dist = toOrigin.magnitude;
            if (dist <= stopDistance) return;
            Vector3 move = toOrigin.normalized * pullStrength * Time.deltaTime;
            pulledCc.Move(move);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (pulledRb != null && other.attachedRigidbody == pulledRb) ClearPull();
        if (pulledCc != null && other.GetComponentInParent<CharacterController>() == pulledCc) ClearPull();
    }

    private void ClearPull()
    {
        if (pulledMovement != null) pulledMovement.enabled = true;
        pulledMovement = null;
        pulledRb = null;
        pulledCc = null;
    }

    private void OnDisable() => ClearPull();
}
