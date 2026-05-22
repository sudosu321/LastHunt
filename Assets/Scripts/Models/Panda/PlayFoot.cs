using UnityEngine;

public class PlayFoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Enemy enemy;

    void Start()
    {
        
    }
    public void playFoot()
    {
     //   if(!foot.isPlaying)foot.pitch=Random.Range(foot.pitch-0.2f,foot.pitch+0.2f);
        enemy.audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
