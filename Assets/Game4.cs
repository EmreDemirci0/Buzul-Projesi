using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game4 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public List<GameObject> infoPanels;
    public int index = 0;
    private GameObject lastTriggeredObject;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level4Trash")
        {
            ToplaButton.gameObject.SetActive(true);
            lastTriggeredObject = other.gameObject;
            ToplaText.text = "Take";

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level4Trash")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }
        
    }

    public void CollectButton()
    {
        Destroy(lastTriggeredObject);
        ToplaButton.gameObject.SetActive(false);
        if (index==1)
        {
            infoPanels[0].gameObject.SetActive(true);
        }
        if (index == 3)
        {
            infoPanels[1].gameObject.SetActive(true);
        }
        if (index == 5)
        {
            infoPanels[2].gameObject.SetActive(true);
        }

        if (index == infoPanels.Count - 1)
        {
            //Debug.Log("Bitti");
        }
        index++;
       
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }
}
