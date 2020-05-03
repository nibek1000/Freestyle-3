using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour {
    public Sprite[] imagesToRoll;
    public Image nowImage;
    public Image nextImage;
    private int next;
    private int now;
    public Slider sliderTime;
    public Text changenarrator;
    private int changeIn;
    public GameObject changeGO;
    public GameObject fail;
    public Text scoreText;
    private int score;
    public Text EndTimer;
    private int tillEnd;

    public GameObject finished;
    public Text totalScore;
	// Use this for initialization
	void Start () {
        StartCoroutine(firstRoll());
        Debug.Log("Game started");

        if(PlayerPrefs.GetInt("rd") == 0)
        {
            tillEnd = 999999999;
        }
        else
        {
            tillEnd = PlayerPrefs.GetInt("rd");
        }


        if (PlayerPrefs.GetInt("nd") == 0)
        {
            changeIn = 999999999;
        }
        else
        {
            changeIn = PlayerPrefs.GetInt("nd");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.GetInt("rd") == 0)
        {
            EndTimer.text = "Till story ending: " + "∞";
        }
        else
        {
            EndTimer.text = "Till story ending: " + tillEnd;
        }


        if (PlayerPrefs.GetInt("nd") == 0)
        {
            changenarrator.text = "Change narrator in: " + "∞";
        }
        else
        {
            changenarrator.text = "Change narrator in: " + changeIn;
        }


	}
    public void SomeoneFailed()
    {
        StopAllCoroutines();
        fail.SetActive(true);
        scoreText.text = "You made: " + score + " in row!";
        StartCoroutine(finish());
    }

    void FinishedGame()
    {
        StopAllCoroutines();
        finished.SetActive(true);
        totalScore.text = "You made: " + score + " in row!";
        StartCoroutine(finish());
    }

    IEnumerator finish()
    {
        yield return new WaitForSeconds(5);
        Application.LoadLevel(0);
    }


    IEnumerator firstRoll()
    {

        Debug.Log(PlayerPrefs.GetFloat("st") / 100);
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("st") / 100);
        sliderTime.value = sliderTime.value - 0.01f;
        if (sliderTime.value == 0)
        {
            now = Random.Range(0, 249);
            next = Random.Range(0, 249);
            Apply();
            Debug.Log("first roll ended");
            sliderTime.value = 1;
            StartCoroutine(roll());
        }
        else
        {
            StartCoroutine(firstRoll());
        }
        
    }

    IEnumerator roll()
    {
        Debug.Log("roll started");
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("st") / 100);
        sliderTime.value = sliderTime.value - 0.01f;
        if (sliderTime.value == 0)
        {


            if (changeIn == 0)
            {
                sliderTime.value = 1;
                StartCoroutine(changei());

            }
            else
            {
                StartCoroutine(nochange());

            }

        }
        else
        {
            StartCoroutine(roll());
        }
    }


    IEnumerator nochange()
    {
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("st") / 100);
        sliderTime.value = sliderTime.value - 0.01f;
        if (sliderTime.value == 0)
        {
            changeIn--;
            tillEnd--;
            changeGO.SetActive(false);
            now = next;
            score++;
            next = Random.Range(0, 249);
            Apply();
            Debug.Log("DONE!");
            sliderTime.value = 1;
            StartCoroutine(roll());
            if(tillEnd == 0)
            {
                FinishedGame();
            }

        }
        else
        {
            StartCoroutine(nochange());
        }
    }


    IEnumerator changei()
    {
        changeGO.SetActive(true);
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("st") / 100);
        sliderTime.value = sliderTime.value - 0.01f;
        if (sliderTime.value == 0)
        {
            changeIn = PlayerPrefs.GetInt("nd");
            changeGO.SetActive(false);
            now = next;
            score++;
            next = Random.Range(0, 249);
            Apply();
            Debug.Log("DONE!");
            sliderTime.value = 1;
            StartCoroutine(roll());
            if (tillEnd == 0)
            {
                FinishedGame();
            }
        }
        else
        {
            StartCoroutine(changei());
        }
    }


    void Apply()
    {
        nowImage.GetComponent<Image>().sprite = imagesToRoll[now];
        nextImage.GetComponent<Image>().sprite = imagesToRoll[next];
    }
}
