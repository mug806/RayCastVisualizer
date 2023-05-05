using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class RayCastVisualizer : MonoBehaviour {
    [HideInInspector]
    public bool autoCast = false;
    public enum RaycastMode { Ray, Box };
    public RaycastMode raycastMode = RaycastMode.Ray;
    public float maxDistance;
    public LayerMask layerMask;
    public QueryTriggerInteraction queryTriggerInteraction;
    [HideInInspector]
    public Vector3 halfExtents;

    RaycastHit hit;
    bool valid = false;
    Matrix4x4 gizmoMatrix;

    private void OnValidate() {
        valid = false;
    }

#if UNITY_EDITOR
    private void Update() {
        if (autoCast) {
            RayCast();
        } 
    }
#endif

    public bool RayCast() {
        gizmoMatrix = transform.localToWorldMatrix;
        if (raycastMode == RaycastMode.Ray) {
            valid = true;
            return Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask, queryTriggerInteraction);
        } else if (raycastMode == RaycastMode.Box) {
            valid = true;
            return Physics.BoxCast(transform.position, halfExtents, transform.forward, out hit, transform.rotation, maxDistance, layerMask, queryTriggerInteraction);
        }
        return false;
    }

    private void OnDrawGizmos() {
        if (raycastMode == RaycastMode.Box) {
            Gizmos.color = Color.white;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2);
        }

        if (valid) {
            Gizmos.matrix = gizmoMatrix;
            if (hit.collider == null) {
                Gizmos.color = Color.white;
                Gizmos.DrawRay(Vector3.zero, Vector3.forward * maxDistance);
            } else {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(Vector3.zero, Vector3.forward * hit.distance);
            }
            if (raycastMode == RaycastMode.Box) {
                
                if (hit.collider == null) {
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(Vector3.forward * maxDistance, halfExtents * 2);
                } else {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(Vector3.forward * hit.distance, halfExtents * 2);
                }
            }
        }
    }

    public RaycastHit GetHitInfo() {
        return hit;
    }

    public bool IsValid() {
        return valid;
    }
}
