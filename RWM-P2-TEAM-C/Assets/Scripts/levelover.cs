using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class levelover : MonoBehaviour
{
   private float timer;
    private int minutes;
    private int seconds;
    private float hours;
    private bool ended;
    public Text timing;
    public GameObject over;
    // Start is called before the first frame update
    void Start()
    {
        ended = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ended)
        {
            timer += Time.deltaTime;
            displayText();
        }

        //if(Input.GetMouseButtonDown(0))
      
    }
    void displayText()
    {
        minutes = Mathf.FloorToInt(timer / 60.0f);
        seconds = Mathf.FloorToInt(timer - minutes * 60.0f);
        timing.text = "Time Taken: " + minutes + " : " + seconds;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            levelEnded();
        }
    }

    public void levelEnded()
    {
        ended = true;
        StartCoroutine(AnalyticsManager.instance.PostMethod());
        over.SetActive(true);
    }
}
