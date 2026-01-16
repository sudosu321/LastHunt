using UnityEngine;

public class WeaponClipping : MonoBehaviour
{
    public Transform cameraTransform;
    public float checkDistance = 0.6f;
    public float pushBackDistance = 0.25f;
    public float smoothSpeed = 10f;
    public LayerMask wallMask;

    private Vector3 originalLocalPos;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        bool hitWall = Physics.Raycast(
            ray,
            out RaycastHit hit,
            checkDistance,
            wallMask
        );

        Vector3 targetPos = originalLocalPos;

        if (hitWall)
        {
            float t = 1f - (hit.distance / checkDistance);
            targetPos = originalLocalPos - Vector3.forward * pushBackDistance * t;
        }

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPos,
            Time.deltaTime * smoothSpeed
        );

        Debug.DrawRay(ray.origin, ray.direction * checkDistance, hitWall ? Color.red : Color.green);
    }
}
