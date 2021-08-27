using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Options")]
    [Range(0f, 2f)] public float handleDistance = 1f;
    [Range(0f, 1f)] public float deadZone = 0f;
    public JoystickAxis joystickAxis = JoystickAxis.All;

    protected Vector2 inputVector = Vector2.zero;

    [Header("Components")]
    public RectTransform background;
    public RectTransform handle;

    public bool IsActive { get; set; }
    public float Horizontal { get { return inputVector.x; } }
    public float Vertical { get { return inputVector.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    private void OnEnable()
    {
        IsActive = true;
    }

    private void OnDisable()
    {
        inputVector = Vector2.zero; 
        handle.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }

    public void ResetHandle()
    {
        
    }

    protected void ClampJoystick()
    {
        if (joystickAxis == JoystickAxis.Horizontal)
            inputVector = new Vector2(inputVector.x, 0f);
        if (joystickAxis == JoystickAxis.Vertical)
            inputVector = new Vector2(0f, inputVector.y);
    }
}

public enum JoystickAxis { All, Horizontal, Vertical}
