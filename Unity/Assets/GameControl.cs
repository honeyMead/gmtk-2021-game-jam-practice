using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private Transform messageBox;

    void Start()
    {
        messageBox = transform.GetChild(0).Find("MessageBox");
        messageBox.gameObject.SetActive(false);
    }

    public void ShowWinMessage()
    {
        messageBox.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
