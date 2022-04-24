using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D player;
    public static bool midJump = false;
    public LayerMask whatIsIceWall; //check if layer is an icewall (for climbing)
    public LayerMask whatIsLadder; //check if layer is an icewall (for climbing)
    public float distance = 5; //good distance is 5-6 in test
    private bool isClimbing;
    //private bool dodge;
    public float speed = 7f;
    public float dodgeSpeed = 10f;
    private float inputVertical;
    GameObject iceWallObject;
    


    //toDo:
    //disable moving left and right in air
    //climb (climb on ice)
        //REMEMBER to set layer of icewall object to "icewall"
        //set icewall box collider to trigger mode        

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Awake()
    
    {
        iceWallObject = GameObject.FindGameObjectWithTag("iceWall");
    }

    // Update is called once per frame
    void Update()
    {
        //LEFT, RIGHT
        float direction = Input.GetAxis("Horizontal"); //change to GetAxisRaw to remove smooth movement if preferred
        player.velocity = new Vector2(direction * speed, player.velocity.y); //move left or right (change to buttons on display!)


        //JUMPING
        if (Input.GetButtonDown("Jump") && midJump == false){ //change to button on display! //change to not accept jump when in air
            Jump();
        }
        if (player.velocity.y == 0) {
            midJump = false;
        }

        //DODGE
        if (Input.GetKeyDown(KeyCode.Z) && midJump == false) {
            player.transform.Translate (-2,0,0); //make it smooth?
            //dodge = true;
        }

        
        //CLIMBING ICEWALL
        //get iceWallStatus to determine if action can be performed (0 or 1)
        float checkStatus = iceWallObject.GetComponent<iceWall>().iceWallStatus;

        if (checkStatus == 1){
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsIceWall);
            if (hitInfo.collider != null){
                isClimbing = true;
            }
            else {
                isClimbing = false;
            }
        }
        
        //CLIMBING LADDERS
        //RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        //if (hitInfo2.collider != null){
            //isClimbing = true;
        //}
        //else {
            //isClimbing = false;
        //}

    }

    public void Jump(){
        player.velocity = new Vector2(0,12f);
        midJump = true;
    }

    void FixedUpdate(){
        if (isClimbing == true){
            inputVertical = Input.GetAxisRaw("Vertical");
            player.velocity = new Vector2(player.velocity.x, inputVertical * speed);
            player.gravityScale = 0; //no gravity while climbing
        }
        else {
            player.gravityScale = 3;
        }

        //if (dodge){
            //float dodgeSpeedDrop = 5f;
            //dodgeSpeed -= dodgeSpeed * dodgeSpeedDrop * Time.deltaTime;
            //player.velocity = new Vector2(-5,0) * dodgeSpeed;

            //dodge = false;
        //}
    }
}