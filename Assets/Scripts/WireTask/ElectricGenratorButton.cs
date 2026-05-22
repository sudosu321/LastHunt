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
    public GameObject[] nser;
    public GameObject[] cser;
    public Material nserMat;
    public Material cserMat;
    public AudioSource serverHum;

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
        
        if (id_gen == 1)
        {
            lights.SetActive(true);
            promptMessage="Generator working";
            elecGenStarted=true;
            player.A1_FEUL_TASK=true;
            gen.genHuming.Play();
        }
        else
        {
            if (player.wireTaskComplete)
            {
                lights.SetActive(true);
                promptMessage="Generator working ,Servers are now on !";
                elecGenStarted=true;
                gen.genHuming.Play();
                for(int i =0;i< 9; i++)
                {
                    nser[i].GetComponent<Hinter>().defaultMEssage="Servers are now powered on";
                    nser[i].GetComponent<Hinter>().onInteract="Servers are working";
                    nser[i].GetComponent<Renderer>().material=nserMat;
                }
                for(int i =0;i< 3; i++)
                {
                    cser[i].GetComponent<Hinter>().defaultMEssage="Servers are now powered on";
                    cser[i].GetComponent<Hinter>().onInteract="Servers corrupted";
                    cser[i].GetComponent<Renderer>().material=cserMat;


                }
                serverHum.Play();
                player.SERVER_FEUL_TASK=true;
            }
            else
            {
                promptMessage="circuit couldnt complete";
                
            }
        }
        
    }
}
