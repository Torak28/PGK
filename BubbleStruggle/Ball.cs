using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector2 startForce;

    public GameObject nextBall;

    public Rigidbody2D rb;

	void Start () {
        rb.AddForce(startForce, ForceMode2D.Impulse);
	}

    public void Split()
    {
        if (nextBall != null)
        {
            GameObject ball1 = Instantiate(nextBall, rb.position + Vector2.right / 4f, Quaternion.identity);
            GameObject ball2 = Instantiate(nextBall, rb.position + Vector2.left / 4f, Quaternion.identity);

            ball1.GetComponent<Ball>().startForce = new Vector2(3f, 8f);
            ball2.GetComponent<Ball>().startForce = new Vector2(-3f, 8f);
        }

        Destroy(gameObject);
    }

}
