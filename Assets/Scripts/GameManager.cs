using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("PlayerData")]
    public float score = 0;
    public float gameTime = 90;
    public float comboValue = 0;
    public float maxTimeBetweenCombos = 0.5f;
    public float comboCountdown = 0.0f;
    public float maxCombo = 8;
    private float timeRemaining;

    [Header("Leaves")]
    public float numLeaves = 0;
    public float leavesPerSecond = 5;
    public float maxLeaves = 10;
    [Header("Runners")]
    public List<Runner> runnerPrefabs;
    public List<Runner> runners;
    public List<RunnerSpawnPoint> runnerSpawners;
    public float minRunnerSpawnTime = 0.5f;
    public float maxRunnerSpawnTime = 7.0f;
    public int minRunnersPerSpawn = 1;
    public int maxRunnersPerSpawn = 3;
    private float nextRunnerSpawnTime;

    [Header("Game Data")]
    public bool isPaused = false;

    [Header("UI Objects")]
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject GameUI;
    public GameObject GameOverUI;
    public Image leafCounter;
    public Text scoreTextbox;
    public Text timerTextbox;
    public Image timerImage;
    public GameObject floatingScorePrefab;
    public List<Color> TimerLeafColors;
    public Text FinalScoreBox;

    [Header("Sounds")]
    public List<AudioClip> leafDropSounds;
    public List<AudioClip> leafPopSounds;
    public List<AudioClip> stumbleSounds;
    public List<AudioClip> fallSounds;
    public AudioSource musicPlayer;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        TogglePause();
        GameOverUI.SetActive(false);
        GameUI.SetActive(false);
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);

        // Set runner spawn time to now
        nextRunnerSpawnTime = Time.time;

        // start with game time
        timeRemaining = gameTime;

        // Start Music
        musicPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //If we are in the game mode
        if (!isPaused)
        {
            // Get leaves
            IncreaseLeavesOverTime();

            // reduce timers
            timeRemaining -= Time.deltaTime;
            comboCountdown -= Time.deltaTime;
            comboCountdown = Mathf.Max(0.0f, comboCountdown);           

            // Check for end game
            if (timeRemaining <= 0) {
                GameOver();
            }

            //Spawn runners
            SpawnRunners();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        //Update UI
        UpdateUI();


    }

    public void PlayRandomSoundFromList(List<AudioClip> soundList, Vector3 position, float volume = 1.0f)
    {
        
        if (position == null) {
            position = Camera.main.transform.position;
        }
        if (soundList != null && soundList.Count > 0) {
            AudioSource.PlayClipAtPoint(soundList[Random.Range(0, soundList.Count)], position, volume);
        }
    }

    public void GameOver()
    {
        TogglePause();
        MainMenu.SetActive(false);
        GameUI.SetActive(false);
        PauseMenu.SetActive(false);
        GameOverUI.SetActive(true);
        FinalScoreBox.text = "Final Score:\n" + score;
        musicPlayer.Stop();
        musicPlayer.Play();
    }

    public void  UpdateUI()
    {

        leafCounter.fillAmount = numLeaves / maxLeaves;
        timerImage.fillAmount = timeRemaining / gameTime;
        Debug.Log(""+ Mathf.FloorToInt((timeRemaining / gameTime) * TimerLeafColors.Count) + "/" + (TimerLeafColors.Count - 1));
        // timerImage.color = TimerLeafColors[Mathf.FloorToInt( (timeRemaining/gameTime) * TimerLeafColors.Count) ];
        timerTextbox.text = ""+Mathf.Ceil(timeRemaining);
        scoreTextbox.text = "Points: "+score;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused) {
            Time.timeScale = 0.0f;
            musicPlayer.Pause();
        }
        else {
            Time.timeScale = 1.0f;
            musicPlayer.UnPause();
        }

        // Toggle on the pause menu
        PauseMenu.SetActive(isPaused);
        GameUI.SetActive(!isPaused);
    }

    public void StartGame ()
    {
        score = 0;
        musicPlayer.Play();
        isPaused = false;
        MainMenu.SetActive(false);
        GameUI.SetActive(true);
        GameOverUI.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);        
    }

    public void AddScore(Runner runner)
    {
        if (runner != null) {
            float scoreToAdd = runner.scoreValue;

            // Multiply for Combo
            if (comboCountdown > 0.0f) {
                comboValue += 1;
                comboValue = Mathf.Min(comboValue, maxCombo);
            } else {
                comboValue = 1;
            }
            scoreToAdd *= comboValue;

            // Add to score
            score += scoreToAdd;

            // Update text
            GameObject floatingScore = Instantiate(floatingScorePrefab, runner.transform.position + Vector3.up, Quaternion.identity) as GameObject;

            Text textBox = floatingScore.GetComponentInChildren<Text>();

            if (runner.scoreValue >= 0) { textBox.text = "+"; }
            else {
                textBox.text = "";
            }
            textBox.text += scoreToAdd;

            if (comboValue > 1) {
                textBox.text += "\nCOMBO x" + comboValue;                   
                textBox.color = Color.yellow;
            }

            // Set timer for next combo
            comboCountdown = maxTimeBetweenCombos;
        }



    }

    public void Quit()
    {
        Application.Quit();

    }

    public void IncreaseLeavesOverTime()
    {
        if (numLeaves < maxLeaves) {
            numLeaves += leavesPerSecond * Time.deltaTime;
        }
    }

    public void SpawnRunners()
    {
        // If it is time to spawn a runner
        if (Time.time >= nextRunnerSpawnTime)
        {
            int numSpawners = Random.Range(minRunnersPerSpawn, maxRunnersPerSpawn);

            for (int i = 0; i<numSpawners; i++)
            {
                Transform randomSpawner = runnerSpawners[Random.Range(0, runnerSpawners.Count)].transform;
                Runner newRunner = Instantiate(runnerPrefabs[Random.Range(0, runnerPrefabs.Count)], randomSpawner.position, randomSpawner.rotation) as Runner;
                //newRunner.moveSpeed = Random.Range(1.0f, 6.0f);
            }

            SetNextRunnerSpawnTime();
        }
    }

    public void SetNextRunnerSpawnTime()
    {
        nextRunnerSpawnTime = Time.time + Random.Range(minRunnerSpawnTime, maxRunnerSpawnTime);
    }




}
