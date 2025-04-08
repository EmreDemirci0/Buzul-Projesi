using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game7 : MonoBehaviour
{
    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public List<GameObject> infoPanels;
    public Collider collider1;
    public Collider collider2;
    private GameObject lastTriggeredObject;
    public Transform handTransform;
    public bool isHandFull;
    public GameObject handObject;

    public GameObject kopruParent;
    public int index;
    private void Start()
    {
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level7Wood" && !isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Pick Up";
            lastTriggeredObject = other.gameObject;

        }
        if (other.gameObject.tag == "Level7Kopru" && isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Build";
          
        }
        if (other.gameObject.tag == "Level7Finish" )
        {
            infoPanels[1].gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level7Wood"  )
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;

        }
        if (other.gameObject.tag == "Level7Kopru" )
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
            lastTriggeredObject.transform.DOLocalMove(new Vector3(.07f,-.4f,1.02f), 1f);
            lastTriggeredObject.transform.DOLocalRotate(new Vector3(0,36,0), 1f);
            lastTriggeredObject.transform.DOScale(new Vector3(1.4f,.18f,.5f),1);
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            isHandFull = true;

            ToplaButton.gameObject.SetActive(false);
        }
        if (ToplaText.text=="Build")
        {
            handObject.transform.parent = null;
            handObject.transform.DOScale(kopruParent.transform.GetChild(index).transform.localScale, 1);
            handObject.transform.DORotate(kopruParent.transform.GetChild(index).transform.rotation.eulerAngles, 1);
            handObject.transform.DOMove(kopruParent.transform.GetChild(index).transform.position, 1).OnComplete(()=> {

                kopruParent.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().enabled=false;
                if (index==1)
                {
                    //infoPanels[0].gameObject.SetActive(true);
                }
                if (index == 3)
                {
                    infoPanels[0].gameObject.SetActive(true);
                    collider1.enabled = false;
                    collider2.enabled = false;
                }
                

                index++;
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
