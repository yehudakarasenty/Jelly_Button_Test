using UnityEngine;

/// <summary>
/// Responsibility: Camera smooth follow target
/// </summary>
public class SmoothFollow : MonoBehaviour, ISmoothFollow
{
    #region Members
    private ISmoothFollowController mController;

    [SerializeField]
    private float currentDistance = 6f;

    [SerializeField]
    private float height = 1f;

    [SerializeField]
    private float heightDamping = 2f;

    [SerializeField]
    private float rotationDamping = 3f;

    [SerializeField]
    private float distanceDamping = 3f;

    private float wantedDistance = 6;

    [SerializeField]
    private Transform target;
    #endregion

    #region
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
        if(currentDistance!= wantedDistance)
            currentDistance = Mathf.Lerp(currentDistance, wantedDistance, distanceDamping * Time.deltaTime);
        pos = target.position - currentRotation * Vector3.forward * currentDistance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
    }

    public void SetDistance(float distance)=> wantedDistance = distance;

    public void SetHeight(float height)=> this.height = height;
    #endregion
}