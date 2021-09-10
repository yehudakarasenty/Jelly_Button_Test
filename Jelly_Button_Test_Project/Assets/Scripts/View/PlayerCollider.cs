using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    private PlayerView mPlyerView;

    private void OnCollisionEnter(Collision collision)
    {
        mPlyerView.OnCollision(collision);
    }
}
