using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI stageText;


    void Start()
    {
        stageText.text = "STAGE " + PlayerPrefs.GetInt("Stage");
        StartCoroutine(StartStage());

    }

    
    void Update()
    {
        // Timer
    }

    IEnumerator StartStage() 
    {
        yield return new WaitForSeconds(3f);
        stageText.gameObject.SetActive(false);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() 
    {
        while (true)
        {
            Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(-4.4f, 4.4f), 0f);
            Instantiate(enemy, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
