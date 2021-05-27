using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float currentSpeed = 5.0f;
    public float acceleration = 0.01f;
    public float laneSpeed = 7.0f;
    public int currentLane = 0;
    public int score = 0;
    public static float laneOffset = 2.0f;
    public static int numOfLanes = 5;

#if !UNITY_STANDALONE
    protected Vector2 m_StartingTouch;
	protected bool m_IsSwiping = false;
#endif


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameManagerInstance.gameActive)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            // Use key input in editor or standalone
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(1);
            }
#else
        // Use touch input on mobile
        if (Input.touchCount == 1)
        {
			if(m_IsSwiping)
			{
				Vector2 diff = Input.GetTouch(0).position - m_StartingTouch;

				// Put difference in Screen ratio, but using only width, so the ratio is the same on both
                // axes (otherwise we would have to swipe more vertically...)
				diff = new Vector2(diff.x/Screen.width, diff.y/Screen.width);

				if(diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
				{
						if(diff.x < 0)
						{
							ChangeLane(-1);
						}
						else
						{
							ChangeLane(1);
						}
					}		
					m_IsSwiping = false;
				
            }

        	// Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
			// a swipe can still be registered (otherwise, m_IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				m_StartingTouch = Input.GetTouch(0).position;
				m_IsSwiping = true;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				m_IsSwiping = false;
			}
        }

#endif
            currentSpeed = currentSpeed + acceleration;

            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentLane * laneOffset, transform.position.y, transform.position.z), Time.deltaTime * laneSpeed);

        }
    }

    public void ChangeLane(int direction)
    {
        //if (!trackManager.isMoving)
        //return;

        int targetLane = currentLane + direction;

        if (targetLane < -2 || targetLane > 2)
            // Ignore, we are on the borders.
            return;

        currentLane = targetLane;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
            GameManager.gameManagerInstance.gameOver();
    }

    public void collectItem()
    {
        score++;
    }
}
