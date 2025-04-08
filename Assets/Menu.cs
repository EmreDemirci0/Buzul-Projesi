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
        // Enum de�erini int'e d�n��t�r�p kar��la�t�r�yoruz
        if ((int)selectedScene < 0 || (int)selectedScene >= scene.Count)
        {
            Debug.LogError("Ge�ersiz sahne se�imi!");
            return;
        }

        titleText.text = scene[(int)selectedScene].title;
        if (string.IsNullOrEmpty(scene[(int)selectedScene].title))
        {
            titleText.text = "Ba�l�k Bulunamad�";
        }
    }

    public void StartGame()
    {
        // Enum de�erini int'e d�n��t�r�p kar��la�t�r�yoruz
        if ((int)selectedScene < 0 || (int)selectedScene >= scene.Count)
        {
            Debug.LogError("Ge�ersiz sahne se�imi!");
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
