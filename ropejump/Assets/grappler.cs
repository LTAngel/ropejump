using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappler : MonoBehaviour
{

    public Rigidbody2D rb;
    public bool isGrounded;
    // private bool confused = false;
    public Camera cam;
    private Vector2 mousePos;
    public float pullForce;
    public float swingForce;
    SpringJoint2D rope;
    DistanceJoint2D hook;
    Vector2 lookDir;
    public float maxGrappleLength;
    public LayerMask ropeLayerMask;
    public float dRatio;
    public float ropeL;
    public float ropeF;
    private bool hitCheck;

    LineRenderer line;




    // Update is called once per frame
    void Start(){

        hook = gameObject.AddComponent<DistanceJoint2D>();
        hook.enabled = false;
        hook.enableCollision = true;

        hook.distance = 1f;

        rope = gameObject.AddComponent<SpringJoint2D>();
        rope.enabled = false;
        rope.enableCollision = true;
        
        rope.distance = ropeL;
        rope.dampingRatio = dRatio;
        rope.frequency = ropeF;

        line = GetComponent<LineRenderer>();
        line.enabled = false;

        isGrounded = true;
        hitCheck = false;
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.collider.name == "TileMap"){
            Debug.Log("bounce");
        }
        isGrounded = true;
        Debug.Log("grounded");
    }


    void Update()
    {

        getMousePos();
        line.SetPosition(0, transform.position);
        //checkCamPos();
        //checkConfusion();

        if(Input.GetMouseButtonDown(0)){
            Debug.Log("clicked");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir, maxGrappleLength, ropeLayerMask);

            if(hit.collider != null){
                hitCheck = true;
                if(isGrounded){
                    grapple(hit);
                }else if(!isGrounded){
                    swing(hit);
                }
                
            }else{
                hitCheck = false;
            }
        }

        if(Input.GetMouseButtonUp(0) && hitCheck){

            destroyRope();
            
        }
    }

    void getMousePos(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void grapple(RaycastHit2D hit){

        isGrounded = false;

        rope.connectedAnchor = hit.point; 
        line.SetPosition(1, hit.point);
        rope.enabled = true;
        line.enabled = true;
        

        rope.distance = ropeL;
        // rope.dampingRatio = dRatio;
        // rope.frequency = ropeF;
        
        
    }

    void swing(RaycastHit2D hit){

        line.SetPosition(1, hit.point);
        line.enabled = true;
        hook.connectedAnchor = hit.point;
        hook.enabled = true;
        hook.distance = hit.distance;

    }

    // void destroyHook(){
    //     hook.enabled = false;
    //     line.enabled = false;
    //     rb.AddForce(transform.up * 2, ForceMode2D.Impulse);
    // }
    
    void destroyRope(){
        rope.enabled = false;
        if(hook.enabled == true){
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
        hook.enabled = false;
        line.enabled = false;
        
    }


    // void checkCamPos(){
    //     Vector2 relPos = rb.position - cam.position;

    //     if(relPos.x <= 0){cam.position -= 18};
    //     if(relPos.x > 18){cam.position += 18};
    //     if(relPos.y <= 0){cam.position -= 10};
    //     if(relPos.y > 10){cam.position += 10};
    // }
    // void swing(){

    //     if(lookDir.x > 0){
    //         while(Input.GetMouseButton(0)){
    //             rb.AddRelativeForce(Vector2.right * swingForce, ForceMode2D.Force);
    //         }
    //     }else if(lookDir.x < 0){
    //         while(Input.GetMouseButton(0)){
    //             rb.AddRelativeForce(Vector2.left * swingForce, ForceMode2D.Force);
    //         }
    //     }
        
    // }

    // void checkConfusion(){
    //     rb.onCollisionEnter
    // }
}
