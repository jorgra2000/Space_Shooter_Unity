using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Crossfade crossfade;
    public void StartGame() 
    {
        crossfade.LoadScene(1);
    }
}
