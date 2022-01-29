using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour

{
    public GameObject menuScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject googlePlay;

    public bool isReward = false;

    private void Start()
    {
        menuScreen.SetActive(true);
    }

    public void GoToGameScreen(bool isRewarded)
    {
        isReward = isRewarded;
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void GoToMenuScreen()
    {
        SceneManager.LoadScene("MainScene");
        menuScreen.SetActive(true);
    }

    public void QuitOnClick()
    {
        Debug.Log("QuitOnClick");
        Application.Quit();
    }

    public void GoToLeaderboard()
    {
        Debug.Log("GoToLeaderboard");
        googlePlay.gameObject.GetComponent<PlayGames>().ShowLeaderboard();
    }
}