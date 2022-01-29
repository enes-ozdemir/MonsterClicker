using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private Text checkScoreText;

    private void Update()
    {
        checkScoreText.text = gameManager.GetComponent<GameManager>().GetScore().ToString("F2");
    }

    public void PlayAgainOnClick()
    {
        Debug.Log("PlayAgainOnClick");
        gameManager.GetComponent<GameManager>().RestartGame();
    }

    public void QuitOnClick()
    {
        Debug.Log("QuitOnClick");
        Application.Quit();
    }
}