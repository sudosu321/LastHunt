using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam ; 
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private PlayerUI playerUI;
    private float distance = 10f;

    private InputManager inputManager ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam=GetComponent<PlayerLook>().cam;
        playerUI= GetComponent<PlayerUI>();
        inputManager= GetComponent<InputManager>();
        
    }
    // Update is called once per frame
    void Update()
    {
        playerUI.updateText(string.Empty);
        Ray ray  = new Ray(cam.transform.position,cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction*distance);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, distance, mask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                playerUI.updateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.baseInteract();
                }
            }
        }
    }
}
