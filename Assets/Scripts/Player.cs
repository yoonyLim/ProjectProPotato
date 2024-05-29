using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Player : MonoBehaviour
{
    [SerializeField] ResultDisplay resultDisplay;

    [SerializeField] GenericObserver<int> Lives = new GenericObserver<int>(3);

    [SerializeField] GameObject normalFace;
    [SerializeField] GameObject invincibleFace;
    [SerializeField] GameObject hitFace;

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
    private int blow;
    private bool blowbefore = false, blowcurrent;
   

    //left and right position for potato
    private Vector3 rightPos = Vector3.right * 5 + Vector3.up * 0.5f;
    private Vector3 leftPos = Vector3.left * 5 + Vector3.up * 0.5f;

    // bool to prevent double hit
    private bool isGracePeriod = false;

    //var for fever
    public float feverWaitTime = 5f;
    private enum feverState { steady, ready, on, used }
    [SerializeField] feverState fever = feverState.steady;
    [SerializeField] GenericObserver<int> FeverGauge = new GenericObserver<int>(0);
    public float spinRate = 17f;
    public float spinValue;
    [SerializeField] GameObject flameEffect;

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
        normalFace.SetActive(true);
        Lives.Invoke();
        FeverGauge.Invoke();

        isRight = true;
        isDodge = false;
        gameOver = false;
        flameEffect.SetActive(false);

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
                blowcurrent = NamedPipeClient1.Instance.PotDiff > GameManager.instance.alcoholThreshold;
                if (blowcurrent && !blowbefore)
                {
                    if (fever != feverState.ready)
                    {
                        StartCoroutine(DodgeTime());
                    }
                    else
                    {
                        StartCoroutine(FeverStart());
                    }
                    

                }
               
                blowbefore = NamedPipeClient1.Instance.PotDiff > GameManager.instance.alcoholThreshold;
                
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
            
            //fever check
            if (fever == feverState.steady && FeverGauge.Value >= GameManager.instance.maxPotatoFeverCount)
            {
                fever = feverState.ready;
            }

            if (fever == feverState.on)
            {
                potatoRenderer.material.color = new Color(Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, 5);
                armLegRenderer.material.color = new Color(Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, Random.Range(0, 255) / 50f, 5);
                spinValue += spinRate ;
                runningBody.transform.eulerAngles = new Vector3(0, spinValue+180, 0);
            }

            //potato win
            if (GameManager.instance.elapsedTime > 60f)
            {
                transform.position += Vector3.back * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.z < 28)
            transform.position += Vector3.forward * Time.deltaTime * speed;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground")){
            isDodge = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        
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

    public void ObserveAttack(int attackTimes)
    {
        
        StartCoroutine(JudgeAttacked());

    }

    IEnumerator JudgeAttacked()
    {
        int currentHeath = Lives.Value;
        yield return new WaitForSeconds(1.5f);
        if(currentHeath == Lives.Value && FeverGauge.Value < GameManager.instance.maxPotatoFeverCount)
        {
            FeverGauge.Value++;
        }
    }


    IEnumerator DodgeTime()
    {
        isRight = !isRight;
        isDodge = true;
        AudioManager.instance.PlayJumpSound();
        rigid.velocity += Vector3.up * 20f;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity += Vector3.down * 30f;
        yield return new WaitForSeconds(0.4f);
    }

    IEnumerator Hit()
    {
        isGracePeriod = true;
        AudioManager.instance.PlayHitSound();
        StartCoroutine(HitFace());
        noiseParam.m_AmplitudeGain = 12;
        Lives.Value--;
        yield return new WaitForSeconds(0.3f);
        noiseParam.m_AmplitudeGain = 0;
        isGracePeriod = false;
    }

    IEnumerator HitFace()
    {
        normalFace.SetActive(false);
        invincibleFace.SetActive(false);
        hitFace.SetActive(true);
        yield return new WaitForSeconds(1f);
        hitFace.SetActive(false);
        normalFace.SetActive(true);
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
        normalFace.SetActive(false);
        invincibleFace.SetActive(true);
        hitFace.SetActive(false);
        fever = feverState.on;
        potatoAnimator.SetBool("Fever", true);
        flameEffect.SetActive(true);

        yield return new WaitForSeconds(10f);
        normalFace.SetActive(true);
        invincibleFace.SetActive(false);
        hitFace.SetActive(false);
        potatoRenderer.material.color = originColor;
        armLegRenderer.material.color = originColor;
        fever = feverState.steady;
        potatoAnimator.SetBool("Fever", false);
        runningBody.transform.rotation = Quaternion.Euler(0, 180f, 0);
        flameEffect.SetActive(false);
        FeverGauge.Value = 0;
    }

    IEnumerator Death()
    {
        playerCollider.enabled = false;
        runningBody.SetActive(false);
        corpse.SetActive(true);
        resultDisplay.UpdateWinner("교수님");
        yield return new WaitForSeconds(0f);
    }
}
