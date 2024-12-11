using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{

    public void LoadScene(int index) 
    {
        StartCoroutine(LoadSceneCrossFade(index));
    }

    IEnumerator LoadSceneCrossFade(int index)
    {
        GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
