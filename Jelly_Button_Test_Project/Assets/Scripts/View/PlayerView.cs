using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    private IPlayerController mController;

    [SerializeField]
    private MeshRenderer mModel;

    public Quaternion Rotation { get => mModel.transform.rotation; set => mModel.transform.rotation = value; }
    public Vector3 Position { get => transform.position; set => transform.position = value; }

    public Vector3 Size { get => mModel.bounds.size; }

    private void Awake()
    {
        mController = SingleManager.Get<IPlayerController>();
        mController.SetView(this);
    }

    public void OnCollision(Collision other)
    {
        Debug.LogError("GAME OVER!!");
    }
}
