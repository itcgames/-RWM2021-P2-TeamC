using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_death : MonoBehaviour
{
    public Animator m_amiator;
    public Boss m_boss;
    public int gone;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        m_amiator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    

    public void death_start(bool h)
    {
        if(h)
        {
            gone = 1;
            this.gameObject.SetActive(true);
            death();
            print("works"+ gone);
        }
    }
    void death()
    {
        print("OVERHWRE");
        m_amiator.SetBool("death", true);
        

    }
}
