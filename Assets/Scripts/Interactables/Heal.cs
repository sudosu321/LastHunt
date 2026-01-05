using UnityEngine;

public class Heal : Interactable
{
    public PlayerHealth playerHealth;
    public BloodEffect healMyself;
    public int healAmount=30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Interact()
    {
        healMyself.ShowBlood();
        playerHealth.playerHealth=playerHealth.playerHealth+healAmount;
        playerHealth.MoveBar(healAmount);
        Debug.Log("PlayerHEalth increased");
        Destroy(gameObject);
    }
}
