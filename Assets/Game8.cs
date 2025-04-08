using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game8 : MonoBehaviour
{
    public GameObject penguin;
    public Transform penguinPoint;
    public float detectionRadius = 10f;
    public float moveSpeed = 2f;
    public Transform[] waypoints; // Hareket edilecek noktalar
    private Transform targetWaypoint; // Hedef nokta
    public Animator animator;

    public Slider aclikSlider;
    public Image aclikSliderBG;


    public Button ToplaButton;
    public Button FinishButton;
    public TextMeshProUGUI ToplaText;
    public List<GameObject> infoPanels;
    public bool isHandFull;
    private GameObject lastTriggeredObject;
    public GameObject handObject;
    public Transform handTransform;

    int index;  
    void Start()
    {
        aclikSlider.maxValue = 100;
        aclikSlider.value = 10;
        aclikSliderBG.color = Color.red;
        SetNewDestination(); // Ýlk hedefi belirle,
        ToplaButton.onClick.AddListener(CollectButton);
        FinishButton.onClick.AddListener(FinishButtons);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level8Meat" && !isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Pick Up";
            lastTriggeredObject = other.gameObject;

        }
        if (other.gameObject.tag == "Level8Penguin" && isHandFull)
        {
            ToplaButton.gameObject.SetActive(true);
            ToplaText.text = "Feed";
           
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level8Meat")
        {
            ToplaButton.gameObject.SetActive(false);
            lastTriggeredObject = null;
        }
        if (other.gameObject.tag == "Level8Penguin")
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
            lastTriggeredObject.transform.DOLocalMove(new Vector3(0.517f, -0.187f, 1.247f), 1f);
            lastTriggeredObject.transform.DOLocalRotate(new Vector3(0f, 36, 0), 1f);
            lastTriggeredObject.transform.DOScale(new Vector3(.7f,.7f,.7f), 1);
            lastTriggeredObject.GetComponent<Collider>().enabled = false;
            isHandFull = true;
            lastTriggeredObject.GetComponent<Outline>().enabled = false;
            ToplaButton.gameObject.SetActive(false);
        }
        if (ToplaText.text == "Feed")
        {
            handObject.transform.parent = null;
            handObject.transform.DOScale(Vector3.zero, 1);
            handObject.transform.DORotate(penguinPoint.transform.rotation.eulerAngles, 1);
            float targetValue = Mathf.Clamp(aclikSlider.value + 30, 0, aclikSlider.maxValue);

            aclikSlider.DOValue(targetValue, 1).OnUpdate(() => {
                if (aclikSlider.value < 20)
                {
                    aclikSliderBG.color = Color.red;
                }
                else if (aclikSlider.value >= 20 && aclikSlider.value < 60)
                {
                    aclikSliderBG.color = Color.yellow;
                }
                else if (aclikSlider.value >= 60)
                {
                    aclikSliderBG.color = Color.green;
                }
            });
            handObject.transform.DOMove(penguinPoint.transform.transform.position, 1).OnComplete(() => {
                
                handObject = null;
                isHandFull = false;
                

                ToplaButton.gameObject.SetActive(false);
                infoPanels[index].gameObject.SetActive(true);
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

            //ToplaButton.gameObject.SetActive(false);
            //isHandFull = false;

        }
    }
    public void FinishButtons()
    {
        Debug.Log("Bitti");
        SceneManager.LoadScene("Menu");
    }


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(penguin.transform.position, this.transform.position);

        if (distanceToPlayer > detectionRadius)
        {
            MoveToWaypoint();
            animator.SetBool("isWalking", true);
        }
        else
        {
            LookAtPlayer();
            animator.SetBool("isWalking", false);
        }
    }

    void MoveToWaypoint()
    {
        if (targetWaypoint == null) return;

        // Hedef yönünü hesapla, Y eksenini sabitle
        Vector3 direction = (targetWaypoint.position - penguin.transform.position).normalized;
        direction.y = 0; // Y ekseninde dönmesini önler

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Y eksenine 90 derece ekle
            targetRotation *= Quaternion.Euler(0, 90f, 0);

            // Rotasyonu yumuþak þekilde uygula
            penguin.transform.rotation = Quaternion.Slerp(penguin.transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        // Hareket ettir
        penguin.transform.position = Vector3.MoveTowards(penguin.transform.position, targetWaypoint.position, 2f * Time.deltaTime);

        // Hedefe ulaþtýysa yeni nokta belirle
        if (Vector3.Distance(penguin.transform.position, targetWaypoint.position) < 0.5f)
        {
            SetNewDestination();
        }
    }
    public void LookAtPlayer()
    {

        Vector3 direction = penguin.transform.position - transform.position; // Oyuncuya doðru yönü hesapla
        direction.y = 0; // Y eksenindeki farký yok sayarak sadece yatay düzlemde bakmasýný saðla

        if (direction != Vector3.zero) // Geçerli bir yön varsa
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction); // Normal bakýþ rotasyonu
            penguin.transform.rotation = lookRotation * Quaternion.Euler(0, -90, 0); // +90 derece ekleyerek döndür
        }
    }

    private int currentWaypointIndex = 0;

    void SetNewDestination()
    {
        if (waypoints.Length == 0) return;

        // Bir sonraki waypoint'e geç
        targetWaypoint = waypoints[currentWaypointIndex];

        // Indexi artýr, eðer sona ulaþýldýysa baþa dön
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

}
