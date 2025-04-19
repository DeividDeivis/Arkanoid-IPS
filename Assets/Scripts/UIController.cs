using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Puntaje a mostrar.
    [SerializeField] private TextMeshProUGUI lifesText; // Vidas a mostrar.
    [SerializeField] private GameObject EndScreenContainer; // Contenedor de los elelemntos de la pantalla final.
    [SerializeField] private TextMeshProUGUI endResultText; // Este texto mustra Victoria o Derrota.
    [SerializeField] private Button retryButton; // Boton que recarga la escena completa.


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        retryButton.onClick.AddListener(RetryButtonClicked); // Agregamos un listener a evento onClick del boton
        scoreText.text = $"PUNTAJE: 0";
    }

    public void ShowEndScreen(bool isWin)
    {
        if (isWin)
            endResultText.text = "GANASTE";
        else
            endResultText.text = "PERDISTE";

        EndScreenContainer.SetActive(true);
    }

    private void RetryButtonClicked() 
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single); // Cargamos la escena con el index 0 de manera asincrónica
    }

    public void UpdateScore(int newScore) { scoreText.text = $"PUNTAJE: {newScore}"; }
    public void UpdateLifes(int lifes) { lifesText.text = $"VIDAS: {lifes}"; }
}
