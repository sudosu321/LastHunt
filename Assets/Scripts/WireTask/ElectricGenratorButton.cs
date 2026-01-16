using System.Threading.Tasks;
using UnityEngine;

public class ElectricGenratorButton : Interactable
{
    public GenElec gen;
    private float slideDistance = 0.17f;
    private float slideSpeed = 1f;
    
    private Vector3 startPos;
    private Vector3 targetPos;
    private float t = 0f;
    private bool elecGenStarted=false;
    public GameObject lights;
    public int id_gen=0;    
    void Start()
    {
        promptMessage="Generator Button";
        startPos = transform.position;
        targetPos = startPos + Vector3.left * slideDistance;
    }

    void Update()
    {
        if(!elecGenStarted)return;
        if (t < 1f)
        {
            t += Time.deltaTime * slideSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
        }
    }
    protected override void Interact()
    {
        if (!gen.feulTankDone)
        {
            promptMessage="Generator wont work";
            return;
        }
        GenratorElecStart();
    }
    void GenratorElecStart()
    {
        lights.SetActive(true);
        promptMessage="Generator working";
        elecGenStarted=true;
        if (id_gen == 1)
        {
            player.A1_FEUL_TASK=true;
            
        }
        else
        {
            player.SERVER_FEUL_TASK=true;
        }
        
        taskActive=false;
    }
}
