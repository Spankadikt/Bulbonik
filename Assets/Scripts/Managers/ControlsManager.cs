using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ControlsManager : MonoBehaviour
{
	//VAR
	public static float s_fSwipeTweek = 0.5f;

	public delegate void SwipeHandler();
	public static event SwipeHandler OnSwipe;

	public delegate void TouchHandler(Vector3 point);
	public static event TouchHandler OnTouch;

	public delegate void PressHandler(Vector3 point,float duration);
	public static event PressHandler OnPress;

	public delegate void ReleaseHandler(Vector3 point);
	public static event ReleaseHandler OnRelease;
	
	Vector2 m_v2FirstPressPos;
	Vector2 m_v2SecondPressPos;
	Vector2 m_v2CurrentSwipe;

	private float m_fTouchTimer = 0f;
	private float m_fSwipeTimer = 0f;
	private float m_fPressTimer = 0f;
	private float m_fTimeToTouch = 0.15f;
	// Use this for initialization
	void Start ()
	{
		OnSwipe += new SwipeHandler(MapManager.SideScroll);
		OnSwipe += new SwipeHandler(AntibodiesManager.SpawnAntibodies);
		OnTouch += new TouchHandler(VirusesManager.CheckTouch);
		OnPress += new PressHandler(VirusesManager.CheckPress);
		OnRelease += new ReleaseHandler(VirusesManager.Release);
	}

	// Update is called once per frame
	void Update ()
	{
#if UNITY_EDITOR
		ClickDetection();
		MouseSwipeDetection();
		MousePressDetection();


#else
		TouchDetection();
		TouchSwipeDetection();
		TouchPressDetection();

#endif
	}

	void TouchDetection()
	{
		if(Input.touches.Length == 1)
		{
			m_fTouchTimer += Time.deltaTime;
		}
		if(Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			if(m_fTouchTimer < m_fTimeToTouch)
			{
				//get touch position on screen
				OnTouch(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0));
			}
			m_fTouchTimer = 0;
		}
	}

	void ClickDetection()
	{
		if(Input.GetMouseButton(0))
		{
			m_fTouchTimer += Time.deltaTime;
		}
		if(Input.GetMouseButtonUp(0))
		{
			if(m_fTouchTimer < m_fTimeToTouch)
			{
				OnTouch(Input.mousePosition);
			}
			m_fTouchTimer = 0;
		}
	}

	bool pressed = false;
	void TouchPressDetection()
	{
		if(Input.touches.Length == 1)
		{
			m_fPressTimer += Time.deltaTime;
			if(m_fPressTimer > m_fTimeToTouch && !pressed)
			{
				pressed = true;
				//get touch position on screen
				OnPress(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0),m_fPressTimer);
			}
		}
		if(Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			pressed = false;
			if(m_fPressTimer > m_fTimeToTouch)
			{
				//get touch position on screen
				OnRelease(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0));
			}
			m_fPressTimer = 0;
		}
	}
	
	void MousePressDetection()
	{
		if(Input.GetMouseButton(0))
		{
			m_fPressTimer += Time.deltaTime;
			if(m_fPressTimer > m_fTimeToTouch && !pressed)
			{
				pressed = true;
				OnPress(Input.mousePosition,m_fPressTimer);
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			pressed = false;
			if(m_fPressTimer > m_fTimeToTouch)
			{
				OnRelease(Input.mousePosition);
			}
			m_fPressTimer = 0;
		}
	}

	void TouchSwipeDetection()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				m_fSwipeTimer += Time.deltaTime;
				//save began touch 2d point
				m_v2FirstPressPos = new Vector2(t.position.x,t.position.y);
			}
			
			if(t.phase == TouchPhase.Ended)
			{
				if(m_fSwipeTimer > m_fTimeToTouch)
				{
					//save ended touch 2d point
					m_v2SecondPressPos = new Vector2(t.position.x,t.position.y);
					if(Vector2.Distance(m_v2FirstPressPos,m_v2SecondPressPos)>100)
						SwipeDirectionDetection();
				}
				m_fSwipeTimer = 0f;
			}
		}
	}
	
	void MouseSwipeDetection()
	{
		if(Input.GetMouseButton(0))
			m_fSwipeTimer += Time.deltaTime;
		if(Input.GetMouseButtonDown(0))
		{
			//save began touch 2d point
			m_v2FirstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if(Input.GetMouseButtonUp(0))
		{
			if(m_fSwipeTimer > m_fTimeToTouch)
			{
				//save ended touch 2d point
				m_v2SecondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
				if(Vector2.Distance(m_v2FirstPressPos,m_v2SecondPressPos)>150)
					SwipeDirectionDetection();
			}
			m_fSwipeTimer = 0f;
		}
	}

	void SwipeDirectionDetection()
	{

		//create vector from the two points		
		m_v2CurrentSwipe = new Vector2(m_v2SecondPressPos.x - m_v2FirstPressPos.x, m_v2SecondPressPos.y - m_v2FirstPressPos.y); 
		//normalize the 2d vector
		m_v2CurrentSwipe.Normalize();	
		//swipe upwards
		if(m_v2CurrentSwipe.y > 0 && m_v2CurrentSwipe.x > -s_fSwipeTweek && m_v2CurrentSwipe.x < s_fSwipeTweek)
		{
			Debug.Log("up swipe");
		}
		//swipe down
		if(m_v2CurrentSwipe.y < 0 && m_v2CurrentSwipe.x > -s_fSwipeTweek && m_v2CurrentSwipe.x < s_fSwipeTweek)
		{
			Debug.Log("down swipe");
		}
		//swipe left
		if(m_v2CurrentSwipe.x < 0 && m_v2CurrentSwipe.y > -s_fSwipeTweek && m_v2CurrentSwipe.y < s_fSwipeTweek)
		{
			Debug.Log("left swipe");
			OnSwipe();
			
		}
		//swipe right
		if(m_v2CurrentSwipe.x > 0 && m_v2CurrentSwipe.y > -s_fSwipeTweek && m_v2CurrentSwipe.y < s_fSwipeTweek)
		{
			Debug.Log("right swipe");
		}

	}
}

