using UnityEngine;
using UnityEngine.Rendering;

public class BallController : MonoBehaviour
{
    //[SerializeField] private float initialForce = 3f;
    //private Rigidbody rb;
    [SerializeField] private float ballSpeed = 3f;
    [SerializeField] private bool move = false;
    [SerializeField] private Vector3 moveVector = new Vector3(1f, 1f, 0f);

    private GameObject lastBrickHit;
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector2.one * initialForce, ForceMode.Impulse);*/

        player = FindAnyObjectByType<PlayerController>();
        move = true;
    }

    private void Update()
    {
        if (!move) return;

        transform.position += moveVector * ballSpeed * Time.deltaTime;
    }

    // Se ejecuta cuando el componente collision detecta por primera vez una colisión solida, esta infomacion es guarda en collision. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Brick" && collision.gameObject != lastBrickHit)
        {
            lastBrickHit = collision.gameObject;
            collision.gameObject.GetComponent<BrickController>().DestroyBrick();

            bool lastBrickDestroyed = player.BrickDestroyed();
            if (lastBrickDestroyed)
                move = false;
            //rb.Sleep(); // Al dormir un rigidbody este deja de afectado por las fuerzas aplicadas.
        }

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Brick" || collision.gameObject.tag == "Player") 
        {
            Debug.Log($"Contact {collision.gameObject.name} Normal: {collision.contacts[0].normal}");
            moveVector = Vector3.Reflect(moveVector, collision.contacts[0].normal);
        }
    }

    public void ResetBall() 
    {
        
    }
}
