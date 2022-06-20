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
            var activeScene = SceneManager.GetActiveScene().buildIndex;
            var lastScene = SceneManager.sceneCountInBuildSettings - 1;

            if (activeScene == lastScene)
            {
                gameControl.ShowWinMessage();
            }
            else
            {
                SceneManager.LoadScene(activeScene + 1);
            }
        }
    }
}
