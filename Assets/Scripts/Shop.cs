using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Crossfade crossfade;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Button lifesButton;
    [SerializeField] private Button x2Button;
    [SerializeField] private Button x3Button;

    void Start()
    {
        lifesText.text = PlayerPrefs.GetInt("Lifes").ToString();
        goldText.text = PlayerPrefs.GetInt("Gold").ToString();

        UpdateGUI();
    }

    public void NextLevel() 
    {
        PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
        crossfade.LoadScene(1);
    }

    public void BuyLife() 
    {
        if (PlayerPrefs.GetInt("Gold") >= 50) 
        {
            PlayerPrefs.SetInt("Lifes", PlayerPrefs.GetInt("Lifes") + 1);
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - 50);
            UpdateGUI();
        }

    }

    public void Buyx2() 
    {
        if (PlayerPrefs.GetInt("Gold") >= 100) 
        {
            PlayerPrefs.SetInt("x2", 1);
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - 100);
            UpdateGUI();
        }

    }

    public void Buyx3()
    {
        if (PlayerPrefs.GetInt("Gold") >= 150) 
        {
            PlayerPrefs.SetInt("x3", 1);
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - 150);
            UpdateGUI();
        }
    }

    void UpdateGUI() 
    {
        lifesText.text = PlayerPrefs.GetInt("Lifes").ToString();
        goldText.text = PlayerPrefs.GetInt("Gold").ToString();
        if(PlayerPrefs.GetInt("Lifes") >= 5) 
        {
            lifesButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("x2") == 1)
        {
            x2Button.interactable = false;
        }

        if (PlayerPrefs.GetInt("x3") == 1)
        {
            x3Button.interactable = false;
        }
    }
}
