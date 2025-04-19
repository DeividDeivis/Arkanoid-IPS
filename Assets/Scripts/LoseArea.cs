using UnityEngine;

public class LoseArea : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Destroy(other.gameObject);
            player.LoseLife();
        }
    }
}
