using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTransform;
    public float verticalAngle = 60f;
    public float hortizontalAngle = 180f;
    public float distance = 20f;


    void FixedUpdate()
    {
        if (this.targetTransform == null)
        {
            return;
        }

        var vector = Quaternion.AngleAxis(this.verticalAngle, Vector3.forward) * Vector3.right;
        vector = Quaternion.AngleAxis(this.hortizontalAngle, Vector3.up) * vector;

        var result = this.targetTransform.position + vector * distance;

        this.transform.position = Vector3.Slerp(this.transform.position, result, 0.5f);
        this.transform.LookAt(this.targetTransform);
    }

    public void SetTarget(Transform transform)
    {
        this.targetTransform = transform;
    }
}
