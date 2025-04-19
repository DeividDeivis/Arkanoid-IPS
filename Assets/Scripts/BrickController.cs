using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private int destroyPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyBrick() 
    {
        //Destroy(gameObject); Podriamos usar el metodo Destroy si quisieramos destruir el gameobject completo pero chocaria con el heck de la ball.
        gameObject.SetActive(false); // En lugar de destruirlo lo desactivamos, de esa forma la referencia del objeto guardada por la Ball no se pierde.
    }
}
