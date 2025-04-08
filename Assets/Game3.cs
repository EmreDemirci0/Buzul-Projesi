using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game3 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public GameObject infoPanel;
    public GameObject sulamaAleti;
    public GameObject agac;
    public Transform handTransform;
    bool isHandFull;
    int sulamaIndex;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level3Watering")
        {
            ToplaButton.gameObject.SetActive(true);
            //lastTriggeredObject = other.gameObject;
            ToplaText.text = "Take";

        }
        if (other.gameObject.tag == "Level3Tree" && isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Water";
            //selectedSlot = other.gameObject;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level3Watering")
        {
            ToplaButton.gameObject.SetActive(false);
            //lastTriggeredObject = null;
        }
        if (other.gameObject.tag == "Level3Tree")
        {
            ToplaButton.gameObject.SetActive(false);
            //selectedSlot = null;
        }
    }

    public void CollectButton()
    {
        if (ToplaText.text == "Take")
        {
            sulamaAleti.transform.parent = this.handTransform.transform.parent;
            sulamaAleti.transform.DOLocalMove(new Vector3(0.12f, -0.47f, .71f), 1f);
            sulamaAleti.transform.DOLocalRotate(new Vector3(0, -45, 0), 1f);
            sulamaAleti.transform.DOScale(new Vector3(.8f, .8f, .8f), 1f);
            ToplaButton.gameObject.SetActive(false);
            isHandFull = true;
            sulamaAleti.GetComponent<Collider>().enabled = false;
            sulamaAleti.GetComponent<Outline>().enabled = false;
            agac.GetComponent<Outline>().enabled = true;
            //sulamaAleti.transform.DOLocalScale(new Vector3(0,-45,0),1f);
            //lastTriggeredObject.transform.parent = this.handTransform.transform;
            //lastTriggeredObject.transform.localPosition = Vector3.zero;
            //lastTriggeredObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //lastTriggeredObject.transform.localScale = Vector3.one;


        }
        else if (ToplaText.text == "Water")
        {
            ToplaButton.gameObject.SetActive(false);

            agac.transform.DOScaleY(agac.transform.localScale.y + 0.1f, 1f) // 1 saniyede büyüt
                .OnComplete(() =>
                {
                    // Ýþlem tamamlandýðýnda butonu tekrar aktif hale getir
                    ToplaButton.gameObject.SetActive(true);
                    Debug.Log("Aðaç büyütme iþlemi tamamlandý.");
                    sulamaIndex++;
                    if (sulamaIndex>=3)
                    {
                        infoPanel.gameObject.SetActive(true);
                        ToplaButton.gameObject.SetActive(false);
                    }
                });

        }
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }

}
