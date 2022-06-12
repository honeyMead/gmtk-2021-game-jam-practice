using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameControl gameControl;

    void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag("Player");

        if (isPlayer)
        {
            var isLastScene = SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1;
            if (isLastScene)
            {
                gameControl.ShowWinMessage();
            }
        }
    }
}
