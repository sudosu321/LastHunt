using UnityEngine;
using UnityEngine.UIElements;


public class PlayerInteract : MonoBehaviour
{
    public Camera cam ; 
    public Transform hand;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private PlayerUI playerUI;
    public float distance = 8f;
    public GameObject useButton;
    private InputManager inputManager ;
    public Interactable current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam=GetComponentInChildren<PlayerLook>().cam;
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
                //Debug.Log(interactable.transform.name);
                useButton.SetActive(interactable.taskActive);
                playerUI.updateText(interactable.promptMessage);

                current=interactable;
                if(current!=null) current.interactableConnect();
                
            }
            else
            {
                current=null;
                useButton.SetActive(false);
            }
        }
        else
        {
                current=null;

            useButton.SetActive(false);
        }
        
    }
    public void DropItem(){
       // GetComponent<PlayerHold>().dropItem();
        if (current != null)
        {
            
        }
    }
    public void Use()
    {
        if (current != null)
        {
            current.baseInteract();
            useButton.SetActive(false);
        }
    }
}
