using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private float maxLevelTimer;
    [SerializeField] private Crossfade crossfade;
    [SerializeField] private SpriteRenderer background1, background2;
    [SerializeField] private Sprite[] backgroundsLevels;
    [SerializeField] private AudioClip[] songs;

    private float levelTimer;
    private bool levelComplete;
    private int currentStage;
    private float spawnTimeLevel;

    private System.Action enemyToSpawn;

    void Start()
    {
        currentStage = PlayerPrefs.GetInt("Stage");
        stageText.text = "STAGE " + currentStage;
        levelTimer = 0f;

        if (currentStage <= 2)
        {
            background1.sprite = backgroundsLevels[0];
            background2.sprite = backgroundsLevels[0];
            GetComponent<AudioSource>().clip = songs[0];
            spawnTimeLevel = 2f;
            enemyToSpawn = StageBasic;
        }
        else if (currentStage <= 4)
        {
            background1.sprite = backgroundsLevels[1];
            background2.sprite = backgroundsLevels[1];
            GetComponent<AudioSource>().clip = songs[1];
            spawnTimeLevel = 1.5f;
            enemyToSpawn = StageIntermediate;
        }
        else if (currentStage <= 6)
        {
            background1.sprite = backgroundsLevels[2];
            background2.sprite = backgroundsLevels[2];
            GetComponent<AudioSource>().clip = songs[2];
            spawnTimeLevel = 1.5f;
            enemyToSpawn = StageHard;
        }
        else 
        {
            background1.sprite = backgroundsLevels[3];
            background2.sprite = backgroundsLevels[3];
            GetComponent<AudioSource>().clip = songs[2];
            spawnTimeLevel = 1.25f;
            enemyToSpawn = StageVeryHard;
        }

        GetComponent<AudioSource>().Play();

        StartCoroutine(StartStage());

    }

    
    void Update()
    {
        levelTimer += Time.deltaTime;

        if (levelTimer >= maxLevelTimer) 
        {
            levelComplete = true;
        }
    }

    IEnumerator StartStage() 
    {
        yield return new WaitForSeconds(3f);
        stageText.gameObject.SetActive(false);
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(SpawnEnemies(spawnTimeLevel));
    }

    IEnumerator SpawnAsteroids()
    {
        while (!levelComplete)
        {
            Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
            float randomAsteroid = Random.Range(0f, 1f);

            if(randomAsteroid > 0.7f)
                Instantiate(enemies[1], randomPosition, Quaternion.identity);
            else
                Instantiate(enemies[0], randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(5f);
        if (GameObject.Find("Player"))
        {
            PlayerPrefs.SetInt("Lifes", GameObject.Find("Player").GetComponent<Player>().Lifes);
            PlayerPrefs.SetInt("Gold", GameObject.Find("Player").GetComponent<Player>().Gold);
            crossfade.LoadScene(2);
        }
    }

    IEnumerator SpawnEnemies(float timeSpawn) 
    {
        while (!levelComplete)
        {
            enemyToSpawn?.Invoke();
            yield return new WaitForSeconds(timeSpawn);
        }
    }

    void StageBasic()
    {
        Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
        Instantiate(enemies[2], randomPosition, Quaternion.identity);
    }

    void StageIntermediate() 
    {
        Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
        float enemyRandom = Random.Range(0f, 1f);
        if(enemyRandom < 0.5f) 
        {
            Instantiate(enemies[2], randomPosition, Quaternion.identity);
        }
        else if (enemyRandom < 0.75f) 
        {
            Instantiate(enemies[3], randomPosition, Quaternion.identity);
        }
        else 
        {
            Instantiate(enemies[4], randomPosition, Quaternion.identity);
        }
    }

    void StageHard() 
    {
        Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
        float enemyRandom = Random.Range(0f, 1f);
        if (enemyRandom < 0.2f)
        {
            Instantiate(enemies[2], randomPosition, Quaternion.identity);
        }
        else if (enemyRandom < 0.4f)
        {
            Instantiate(enemies[3], randomPosition, Quaternion.identity);
        }
        else if (enemyRandom < 0.6f)
        {
            Instantiate(enemies[4], randomPosition, Quaternion.identity);
        }
        else if (enemyRandom < 0.8f)
        {
            Instantiate(enemies[5], randomPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(enemies[6], randomPosition, Quaternion.identity);
        }
    }

    void StageVeryHard() 
    {
        Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
        float enemyRandom = Random.Range(0f, 1f);
        if (enemyRandom < 0.1f)
        {
            Instantiate(enemies[2], randomPosition, Quaternion.identity);
        }
        else if (enemyRandom < 0.2f)
        {
            Instantiate(enemies[3], randomPosition, Quaternion.identity);
        }
        else if (enemyRandom < 0.3f)
        {
            Instantiate(enemies[4], randomPosition, Quaternion.identity);
        }
        else if (enemyRandom < 0.6f)
        {
            Instantiate(enemies[5], randomPosition, Quaternion.identity);
        }
        else if(enemyRandom < 0.9f)
        {
            Instantiate(enemies[6], randomPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(enemies[7], randomPosition, Quaternion.identity);
        }
    }
}