using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [SerializeField] ResultDisplay resultDisplay;

    [SerializeField] GenericObserver<int> Lives = new GenericObserver<int>(3);

    Animator potatoAnimator;
    Rigidbody rigid;

    public CinemachineVirtualCamera virCam;
    CinemachineBasicMultiChannelPerlin noiseParam;
    public GameObject runningBody;
    public GameObject corpse;
    CapsuleCollider playerCollider;


    private bool isRight, isDodge;
    public float speed = 20;
    public bool gameOver;
   

    //left and right position for potato
    private Vector3 rightPos = Vector3.right * 5 + Vector3.up * 0.5f;
    private Vector3 leftPos = Vector3.left * 5 + Vector3.up * 0.5f;

    // bool to prevent double hit
    private bool isGracePeriod = false;

    //var for fever
    public float feverWaitTime = 5f;
    private enum feverState { steady, ready, on, used }
    [SerializeField] feverState fever = feverState.steady;

    //var for hold input
    private bool holding = false;
    private float holdStart;

    //renderers
    Renderer potatoRenderer;
    Renderer armLegRenderer;

    // color to save original
    Color originColor;

    // Start is called before the first frame update
    void Awake()
    {
        Lives.Invoke();

        isRight = true;
        isDodge = false;
        gameOver = false;
        

        potatoAnimator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        noiseParam = virCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        playerCollider = GetComponent<CapsuleCollider>();

        potatoRenderer = runningBody.GetComponentsInChildren<Renderer>()[0];
        armLegRenderer = runningBody.GetComponentsInChildren<Renderer>()[1];
        originColor = potatoRenderer.material.color;
        
        runningBody.SetActive(true);
        corpse.SetActive(false);
        playerCollider.enabled = true;

        transform.position = rightPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (isRight && transform.position.x < rightPos.x)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
            else if (!isRight && transform.position.x > leftPos.x)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }

            if (!isDodge)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    holding = true;
                    holdStart = Time.time;
                
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    float howLong = Time.time - holdStart;
                    if(howLong < 0.7f || fever != feverState.ready)
                    {
                        StartCoroutine(DodgeTime());
                    }
                    else
                    {
                        StartCoroutine(FeverStart());
                    }
                    holding = false;
                }       
            }
            

            if (fever == feverState.steady && GameManager.instance.elapsedTime > feverWaitTime)
            {
                fever = feverState.ready;
            }

            if (fever == feverState.on)
            {
                potatoRenderer.material.color = new Color(Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, 5);
                armLegRenderer.material.color = new Color(Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, 5);
            }
        }
        else
        {
            if (transform.position.z <30)
            transform.position += Vector3.forward * Time.deltaTime * speed;
            
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Knife" && !isGracePeriod && fever != feverState.on)
        {
            StartCoroutine(Hit());
            StartCoroutine(HitEffect());
            if(Lives.Value <= 0)
            {
                gameOver = true;
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator DodgeTime()
    {
        isRight = !isRight;
        isDodge = true;
        rigid.velocity += Vector3.up * 20f;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity += Vector3.down * 30f;
        yield return new WaitForSeconds(0.4f);
        isDodge = false;
    }

    IEnumerator Hit()
    {
        isGracePeriod = true;
        noiseParam.m_AmplitudeGain = 12;
        Lives.Value--;
        yield return new WaitForSeconds(0.3f);
        noiseParam.m_AmplitudeGain = 0;
        isGracePeriod = false;
    }

    IEnumerator HitEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            potatoRenderer.material.color = Color.red;
            armLegRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.06f);
            potatoRenderer.material.color = originColor;
            armLegRenderer.material.color = originColor;
            yield return new WaitForSeconds(0.06f);
        }
    }

    IEnumerator FeverStart()
    {
        fever = feverState.on;
        potatoAnimator.SetBool("Fever", true);
        yield return new WaitForSeconds(10f);
        potatoRenderer.material.color = originColor;
        armLegRenderer.material.color = originColor;
        fever = feverState.used;
        potatoAnimator.SetBool("Fever", false);
    }

    IEnumerator Death()
    {
        playerCollider.enabled = false;
        runningBody.SetActive(false);
        corpse.SetActive(true);
        resultDisplay.UpdateWinner("±³¼ö´Ô");
        yield return new WaitForSeconds(0f);
    }
}
