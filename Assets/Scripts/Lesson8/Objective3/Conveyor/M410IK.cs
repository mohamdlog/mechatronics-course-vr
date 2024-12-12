using UnityEngine;

public class ArmAndForearmController : MonoBehaviour
{
    [SerializeField] private Transform elbowPivot;
    [SerializeField] private Transform wristPivot;
    [SerializeField] private Transform forearmSegmentPivot;
    [SerializeField] private Transform armSegmentPivot;

    void LateUpdate()
    {
        // --- Forearm Segment ---
        Vector3 direction = wristPivot.position - elbowPivot.position;
        Vector3 localDirection = forearmSegmentPivot.parent.InverseTransformDirection(direction);

        float angleX = Mathf.Atan2(localDirection.y, localDirection.z) * Mathf.Rad2Deg;
        angleX -= 4.0f;

        Vector3 forearmLocalEuler = forearmSegmentPivot.localEulerAngles;
        forearmLocalEuler.x = -angleX;
        forearmSegmentPivot.localEulerAngles = forearmLocalEuler;

        // --- Arm Segment ---
        Vector3 elbowLocalEuler = elbowPivot.localEulerAngles;

        Vector3 armSegmentLocalEuler = armSegmentPivot.localEulerAngles;
        armSegmentLocalEuler.x = -elbowLocalEuler.x;
        armSegmentPivot.localEulerAngles = armSegmentLocalEuler;
    }
}
