using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Crossfade crossfade;

    void Start()
    {
        PlayerPrefs.SetInt("Lifes", 3);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("x2", 0);
        PlayerPrefs.SetInt("x3", 0);
        PlayerPrefs.SetInt("Stage", 1);
    }

    public void StartGame() 
    {
        crossfade.LoadScene(1);
    }
}
