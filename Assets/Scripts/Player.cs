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

    public bool isRight, isDodge;
    public float speed = 20;
    public bool gameOver;

    private Vector3 rightPos = Vector3.right * 5 + Vector3.up * 0.5f;
    private Vector3 leftPos = Vector3.left * 5 + Vector3.up * 0.5f;

    // bool to prevent double hit
    private bool isGracePeriod = false;

    // Start is called before the first frame update
    void Awake()
    {
        Lives.Invoke();

        isRight = true;
        isDodge = false;
        gameOver = false;

        potatoAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        noiseParam = virCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        playerCollider = GetComponent<CapsuleCollider>();
        
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



            if (Input.GetKeyDown(KeyCode.Space) && !isDodge)
            {
                //potatoAnimator.SetBool("isJump", true);

                StartCoroutine(DodgeTime());
            }
        }
        else
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
            
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Knife" && !isGracePeriod)
        {
            StartCoroutine(Hit());
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
        yield return new WaitForSeconds(1);
        isGracePeriod = false;
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
