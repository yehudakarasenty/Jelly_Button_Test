using UnityEngine;

/// <summary>
/// Responsibility: View of the player
/// </summary>
public interface IPlayerView
{
    Quaternion Rotation { get; set; }
    Vector3 Position { get; set; }
    Vector3 Size { get; }
}
