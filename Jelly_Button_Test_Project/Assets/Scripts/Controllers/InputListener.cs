using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsibility: Manage user Input and notify
/// </summary>
public class InputListener : MonoBehaviour, IInputListener
{
    #region Members
    private FloatUnityEvent OnHorizontalChange = new FloatUnityEvent();

    public bool AnyKey { get => Input.anyKey; }
    #endregion

    #region Functions
    private void Awake()
    {
        SingleManager.Register<IInputListener>(this);
    }

    void Update()
    {
        ListenHorizontal();
    }

    private void ListenHorizontal()
    {
        if (OnHorizontalChange != null)
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal != 0)
                OnHorizontalChange.Invoke(horizontal);
        }
    }

    public void RegisterToHorizontalInput(UnityAction<float> action) => OnHorizontalChange.AddListener(action);

    public void RemoveFromHorizontalInput(UnityAction<float> action) => OnHorizontalChange.RemoveListener(action);

    public bool IsKeyIsDown(KeyCode keyCode) => Input.GetKey(keyCode);

    private void OnDestroy()
    {
        SingleManager.Remove<IInputListener>();
    }
    #endregion
}

public class FloatUnityEvent : UnityEvent<float> { }
