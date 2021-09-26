using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("PlayerData")]
    public float score = 0;
    public float timeRemaining = 120;

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
    public GameObject GameUI;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        TogglePause();

        // Set runner spawn time to now
        nextRunnerSpawnTime = Time.time;


    }

    // Update is called once per frame
    void Update()
    {

        //If we are in the game mode
        if (!isPaused)
        {
            IncreaseLeavesOverTime();

            //TODO: Spawn runners
            SpawnRunners();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused) { Time.timeScale = 0.0f; }
        else { Time.timeScale = 1.0f; }

        // Toggle on the pause menu
        MainMenu.SetActive(isPaused);
        GameUI.SetActive(!isPaused);
    }

    public void ShowMenu ()
    {

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
