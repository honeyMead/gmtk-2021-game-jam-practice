using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private Transform messageBox;
    private static GameObject permanentAudio = null;

    void Awake()
    {
        var audios = GameObject.FindGameObjectsWithTag("audio");

        foreach (var audio in audios)
        {
            if (permanentAudio == null)
            {
                permanentAudio = audio;
                DontDestroyOnLoad(permanentAudio);
                return;
            }
            else if (audio != permanentAudio)
            {
                Destroy(audio);
            }
        }
    }

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
