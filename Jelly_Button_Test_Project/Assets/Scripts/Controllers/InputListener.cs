using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour, IInputListener
{
    private FloatUnityEvent OnHorizontalChange = new FloatUnityEvent();

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

    public void RegisterToHorizontalInput(UnityAction<float> action)
    {
        OnHorizontalChange.AddListener(action);
    }

    public void RemoveFromHorizontalInput(UnityAction<float> action)
    {
        OnHorizontalChange.RemoveListener(action);
    }

    private void OnDestroy()
    {
        SingleManager.Remove<IInputListener>();
    }
}

public class FloatUnityEvent : UnityEvent<float> { }
