using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Text GoldValue;
    [SerializeField] private Text MetallValue;
    [SerializeField] private Text WoodValue;
    [SerializeField] private Text FishValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowValues();
    }

    public void SceneRestart()
    {
        string _currentScene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(_currentScene);
        SceneManager.LoadScene(_currentScene, LoadSceneMode.Single);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowValues()
    {
        GoldValue.text = "Gold:" + StructCurrency.Gold.ToString();
        MetallValue.text = "Metall:" +StructCurrency.Metall.ToString();
        WoodValue.text = "Wood:" + StructCurrency.Wood.ToString();
        FishValue.text = "Fish:" + StructCurrency.Fish.ToString();
    }
}
