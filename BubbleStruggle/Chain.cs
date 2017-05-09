using UnityEngine;

public class Chain : MonoBehaviour {
    public Transform player;

    public float speed = 2f;

    public AudioClip laserSound;
    AudioSource laser;

    public static bool IsFired;

    void Start () {
        IsFired = false;
        laser = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            //szczau
            laser.Play();
            IsFired = true;
        }
        if (IsFired){
            transform.localScale = transform.localScale + Vector3.up * Time.deltaTime * speed;
        }
        else{
            Vector3 wektor = new Vector3(player.position.x, player.position.y, player.position.z);
            transform.position = wektor;
            transform.localScale = new Vector3(1f, 0f, 1f);
        }
    }
}
