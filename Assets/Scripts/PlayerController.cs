using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xLimitMove = 18f;

    [SerializeField] private int lifes = 3;

    [SerializeField] private int bricksInLevel = 0;
    [SerializeField] private int playerScore = 0;
    private bool gameOver = false;

    private UIController m_UI;
    [SerializeField] private GameObject ballPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Buscamos todos los objetos del tipo BrickController mediante el metodo FindObjectsByType, el cual devuelve un array con los objetos encontrados.
        bricksInLevel = FindObjectsByType<BrickController>(FindObjectsSortMode.InstanceID).Length; // Lenght devuelve el tamaño del array, la cantidad de objetos que tiene.
        
        m_UI = FindFirstObjectByType<UIController>();
        m_UI.UpdateLifes(lifes);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return; // Al utilizar la sentencia return hacemos que el codigo no continue si no que retorne al flujo que lo llamo.

        // Podemos obtener el valor del input Horizontal configurado en el Input Manager. valores posibles -1,0,1
        // Usamos Time.deltaTime para fijar el movimiento al tiempo y no a la taza de fps
        float movInputX = Input.GetAxisRaw("Horizontal");     
        transform.position += new Vector3(movInputX * moveSpeed * Time.deltaTime, 0f, 0f);
        // Clamp Pos X
        float clampPosX = Mathf.Clamp(transform.position.x, -xLimitMove, xLimitMove);
        transform.position = new Vector3(clampPosX, transform.position.y, transform.position.z);
    }

    public bool BrickDestroyed() 
    {
        bricksInLevel--;

        playerScore += 100;
        m_UI.UpdateScore(playerScore);

        if (bricksInLevel <= 0) // Revisamos si quedan ladrillos por destruir y le devolvemos a la Ball si se destruyo el ultimo o no.
        {
            Debug.Log("Game Win");
            gameOver = true; // Pausamos la lectura del input del jugador para que el player se pare.
            m_UI.ShowEndScreen(true);
            return true;
        }
        else
            return false;
    }

    public void LoseLife() 
    {
        lifes--;
        m_UI.UpdateLifes(lifes);
        if (lifes <= 0)
        {
            gameOver = true;
            m_UI.ShowEndScreen(false);
        }
        else
        {
            transform.position = new Vector3(0, transform.position.y, 0); // Movemos al player al centro de nuevo
            Instantiate(ballPrefab, new Vector3(0, -7.5f, 0), Quaternion.identity); // Instancia una nueva Ball
        }
    }
}
