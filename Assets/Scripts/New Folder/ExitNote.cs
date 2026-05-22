using UnityEngine;

public class ExitNote : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject controlUI;
    public GameObject noteUI;
    public PlayerHold player;
   public void OnClick()
    {
        if(player.paperObject==null)return;
        player.paperObject.SetActive(true);
        player.paperObject.transform.SetParent(null);
        controlUI.SetActive(true);
        noteUI.SetActive(false);
        player.isNoteOpened=false;

    }
}
