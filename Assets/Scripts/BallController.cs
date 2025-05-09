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
        if (!move) return; // Si move es igual a falso no se mueve.

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
            // collision.contacts o collision.GetContact() nos devuelve un array de los puntos contra los que impacto nuestro collider
            // La normal de un objecto es el vector hacia donde la cara su superficie apunta o mira.
            // ej: la pared derecha devuelve como normal (-1,0,0) por que esta tiene su superficie apuntando hacia la izquierda 
            Debug.Log($"Contact {collision.gameObject.name} Normal: {collision.GetContact(0).normal}");
            // Vector3.Reflect nos devuelve un vector resultante tomando el vector de movimiento que creamos y la normal de la superficie.
            // ej: si nuestro vector de movimiento es (1,1,0), es decir nos estamos moviendo en diagonal hacia la derecha, e impactamos a
            // un ladrillo desde abajo , normal = (0,-1,0), el vector reflejado podria ser (1,-1,0), haciendo que la pelota vaya hacia abajo.
            moveVector = Vector3.Reflect(moveVector, collision.contacts[0].normal);
        }
    }

    // Podria utilizarse un metodo que resetee la posicion de la pelota en lugar de destruirla.
    public void ResetBall() 
    {
        
    }
}
