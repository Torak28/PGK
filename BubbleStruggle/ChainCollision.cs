using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChainCollision : MonoBehaviour {

    private int ile;
    public Text countText;
    public Text winText;

    public AudioSource[] sounds;
    public AudioClip winSound;
    public AudioClip pickSound;

    AudioSource win;
    AudioSource pick;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        win = sounds[0];
        pick = sounds[1];
        ile = 0;
        winText.text = "";
        SetCountText();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Chain.IsFired = false;

        if (col.tag == "Ball")
        {
            //Debug.Log(col.name);
            ile++;
            SetCountText();
            //UderzeniePily
            pick.Play();
            col.GetComponent<Ball>().Split();
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Punkty " + ile.ToString();
        if (ile == 31)
        {
            win.Play();
            winText.text = "Wygrales";
        }
    }

    }
