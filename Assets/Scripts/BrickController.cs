using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BrickController : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int destroyPoints;

    public Action<int> OnDestroy;

    /*public delegate void OnDestroyDelegate(int newScore);
    public static event OnDestroyDelegate OnDestroy;

    public UnityEvent<int> OnDestroy;*/

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
        OnDestroy?.Invoke(score);
        //Destroy(gameObject); Podriamos usar el metodo Destroy si quisieramos destruir el gameobject completo pero chocaria con el heck de la ball.
        gameObject.SetActive(false); // En lugar de destruirlo lo desactivamos, de esa forma la referencia del objeto guardada por la Ball no se pierde.
    }
}
