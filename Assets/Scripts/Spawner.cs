using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private float maxLevelTimer;
    [SerializeField] private Crossfade crossfade;

    private float levelTimer;
    private bool levelComplete;


    void Start()
    {
        stageText.text = "STAGE " + PlayerPrefs.GetInt("Stage");
        levelTimer = 0f;
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
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() 
    {
        while (!levelComplete)
        {
            Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
            Instantiate(enemy, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(5f);
        if (GameObject.Find("Player")) 
        {
            crossfade.LoadScene(2);
        }

    }
}
