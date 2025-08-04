using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out Chest chest))
                {
                    chest.OnClick();
                }
            }
        }
    }
}
