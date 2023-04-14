using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Properties that can be accessed and set from other classes
    public float HandleRange { get; set; }
    public float DeadZone { get; set; }

    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;

    // Serialized fields that can be set in the Unity Inspector
    [SerializeField] private float handleRange = 1f;
    [SerializeField] private float deadZone = 0f;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;
    private RectTransform baseRect = null;

    private Camera cam;
    private Vector2 input = Vector2.zero;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Set the properties to their serialized field values
        HandleRange = handleRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();

        MoveThreshold = moveThreshold;
        // Set the pivot and anchored position of the background and handle RectTransforms
        background.gameObject.SetActive(false);
        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    // Called when a pointer is pressed down on the object this script is attached to
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        // Set the position of the background RectTransform to the position of the pointer
        background.position = eventData.position;
        background.gameObject.SetActive(true);
        // Call the OnDrag method to update the handle position
        OnDrag(eventData);
    }

    // Called when a pointer is dragged while over the object this script is attached to
    public void OnDrag(PointerEventData eventData)
    {
        // Get the camera that captured the press event
        cam = eventData.pressEventCamera;

        // Convert the position of the background RectTransform to screen space
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        // Calculate the radius of the background RectTransform
        Vector2 radius = background.sizeDelta / 2;
        // Calculate the input vector based on the position of the pointer and the background radius
        input = (eventData.position - position) / (radius * transform.lossyScale.x);
        // Handle the input vector
        HandleInput(input.magnitude, input.normalized, radius, cam);
        // Set the anchored position of the handle RectTransform based on the input vector and handle range
        handle.anchoredPosition = input * radius * handleRange;
    }

    // Handle the input vector based on its magnitude, direction, radius, and camera
    protected virtual void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera cam)
    {
        if (magnitude > DeadZone)
        {
            // If the magnitude is greater than 1, set the input vector to its normalized value
            if (magnitude > 1)
            {                
                Vector2 difference = normalized * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;

            }
                // Invoke the event with the input direction
                EventsManager.InvokeOnMove(input);
        }
        else
            // Otherwise, set the input vector to zero
            input = Vector2.zero;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(false);

        EventsManager.InvokeEndMove();
    }
}
