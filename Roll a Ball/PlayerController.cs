using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float moc_skoku;

    private Vector3 moveVector;

    public AudioSource[] sounds;

    public AudioClip pickSound;
    public AudioClip enemySound;
    public AudioClip jumpSound;
    public AudioClip winSound;

    AudioSource pick;
    AudioSource enemy;
    AudioSource jump;
    AudioSource win;

    public Text countText;
    public Text winText;

    private Rigidbody rb;

    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        GetComponent<AudioSource>().playOnAwake = false;
        sounds = GetComponents<AudioSource>();
        pick = sounds[0];
        enemy = sounds[1];
        jump = sounds[2];
        win = sounds[3];
    }

    //Kiedy trzeba fizyki
    void FixedUpdate()
    {
        /*float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool spacja = Input.GetKeyDown("space");*/

        float moveHorizontal = Input.acceleration.x;
        float moveVertical = Input.acceleration.y;
        bool spacja = Input.GetMouseButtonDown(0);

        bool spacjaF = false;
        if (spacja && !spacjaF)
        {
            moveVector = new Vector3(moveHorizontal, moc_skoku, moveVertical);
            spacjaF = true;
            rb.AddForce(moveVector * speed);
            jump.Play();
        }
        else
        {
            moveVector = new Vector3(moveHorizontal,  0, moveVertical);
            spacjaF = !spacjaF;
            rb.AddForce(moveVector * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            gameObject.transform.localScale *= 1.05f;
            count++;
            SetCountText();
            pick.Play();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            rb.position = new Vector3(-1.5f, 0.5f, -12.0f);
            enemy.Play();
        }
    }

    void SetCountText()
    {
        countText.text = "Ilość: " + count.ToString();
        if(count >= 12)
        {
            winText.text = "Wygrałeś!!!";
            win.Play();
        }
    }
}
