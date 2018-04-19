using UnityEngine;
using System.Collections;

public class JumpHandler : MonoBehaviour
{
    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    /*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
    private bool grounded;
    public LayerMask whatIsGround;
    public bool stoppedJumping;

    /*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    public Transform groundCheck;
    public float groundCheckRadius;

    //You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :)
    private Rigidbody2D rb;

		void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

    void Start()
    {
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
    }

    void Update()
    {
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);


        //if we are grounded...
        if(grounded)
        {
            //the jumpcounter is whatever we set jumptime to in the editor.
            jumpTimeCounter = jumpTime;
        }
    }

    void FixedUpdate()
    {
        //I placed this code in FixedUpdate because we are using phyics to move.

				//Convert to polar coordinates so the player jumps normal to the planet
				Vector2 verticalDirection = Conversions.getVectorFromAngle((rb.rotation + 90) * Mathf.Deg2Rad);

        //if you press down the mouse button...
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            //and you are on the ground...
            if(grounded)
            {
                //jump!
                //rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
								rb.velocity = new Vector2(rb.velocity.x + verticalDirection.x * jumpForce, rb.velocity.y + verticalDirection.y * jumpForce);
                stoppedJumping = false;
            }
        }

        //if you keep holding down the mouse button...
        if((Input.GetKey(KeyCode.Space)) && !stoppedJumping)
        {
            //and your counter hasn't reached zero...
            if(jumpTimeCounter > 0)
            {
                //keep jumping!
                //rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
								rb.velocity = new Vector2(rb.velocity.x + verticalDirection.x * jumpForce, rb.velocity.y + verticalDirection.y * jumpForce);
            }
        }


        //if you stop holding down the mouse button...
        if(Input.GetKeyUp(KeyCode.Space))
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

				jumpTimeCounter -= Time.deltaTime;

				//if you are going down
				if(jumpTimeCounter <= 0) {
					rb.velocity = new Vector2(rb.velocity.x - verticalDirection.x * jumpForce, rb.velocity.y - verticalDirection.y * jumpForce);
				}
    }
}
