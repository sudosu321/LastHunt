using UnityEngine;
using UnityEngine.Rendering;

public class GunRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float recoilAngle = 10f;      // how much the gun kicks up
    public float recoilReturnSpeed = 10f; // how fast it returns
    public float recoilKickBack = 0.05f;  // optional small backward movement

    private Quaternion originalRotation;
    private Vector3 originalPosition;

    private Quaternion targetRotation;
    private Vector3 targetPosition;

    void Start()
    {
        originalRotation = transform.localRotation;
        originalPosition = transform.localPosition;
        targetRotation = originalRotation;
        targetPosition = originalPosition;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, Time.deltaTime * recoilReturnSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilReturnSpeed);
    }

    public void Recoil()
    {

        targetRotation = originalRotation * Quaternion.Euler(-recoilAngle, 0f, 0f);
        targetPosition = originalPosition + new Vector3(0f, 0f, -recoilKickBack);
        transform.localRotation = targetRotation;
        transform.localPosition = targetPosition;
    }
}
