using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game5 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public List<GameObject> infoPanels;
    public int index = 0;
    private GameObject lastTriggeredObject;
    public Transform handTransform;
    public bool isHandFull;
    public GameObject handObject;

    public int plasticCount;
    public int paperCount;
    public int glassCount;

    public Outline plasticBinOutline;
    public Outline paperBinOutline;
    public Outline glassBinOutline;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    private void Update()
    {
        if (handObject)
        {
            if (handObject.gameObject.tag=="Level5Plastic")
            {
                plasticBinOutline.enabled = true;
                paperBinOutline.enabled = false;
                glassBinOutline.enabled = false;
            }
            if (handObject.gameObject.tag == "Level5Paper")
            {
                plasticBinOutline.enabled = false;
                paperBinOutline.enabled = true;
                glassBinOutline.enabled = false;
            }
            if (handObject.gameObject.tag == "Level5Glass")
            {
                plasticBinOutline.enabled = false;
                paperBinOutline.enabled = false;
                glassBinOutline.enabled = true;
            }
        }
        else
        {
            plasticBinOutline.enabled = false;
            paperBinOutline.enabled = false;
            glassBinOutline.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level5Plastic" && !isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Pick Up";
            lastTriggeredObject = other.gameObject;

        }
        if (other.gameObject.tag == "Level5PlasticBin" && isHandFull && handObject.transform.tag == "Level5Plastic")
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Recycle Plastic";
            lastTriggeredObject = other.gameObject;
        }


        if (other.gameObject.tag == "Level5Paper" && !isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Pick Up";
            lastTriggeredObject = other.gameObject;

        }
        if (other.gameObject.tag == "Level5PaperBin" && isHandFull && handObject.transform.tag == "Level5Paper")
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Recycle Paper";
            lastTriggeredObject = other.gameObject;
        }


        if (other.gameObject.tag == "Level5Glass" && !isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Pick Up";
            lastTriggeredObject = other.gameObject;

        }
        if (other.gameObject.tag == "Level5GlassBin" && isHandFull && handObject.transform.tag == "Level5Glass")
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Recycle Glass";
            lastTriggeredObject = other.gameObject;
        }




    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level5Plastic")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }
        if (other.gameObject.tag == "Level5PlasticBin" && isHandFull && handObject.transform.tag == "Level5Plastic")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }

        if (other.gameObject.tag == "Level5Paper")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }
        if (other.gameObject.tag == "Level5PaperBin" && isHandFull && handObject.transform.tag == "Level5Paper")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }

        if (other.gameObject.tag == "Level5Glass")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }
        if (other.gameObject.tag == "Level5GlassBin" && isHandFull && handObject.transform.tag == "Level5Glass")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }


    }
    public void CollectButton()
    {
        //Plastic
        if (lastTriggeredObject && lastTriggeredObject.gameObject.tag == "Level5Plastic")
        {
            lastTriggeredObject.transform.parent = this.handTransform.transform.parent;
            lastTriggeredObject.transform.DOLocalMove(new Vector3(0.221f, -0.271f, .73f), 1f);
            lastTriggeredObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 1f);
            lastTriggeredObject.transform.DOScale(new Vector3(1, 1, 1), 1f);
            ToplaButton.gameObject.SetActive(false);
            isHandFull = true;
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            if (lastTriggeredObject.TryGetComponent<Outline>(out Outline o))
                o.enabled = false;
            handObject = lastTriggeredObject;
            lastTriggeredObject = null;
        }
        else if(isHandFull && handObject.transform.tag == "Level5Plastic" && lastTriggeredObject.gameObject.tag== "Level5PlasticBin")
        {
            Debug.Log("ÇÖPE");
            
            ToplaButton.gameObject.SetActive(false);
            isHandFull = false;
            handObject.transform.SetParent(null);

            handObject.transform.DOMove(new Vector3(47f,-6.777f,-97.2f),1f);
            handObject.transform.DOScale(new Vector3(0, 0, 0), 1f);
            handObject.transform.DORotate(new Vector3(20f,40,20),1f).OnComplete(()=> {
                handObject.gameObject.SetActive(false);
                handObject = null;
                isHandFull = false;
                plasticCount++;
                if (plasticCount==2)
                {
                    infoPanels[index].gameObject.SetActive(true);
                    index++;
                }
            });
        }

        //Paper
        if (lastTriggeredObject && lastTriggeredObject.gameObject.tag == "Level5Paper")
        {
            lastTriggeredObject.transform.parent = this.handTransform.transform.parent;
            lastTriggeredObject.transform.DOLocalMove(new Vector3(0.221f, -0.271f, .73f), 1f);
            lastTriggeredObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 1f);
            lastTriggeredObject.transform.DOScale(new Vector3(1, 1, 1), 1f);
            ToplaButton.gameObject.SetActive(false);
            isHandFull = true;
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            if (lastTriggeredObject.TryGetComponent<Outline>(out Outline o))
                o.enabled = false;
            handObject = lastTriggeredObject;
            lastTriggeredObject = null;
        }
        else if (isHandFull && handObject.transform.tag == "Level5Paper" && lastTriggeredObject.gameObject.tag == "Level5PaperBin")
        {
            Debug.Log("ÇÖPE");

            ToplaButton.gameObject.SetActive(false);
            isHandFull = false;
            handObject.transform.SetParent(null);

            handObject.transform.DOMove(new Vector3(47.5f, -6.777f, -96.324f), 1f);
            handObject.transform.DOScale(new Vector3(0, 0, 0), 1f);
            handObject.transform.DORotate(new Vector3(0f, 0, 0), 1f).OnComplete(() => {
                handObject.gameObject.SetActive(false);
                handObject = null;
                isHandFull = false;
                paperCount++;
                if (paperCount == 2)
                {
                    infoPanels[index].gameObject.SetActive(true);
                    index++;
                }
            });
        }

        //Glass
        if (lastTriggeredObject && lastTriggeredObject.gameObject.tag == "Level5Glass")
        {
            lastTriggeredObject.transform.parent = this.handTransform.transform.parent;
            lastTriggeredObject.transform.DOLocalMove(new Vector3(0.221f, -0.271f, .73f), 1f);
            lastTriggeredObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 1f);
            lastTriggeredObject.transform.DOScale(new Vector3(1, 1, 1), 1f);
            ToplaButton.gameObject.SetActive(false);
            isHandFull = true;
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            if (lastTriggeredObject.TryGetComponent<Outline>(out Outline o))
                o.enabled = false;
            handObject = lastTriggeredObject;
            lastTriggeredObject = null;
        }
        else if (isHandFull && handObject.transform.tag == "Level5Glass" && lastTriggeredObject.gameObject.tag == "Level5GlassBin")
        {
            Debug.Log("ÇÖPE");

            ToplaButton.gameObject.SetActive(false);
            isHandFull = false;
            handObject.transform.SetParent(null);

            handObject.transform.DOMove(new Vector3(48.168f, -6.77f, -95.631f), 1f);
            handObject.transform.DOScale(new Vector3(0, 0, 0), 1f);
            handObject.transform.DORotate(new Vector3(0f, 0, 0), 1f).OnComplete(() => {
                handObject.gameObject.SetActive(false);
                handObject = null;
                isHandFull = false;
                glassCount++;
                if (glassCount == 2)
                {
                    infoPanels[index].gameObject.SetActive(true);
                    index++;
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
