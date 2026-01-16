using UnityEngine;
using UnityEngine.EventSystems;

public class SprintHold : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public PlayerMovement player;

    public void OnPointerDown(PointerEventData eventData)
    {
         if (player.crouching) return;
        player.sprinting = true;
        player.sprinholdactive=true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.sprinting = false;
        player.sprinholdactive=false;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //player.sprinting = false;
    }
}
