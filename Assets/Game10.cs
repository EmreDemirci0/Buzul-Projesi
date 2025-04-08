using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game10 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public GameObject infoPanel;
 
    private GameObject lastTriggeredObject;
    public Transform handTransform;
    public bool isHandFull;
    public GameObject handObject;
    public GameObject ates;
    public int index;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
        ates.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level10Watering" && !isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Pick Up";
            lastTriggeredObject = other.gameObject;

        }
        if (other.gameObject.tag == "Level10Ates" && isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Water";

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level10Watering")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;

        }
        if (other.gameObject.tag == "Level10Ates")
        {
            ToplaButton.gameObject.SetActive(false);

        }

    }
    public void CollectButton()
    {
        if (ToplaText.text == "Pick Up")
        {
            handObject = lastTriggeredObject;
            lastTriggeredObject.gameObject.transform.parent = handTransform.parent;
            lastTriggeredObject.transform.DOLocalMove(new Vector3(0.424f, -0.474f, 0.982f), 1f);
            lastTriggeredObject.transform.DOLocalRotate(new Vector3(0, -40, 10), 1f);
            lastTriggeredObject.transform.DOScale(new Vector3(1,1,1), 1);
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            lastTriggeredObject.GetComponent<Outline>().enabled = false;
            isHandFull = true;

            ToplaButton.gameObject.SetActive(false);
        }

        if (ToplaText.text == "Water")
        {
            //ates.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
            ParticleSystem ps = ates.transform.GetChild(1).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = ps.main;

            // DoTween ile startSize deðerini 0'a düþür
            handObject.transform.DOLocalRotate(new Vector3(31.815f, -31.83f, -6.655f),1);
            DOTween.To(() => main.startSize.constant, x => main.startSize = x, 0f, 2f).OnComplete(()=> {
                ates.transform.GetChild(2).gameObject.SetActive(false);
                lastTriggeredObject.transform.DOLocalRotate(new Vector3(0, -40, 10), 1f);
                Debug.Log("Bitti");
                infoPanel.gameObject.SetActive(true);
                ToplaButton.gameObject.SetActive(false);
            });
         

        }
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }


}
