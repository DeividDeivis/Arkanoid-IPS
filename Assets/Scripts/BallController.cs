using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float initialForce = 3f;
    private Rigidbody rb;

    private GameObject lastBrickHit;
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector2.one * initialForce, ForceMode.Impulse);

        player = FindAnyObjectByType<PlayerController>();
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
                rb.Sleep(); // Al dormir un rigidbody este deja de afectado por las fuerzas aplicadas.
        }
    }
}
