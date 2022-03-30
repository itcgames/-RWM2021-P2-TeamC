using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    //buttoms
    public SpriteRenderer D;
    public SpriteRenderer W;
    public SpriteRenderer A;
    public SpriteRenderer P;
    public SpriteRenderer space;

    public Sprite[] PS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))//once A is pressed
        {
            A.sprite = PS[1];
        }
        if (Input.GetKeyUp(KeyCode.A))//once A is unpressed
        {
            A.sprite = PS[0];

        }
        if (Input.GetKeyDown(KeyCode.P))//once p is pressed
        {
            P.sprite = PS[5];
        }
        if (Input.GetKeyUp(KeyCode.P))//once p is unpressed
        {
            P.sprite = PS[4];

        }
        if (Input.GetKeyDown(KeyCode.D))//once D is pressed
        {
            D.sprite = PS[3];
        }
        if (Input.GetKeyUp(KeyCode.D))//once D is unpressed
        {
            D.sprite = PS[2];

        }
        if (Input.GetKeyDown(KeyCode.W))//once W is pressed
        {
            W.sprite = PS[8];
        }
        if (Input.GetKeyUp(KeyCode.W))//once W is unpressed
        {
           W.sprite = PS[7];

        }
        if (Input.GetKeyDown(KeyCode.Space))//once D is pressed
        {
            space.sprite = PS[10];
        }
        if (Input.GetKeyUp(KeyCode.Space))//once p is unpressed
        {
            space.sprite = PS[9];

        }
    }
}
