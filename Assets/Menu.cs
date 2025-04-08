using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SelectedScene
{
    Game1,
    Game2,
    Game3,
    Game4,
    Game5,
    Game6,
    Game7,
    Game8,
    Game9,
    Game10,
}
public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    public SelectedScene selectedScene;
    //public int selectedScene;
    [SerializeField] List<Scenem> scene;

    private void Start()
    {
        // Enum deðerini int'e dönüþtürüp karþýlaþtýrýyoruz
        if ((int)selectedScene < 0 || (int)selectedScene >= scene.Count)
        {
            Debug.LogError("Geçersiz sahne seçimi!");
            return;
        }

        titleText.text = scene[(int)selectedScene].title;
        if (string.IsNullOrEmpty(scene[(int)selectedScene].title))
        {
            titleText.text = "Baþlýk Bulunamadý";
        }
    }

    public void StartGame()
    {
        // Enum deðerini int'e dönüþtürüp karþýlaþtýrýyoruz
        if ((int)selectedScene < 0 || (int)selectedScene >= scene.Count)
        {
            Debug.LogError("Geçersiz sahne seçimi!");
            return;
        }

        SceneManager.LoadScene(scene[(int)selectedScene].sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
[System.Serializable]
public class Scenem
{
    public string sceneName;
    public string title;
   

}
