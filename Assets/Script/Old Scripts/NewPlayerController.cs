using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour
{


    public Text vida;
    public float MoveSpeed;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera maincamera;

    public GunController theGun;

    public GameObject YellowNormal;
    public GameObject BlueNormal;
    public GameObject PinkNormal;

    public float Vida;

    public float VidaQuitada;


    public ParticleSystem DieEffectYellow;
    public ParticleSystem DieEffectBlue;
    public ParticleSystem DieEffectPink;


    Collider m_collider;

    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;
    private Renderer renderPlayer;


    public Animator animator;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        DieEffectYellow.Stop();
        DieEffectBlue.Stop();
        DieEffectPink.Stop();
        gameObject.layer = 8;
        YellowNormal.SetActive(true);
        BlueNormal.SetActive(false);
        PinkNormal.SetActive(false);

        renderPlayer = GetComponent<Renderer>();
        myRigidbody = GetComponent<Rigidbody>();
        maincamera = FindObjectOfType<Camera>();
        //m_collider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Vida);


        if (Vida <= 0)
        {
            if (theGun.BulletYellow)
            {
                Instantiate(DieEffectYellow.gameObject, transform.position, Quaternion.identity);
                Vida = 0;
                Destroy(gameObject);
            }
            if (theGun.BulletBlue)
            {
                Instantiate(DieEffectBlue.gameObject, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Vida = 0;
            }
            if (theGun.BulletPink)
            {
                Instantiate(DieEffectPink.gameObject, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Vida = 0;
            }
        }
        if (Vida > 50)
        {
            Vida = 50;
        }

        if (theGun.BulletYellow)
        {
            gameObject.layer = 8;

            GetComponent<Renderer>().material = Yellow_Material;
            YellowNormal.SetActive(true);
            BlueNormal.SetActive(false);
            PinkNormal.SetActive(false);


        }
        if (theGun.BulletBlue)
        {
            gameObject.layer = 9;

            GetComponent<Renderer>().material = Blue_Material;
            YellowNormal.SetActive(false);
            BlueNormal.SetActive(true);
            PinkNormal.SetActive(false);
        }
        if (theGun.BulletPink)
        {
            gameObject.layer = 10;

            GetComponent<Renderer>().material = Pink_Material;
            YellowNormal.SetActive(false);
            BlueNormal.SetActive(false);
            PinkNormal.SetActive(true);
        }

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * MoveSpeed;

        {
            moveVelocity = moveInput * MoveSpeed;
        }
        Ray cameraRay = maincamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLeght;

        if (groundPlane.Raycast(cameraRay, out rayLeght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLeght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
        if (Input.GetMouseButtonDown(0))
            theGun.isFiring = true;
        if (Input.GetMouseButtonUp(0))
            theGun.isFiring = false;
    }
    void FixedUpdate()
    {
        vida.text = Vida.ToString();
        // textvida.text = "= " + Vida.ToString();
        myRigidbody.velocity = moveVelocity;
    }
    public void HacerDaño()
    {
        Vida = Vida - VidaQuitada;
        Debug.Log(Vida);
    }
    public void RecibeVida()
    {
        Vida = Vida + VidaQuitada;
        Debug.Log(Vida);

    }


    private void OnColliderEnter(Collider col)
    {

        // if (collision.gameObject.tag == "Pink" || collision.gameObject.tag == "Blue" || collision.gameObject.tag == "Yellow" )
        //{
        //  m_collider.enabled = !m_collider.enabled;
        //}
    }

}