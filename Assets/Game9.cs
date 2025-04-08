using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game9 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public List<GameObject> infoPanels;
    private GameObject lastTriggeredObject;
    public Transform handTransform;
    public bool isHandFull;
    public GameObject handObject;
    public GameObject ates;
    public int index;
    public List<GameObject> kureler;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level9Tahta" && !isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Pick Up";
            lastTriggeredObject = other.gameObject;

        }
        if (other.gameObject.tag == "Level9Ates" && isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Put";

        }
        //if (other.gameObject.tag == "Level7Finish")
        //{
        //    infoPanels[2].gameObject.SetActive(true);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level9Tahta")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;

        }
        if (other.gameObject.tag == "Level7Kopru")
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
            lastTriggeredObject.transform.DOLocalMove(new Vector3(.07f, -.4f, 1.02f), 1f);
            lastTriggeredObject.transform.DOLocalRotate(new Vector3(0, 36, 0), 1f);
            lastTriggeredObject.transform.DOScale(new Vector3(1.4f, .18f, .5f), 1);
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            isHandFull = true;

            ToplaButton.gameObject.SetActive(false);
        }
        
        if (ToplaText.text == "Put")
        {
            handObject.transform.parent = null;
            handObject.transform.DOScale(Vector3.zero, 1);
            handObject.transform.DORotate(ates.transform.rotation.eulerAngles, 1);
            handObject.transform.DOMove(ates.transform.position, 1).OnComplete(() => {

                handObject = null;
                isHandFull = false;


                ToplaButton.gameObject.SetActive(false);
            
                if (index==2)
                {
                    ates.transform.GetChild(1).gameObject.SetActive(true);
                    ates.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    ates.transform.GetChild(2).gameObject.SetActive(true);
                    kureler[0].transform.DOScale(Vector3.one*3.5f, 6);
                    kureler[1].transform.DOScale(Vector3.one*3.5f, 6);
                    kureler[2].transform.DOScale(Vector3.one*3.5f, 6);
                    kureler[3].transform.DOScale(Vector3.one*3.5f, 6).OnComplete(()=> {
                        infoPanels[3].gameObject.SetActive(true);
                    });

                }
                else
                {
                    infoPanels[index].gameObject.SetActive(true);
                }
                index++;
                
                //kopruParent.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().enabled = false;
                //if (index == 1)
                //{
                //    infoPanels[0].gameObject.SetActive(true);
                //}
                //if (index == 3)
                //{
                //    infoPanels[1].gameObject.SetActive(true);
                //    collider1.enabled = false;
                //    collider2.enabled = false;
                //}


             
            });

            ToplaButton.gameObject.SetActive(false);
            isHandFull = false;

        }
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }


}
