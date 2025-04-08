using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Game2 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    private GameObject lastTriggeredObject;
    public List<GameObject> infoPanels;
    public Transform handTransform;
    public/**/bool isHandFull;

    public Material panelGercekMaterial;
    public GameObject selectedSlot;

    public GameObject slot1;
    public GameObject slot2;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    public int index = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level2Panel")
        {
            ToplaButton.gameObject.SetActive(true);
            lastTriggeredObject = other.gameObject;
            ToplaText.text = "Pick Up";

        }
        if (other.gameObject.tag == "Level2Slot" && isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);          
            ToplaText.text = "Place";
            selectedSlot = other.gameObject;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level2Panel")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }
        if (other.gameObject.tag == "Level2Slot")
        {
            ToplaButton.gameObject.SetActive(false);
            selectedSlot = null;
        }
    }

    public void CollectButton()
    {
        if (ToplaText.text== "Pick Up")
        {
            lastTriggeredObject.transform.parent = this.handTransform.transform;
            lastTriggeredObject.transform.localPosition = Vector3.zero;
            lastTriggeredObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            lastTriggeredObject.transform.localScale = Vector3.one;
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            ToplaButton.gameObject.SetActive(false);
            isHandFull = true;
            lastTriggeredObject.GetComponent<Outline>().enabled = false;
            slot1.GetComponent<Outline>().enabled = true;
            slot2.GetComponent<Outline>().enabled = true;


        }
        else if (ToplaText.text == "Place")
        {
            Debug.Log("Yerþeltirildi");
            Destroy(handTransform.transform.GetChild(0).gameObject);
            selectedSlot.GetComponent<MeshRenderer>().material = panelGercekMaterial;
            selectedSlot.GetComponent<Collider>().enabled = false;
            ToplaButton.gameObject.SetActive(false);
            isHandFull = false;
            slot1.GetComponent<Outline>().enabled = false;
            slot2.GetComponent<Outline>().enabled = false;
            infoPanels[index].gameObject.SetActive(true);
            index++;
        }  
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }
}
