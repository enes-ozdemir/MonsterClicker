using DragonBones;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScreenManager screenManager;

    [SerializeField] private Material getHitMaterial;
    [SerializeField] private Material noneMaterial;

    [SerializeField] private GameObject soundManager;
    [SerializeField] private GameObject googlePlay;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject scoreScreen;

    [SerializeField] private bool gameOn = true;

    private int _currentEnemy;
    private const float ButtonReactivateDelay = 0.04f;

    //UI region
    [SerializeField] private Texture[] backgrounds;
    [SerializeField] private UnityArmatureComponent armatureComponent;
    [SerializeField] private GameObject moneyAnim;

    //Timer values
    [SerializeField] private Text timerText;
    [SerializeField] private Text generalTimerText;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private float generalTimer;
    [SerializeField] private float timer;
    [SerializeField] private float damTimer;
    [SerializeField] private float timerCap;

    //Enemy
    [SerializeField] private Texture[] enemyImages;
    [SerializeField] private RawImage enemy;
    [SerializeField] private Text enemyName;
    [SerializeField] private Button enemyFrameButton;
    [SerializeField] private GameObject enemyFrame;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthText;
    [SerializeField] private double healthMax;
    [SerializeField] private double currentHealth;
    [SerializeField] private bool isBoss;

    //User info
    [SerializeField] private double money;
    [SerializeField] private int stage;
    [SerializeField] private int killCount;
    [SerializeField] private int killsMax;
    [SerializeField] private Text earnedMoney;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text stageText;
    [SerializeField] private Text killCountText;
    [SerializeField] private Text dpcText;
    [SerializeField] private Text dpsText;
    //end region

    //Upgrade region
    //Upgrade benefit text
    [SerializeField] private Text damageClickText;
    [SerializeField] private Text damageSecondText;
    [SerializeField] private Text extraClickText;

    [SerializeField] private Text extraMoneyText;

    //Upgrade level text
    [SerializeField] private Text damageClickLevelText;
    [SerializeField] private Text damageSecondLevelText;
    [SerializeField] private Text extraClickLevelText;
    [SerializeField] private Text extraMoneyLevelText;

    //Upgrade cost Text
    [SerializeField] private Text damageClickCostText;
    [SerializeField] private Text damageSecondCostText;
    [SerializeField] private Text extraClickCostText;
    [SerializeField] private Text extraMoneyCostText;

    //Upgrade values
    [SerializeField] private double dpc;
    [SerializeField] private double dps;
    [SerializeField] private int extraClick;
    [SerializeField] private int extraMoney;

    //Upgrade Cost
    [SerializeField] private int dpcCost;
    [SerializeField] private int dpsCost;
    [SerializeField] private int extraClickCost;
    [SerializeField] private int extraMoneyCost;

    //Upgrade Level
    [SerializeField] private int dpcLevel;
    [SerializeField] private int dpsLevel;
    [SerializeField] private int extraClickLevel;

    [SerializeField] private int extraMoneyLevel;
    //end region


    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        //Set enemy material
        noneMaterial = enemy.material;

        //Set screen
        gameOverScreen.SetActive(false);
        scoreScreen.SetActive(false);

        generalTimer = 0;
        _currentEnemy = 0;
        damTimer = 1;

        //Set values
        dpc = 10;
        dps = 0;
        extraClick = 0;
        extraMoney = 0;
        dpcCost = 50;
        dpsCost = 100;
        extraClickCost = 900;
        extraMoneyCost = 200;
        dpcLevel = 1;
        dpsLevel = 1;
        CheckForReward();
        extraClickLevel = 1;
        extraMoneyLevel = 1;
        money = 0;
        stage = 1;
        killsMax = 10;
        killCount = 1;
        isBoss = false;
        SetEnemyHealth();
        currentHealth = healthMax;
        timerCap = 10;
        timer = timerCap;

        //Set Objects
        timerObject.SetActive(false);
        moneyAnim.SetActive(false);

        //Start the game
        gameOn = true;
    }

    /**
     * Check if rewarded video is watched
     */
    private void CheckForReward()
    {
        if (screenManager.isReward)
        {
            var randomNumber = Random.Range(1, 3);
            if (randomNumber == 1)
            {
                UpgradeDpc(true);
            }
            else
            {
                UpgradeDps(true);
            }

            screenManager.isReward = false;
        }
    }

    private void Update()
    {
        if (gameOn)
        {
            SetGeneralTimer();
            if (CheckBoss())
            {
                timer -= Time.deltaTime;
                timerObject.SetActive(true);
            }

            CheckTimer();
            SetDps();
        }

        SetUI();
    }

    /**
     * Check if timer is low or up
     */
    private void CheckTimer()
    {
        if (timer < 5)
        {
            timerText.color = Color.red;
        }

        if (timer <= 0)
        {
            timerObject.SetActive(false);
            timer = timerCap;
            StopTimerSound();
            gameOn = false;
            gameOverScreen.SetActive(true);
            Debug.Log("Time is up");
        }
    }

    /**
     * Set a delay for clicks
     */
    private void WhenClicked()
    {
        enemyFrameButton.interactable = false;
        StartCoroutine(EnableButtonAfterDelayCoroutine(enemyFrameButton, ButtonReactivateDelay));
    }

    private IEnumerator EnableButtonAfterDelayCoroutine(Button button, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        button.interactable = true;
        enemy.material = noneMaterial;
    }

    public float GetScore()
    {
        return generalTimer;
    }

    public void RestartGame()
    {
        Debug.Log("Restart the Game");
        Start();
    }

    /**
     * Set general timer of the game
     */
    private void SetGeneralTimer()
    {
        if (gameOn)
            generalTimer += Time.deltaTime;
    }

    private void SetUI()
    {
        moneyText.text = "$" + money.ToString("F0");
        stageText.text = "Stage - " + stage;
        killCountText.text = killCount + " / " + killsMax;
        dpcText.text = dpc + " DPC";
        dpsText.text = dps + " DPS";
        healthText.text = currentHealth.ToString("F0") + " / " + healthMax.ToString("F0") + " HP";
        healthBar.fillAmount = (float) (currentHealth / healthMax);

        damageClickText.text = "+ " + dpcLevel * 10 + " Damage";
        damageSecondText.text = "+ " + dpsLevel * 5 * Math.Pow(2, dpsLevel) + " Damage";
        extraClickText.text = "+ " + extraClickLevel * 1 + " Click";
        extraMoneyText.text = "+% " + extraMoneyLevel * 10 + " Money";
        damageClickLevelText.text = dpcLevel.ToString();
        damageSecondLevelText.text = dpsLevel.ToString();
        extraClickLevelText.text = extraClickLevel.ToString();
        extraMoneyLevelText.text = extraMoneyLevel.ToString();
        damageClickCostText.text = dpcCost + "$";
        damageSecondCostText.text = dpsCost + "$";
        extraClickCostText.text = extraClickCost + "$";
        extraMoneyCostText.text = extraMoneyCost + "$";

        enemy.texture = enemyImages[_currentEnemy];
        enemyName.text = enemyImages[_currentEnemy].name;
        enemyFrame.GetComponent<RawImage>().texture = backgrounds[stage - 1];

        timerText.text = timer.ToString("F2");
        generalTimerText.text = generalTimer.ToString("F2");
    }

    /*
     * Upgrade Damage Per Click value
     */
    private void UpgradeDpc(bool isRewarded)
    {
        if (money >= dpcCost | isRewarded)
        {
            money -= dpcCost;
            dpcLevel++;
            dpc += dpcLevel * 10;
            dpcCost = dpcLevel * 200 + (int) Math.Pow(5, dpcLevel);
            PlayUpgradeDamageSound();
        }
        else
        {
            PlayErrorSound();
        }
    }

    /*
     * Upgrade Damage Per Second value
     */
    private void UpgradeDps(bool isRewarded)
    {
        if (money >= dpsCost | isRewarded)
        {
            money -= dpsCost;
            dpsLevel++;
            dps += dpsLevel * 5 * Math.Pow(2, dpsLevel);
            dpsCost = dpsLevel * 200 + (int) Math.Pow(5, dpsLevel);
            PlayUpgradeDamageSound();
        }
        else
        {
            PlayErrorSound();
        }
    }

    /*
     * Give user a extra click
     */
    private void UpgradeExtraClick()
    {
        if (money >= extraClickCost)
        {
            money -= extraClickCost;
            extraClickLevel++;
            extraClick = extraClickLevel * 1;
            extraClickCost = extraClickCost + (int) Math.Pow(10, extraClickLevel) + extraClickLevel * 300;

            PlayUpgradeOthersSound();
        }
        else
        {
            PlayErrorSound();
        }
    }

    /*
     * Give %10 more extra money to user for every monster killed
     */
    private void UpgradeExtraMoney()
    {
        if (money >= extraMoneyCost)
        {
            money -= extraMoneyCost;
            extraMoneyLevel++;
            extraMoney = extraMoneyLevel * 10;
            extraMoneyCost = extraMoneyCost + (int) Math.Pow(5, extraMoneyLevel) + extraMoneyLevel * 75;
            PlayUpgradeOthersSound();
        }
        else
        {
            PlayErrorSound();
        }
    }

    /*
     * Set damage per second to damage to player
     */
    private void SetDps()
    {
        if (dps != 0)
        {
            if (damTimer > 0)
            {
                damTimer -= Time.deltaTime;
            }

            if (damTimer <= 0)
            {
                currentHealth -= dps;
                damTimer = 1;
            }
        }
    }

    /*
     * Hit to the Enemy
     */
    private void Hit()
    {
        enemy.material = getHitMaterial;
        armatureComponent.animation.Play("Attack A");
        Debug.Log("Attacked");
        WhenClicked();
        soundManager.GetComponent<SoundManager>().PlayHitSound();
        if (extraClick != 0) currentHealth -= dpc * extraClick;
        else currentHealth -= dpc;
        CheckDead();
    }

    /*
     * Check if enemy dead
     */
    private void CheckDead()
    {
        if (currentHealth <= 0)
        {
            GiveMoneyToPlayer();
            killCount++;

            SetBossRound();

            SetEnemyHealth();

            GoToNextStage();

            if (gameOn)
                _currentEnemy++;
        }
    }

    private void SetBossRound()
    {
        if (CheckBoss())
        {
            timerObject.SetActive(true);
            isBoss = true;
            Debug.Log("Boss Round");
            PlayTimerSound();
            timer -= Time.deltaTime;
            stageText.text = "BOSS Stage - " + stage;
        }
        else
        {
            isBoss = false;
            timerObject.SetActive(false);
            StopTimerSound();
        }
    }


    private void GoToNextStage()
    {
        if (killCount > killsMax && stage < 4)
        {
            StopTimerSound();
            killCount = 1;
            stage++;
            timer = timerCap;
            timerObject.SetActive(false);
        }
        else if (stage == 4 && killCount == 11)
        {
            Debug.Log("Boss dead");
            scoreScreen.SetActive(true);
            gameOn = false;
            googlePlay.GetComponent<PlayGames>().AddScoreToLeaderboard();
        }
    }

    private void GiveMoneyToPlayer()
    {
        moneyAnim.SetActive(true);
        PlayMoneySound();
        moneyAnim.GetComponent<Animator>().Play("MoneyAnimation", 0, 0);

        var cash = Math.Ceiling(healthMax / 10) + 40;
        money += cash + (cash * extraMoney / 100);
        earnedMoney.text = "+ " + (cash + (cash * extraMoney / 100));
    }


    private void SetEnemyHealth()
    {
        var addedHealth = 0;
        var bossHealth = 0;
        if (isBoss) bossHealth = stage * 1000 + stage * killCount * 30;
        else
        {
            addedHealth = Random.Range(50, 80) * stage * 3 + killCount * 10;
        }

        healthMax = 10 * Math.Pow(2, stage * 2) + addedHealth + bossHealth + killCount * 10;
        currentHealth = healthMax;
    }

    /**
     * Check if next enemy should be a boss
     */
    private bool CheckBoss()
    {
        return killCount == 10;
    }

    private void PlayUpgradeOthersSound()
    {
        soundManager.GetComponent<SoundManager>().PlayUpgradeStuffSound();
    }

    private void PlayUpgradeDamageSound()
    {
        soundManager.GetComponent<SoundManager>().PlayUpgradeDamageSound();
    }

    private void PlayErrorSound()
    {
        soundManager.GetComponent<SoundManager>().PlayNoSound();
    }

    private void StopTimerSound()
    {
        soundManager.GetComponent<SoundManager>().StopTimerSound();
    }

    private void PlayTimerSound()
    {
        soundManager.GetComponent<SoundManager>().PlayTimerSound();
    }

    private void PlayMoneySound()
    {
        soundManager.GetComponent<SoundManager>().PlayMoneySound();
    }
}