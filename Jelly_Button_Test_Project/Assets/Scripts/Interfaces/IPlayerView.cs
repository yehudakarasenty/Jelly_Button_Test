using UnityEngine;

public interface IPlayerView
{
    Quaternion Rotation { get; set; }
    Vector3 Position { get; set; }
}
