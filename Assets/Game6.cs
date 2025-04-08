using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game6 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public GameObject infoPanel;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level6Penguin")
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Save";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level6Penguin")
        {
            ToplaButton.gameObject.SetActive(false);
        }
    }
    public void CollectButton()
    {
        infoPanel.gameObject.SetActive(true);
        ToplaButton.gameObject.SetActive(false);
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }
}
