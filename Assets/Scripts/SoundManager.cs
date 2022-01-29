using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource upgradeDamage;
    [SerializeField] private AudioSource upgradeStuff;
    [SerializeField] private AudioSource noSound;
    [SerializeField] private AudioSource mainSound;
    [SerializeField] private AudioSource moneySound;
    [SerializeField] private AudioSource timeSound;
    [SerializeField] private Image soundOn;
    [SerializeField] private Image soundOff;


    private bool _musicOn = true;

    public void CheckMusic()
    {
        if (_musicOn)
        {
            StopMainSound();
            soundOff.gameObject.SetActive(false);
            soundOn.gameObject.SetActive(true);
            _musicOn = false;
        }
        else
        {
            PlayMainSound();
            soundOff.gameObject.SetActive(true);
            soundOn.gameObject.SetActive(false);
            _musicOn = true;
        }
    }

    private void PlayMainSound()
    {
        Debug.Log("playMainSound Played");
        mainSound.Play();
    }

    private void StopMainSound()
    {
        Debug.Log("StopMainSound Played");
        mainSound.Stop();
    }

    public void PlayHitSound()
    {
        Debug.Log("playHitSound Played");
        hit.Play();
    }

    public void PlayNoSound()
    {
        Debug.Log("playNoSound Played");
        noSound.Play();
    }

    public void PlayUpgradeDamageSound()
    {
        Debug.Log("playUpgradeDamageSound Played");
        upgradeDamage.Play();
    }

    public void PlayUpgradeStuffSound()
    {
        Debug.Log("playUpgradeStuffSound Played");
        upgradeStuff.Play();
    }
    
    public void PlayMoneySound()
    {
        moneySound.Play();
    }

    public void PlayTimerSound()
    {
        timeSound.Play();
    }

    public void StopTimerSound()
    {
        timeSound.Stop();
    }
}