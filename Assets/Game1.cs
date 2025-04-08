using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game1 : MonoBehaviour
{
    public Button ToplaButton;
    public TextMeshProUGUI ToplaText;
    public Button FinishButton;
    private GameObject lastTriggeredObject;
    public List<GameObject> infoPanels;
    public int index=0;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Level1Orb")
        {
            ToplaButton.gameObject.SetActive(true);
            lastTriggeredObject = other.gameObject;
            ToplaText.text = "Take Orb";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level1Orb")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }
    }
    public void CollectButton()
    {
        lastTriggeredObject.gameObject.SetActive(false);
        lastTriggeredObject = null;
        ToplaButton.gameObject.SetActive(false);
        infoPanels[index].gameObject.SetActive(true);
        index++;
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }
}
