using UnityEngine;

public class SmoothFollow : MonoBehaviour, ISmoothFollow
{
    private ISmoothFollowController mController;

    [SerializeField]
    private float distance = 6f;
    [SerializeField]
    private float height = 1f;
    [SerializeField]
    private float heightDamping = 2f;
    [SerializeField]
    private float rotationDamping = 3f;
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        mController = SingleManager.Get<ISmoothFollowController>();
        mController.SetView(this);
    }

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
    }

    public void SetDistance(float distance)
    {
        this.distance = distance;
    }

    public void SetHeight(float height)
    {
        this.height = height;
    }
}