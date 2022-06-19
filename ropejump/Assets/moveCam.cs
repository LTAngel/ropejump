using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class moveCam : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    
    Vector3 moveX = new Vector3(18, 0, 0);
    Vector3 moveY = new Vector3(0, 10, 0);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 rbPos = new Vector3(rb.transform.position.x, rb.transform.position.y, -10f);
        Vector3 relPos = rbPos - transform.position;
        
        if(relPos.x <= -9)transform.position -= moveX;
        if(relPos.x > 9)transform.position += moveX;
        if(relPos.y <= -5)transform.position -= moveY;
        if(relPos.y > 5)transform.position += moveY;
    }
}
