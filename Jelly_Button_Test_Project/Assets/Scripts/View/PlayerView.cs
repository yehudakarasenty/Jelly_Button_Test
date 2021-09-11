using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    #region Members
    private IPlayerController mController;

    [SerializeField]
    private MeshRenderer mModel;

    public Quaternion Rotation { get => mModel.transform.rotation; set => mModel.transform.rotation = value; }
    
    public Vector3 Position { get => transform.position; set => transform.position = value; }
    
    public Vector3 Size { get => mModel.bounds.size; }
    #endregion

    #region Functions
    private void Awake()
    {
        mController = SingleManager.Get<IPlayerController>();
        mController.SetView(this);
    }

    public void OnCollision()=> mController.PlyerCollided();
    #endregion
}
