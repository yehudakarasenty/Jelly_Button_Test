using UnityEngine;

public class RotateAxisY : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
}
