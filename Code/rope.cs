using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    Rigidbody2D player;
    public LayerMask findRope; //check if layer is a rope
    public float speed = 7f;
    public float distance = 5; //good distance is 5-6 in test
    private bool isClimbing;
    private float inputVertical;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //CLIMBING ROPE
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, findRope);
        if (hitInfo.collider != null){
            isClimbing = true;
        }
        else {
            isClimbing = false;
        }
        
    }

    void FixedUpdate(){ //disable horizontal movement!
        if (isClimbing == true){
            inputVertical = Input.GetAxisRaw("Vertical");
            player.velocity = new Vector2(player.velocity.x, inputVertical * speed);
            player.gravityScale = 0; //no gravity while climbing
        }
        else {
            player.gravityScale = 3;
        }
    }
}
