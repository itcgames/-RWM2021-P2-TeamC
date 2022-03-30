using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableboss : MonoBehaviour
{
    public Boss m_boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_boss.play = true;

        }
    }
}
