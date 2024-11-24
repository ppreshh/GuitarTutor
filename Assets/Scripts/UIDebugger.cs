using UnityEngine;
using UnityEngine.EventSystems;

public class UIDebugger : MonoBehaviour
{
    // Event System to manage UI interaction
    private PointerEventData pointerData;
    private EventSystem eventSystem;

    void Start()
    {
        // Get the EventSystem component
        eventSystem = EventSystem.current;
        pointerData = new PointerEventData(eventSystem);
    }

    void Update()
    {
        // Only respond to left-clicks (Mouse button 0)
        if (Input.GetMouseButtonDown(0))
        {
            LogUIElementsUnderPointer();
        }
    }

    void LogUIElementsUnderPointer()
    {
        // Set the pointer position to the current mouse position
        pointerData.position = Input.mousePosition;

        // Create a list to store the results of the raycast
        var raycastResults = new System.Collections.Generic.List<RaycastResult>();

        // Perform a raycast on the UI using the pointer data
        eventSystem.RaycastAll(pointerData, raycastResults);

        // Log all the UI elements hit by the raycast
        if (raycastResults.Count > 0)
        {
            Debug.Log("UI elements under the pointer:");

            foreach (var result in raycastResults)
            {
                Debug.Log($"Hit UI element: {result.gameObject.name} at position {result.screenPosition}");
            }
        }
        else
        {
            Debug.Log("No UI elements hit by the raycast.");
        }
    }
}
