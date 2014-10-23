using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class Virus : Entity
	{
		public FusionManager _fusionManager;
		public DuplicateManager _duplicateManager;

		public VirusType m_eVirusType;
		public VirusRank m_eVirusRank;
		public VirusState m_eVirusState;
		public string m_sName;	

		public Vector3 s_v3PointToFollow;

		public delegate void VirusTouchHandler();
		public event VirusTouchHandler OnTouch;

		public delegate void VirusPressHandler(Vector3 pressPoint);
		public event VirusPressHandler OnPress;

		public delegate void VirusReleaseHandler();
		public event VirusReleaseHandler OnRelease;

		private Virus nearestVirus;
		private Cell nearestCell;

		private List<Vector3> tabLastPos = new List<Vector3>();

		public Virus()
		{
			base.m_fSpeed = 5f;
			base.m_fTargetTransitionSpeed = 50f;
			
			OnTouch += Selected;
			OnPress += Grabbed;
			OnRelease += Released;

		}


		public override void Start ()
		{
			_fusionManager = GameObject.Find("Managers").GetComponent<FusionManager>();
			_duplicateManager = GameObject.Find("Managers").GetComponent<DuplicateManager>();

			VirusesManager.s_LstViruses.Add(this);

			gameObject.AddComponent<Rigidbody2D>();
			gameObject.rigidbody2D.isKinematic = true;

			gameObject.AddComponent<CircleCollider2D>();

			if(!string.IsNullOrEmpty(m_sName))
				this.gameObject.name = m_sName;

		}
		
		public override void Update ()
		{

			if(m_eVirusState == VirusState.Grabbed)
			{
				GrabBehaviour();
			}
			else
			{
				MoveBehaviour();
			}
			UpdateTargetPoint();
		}

		public override void Move()
		{
			Vector3 result = Vector3.Lerp (transform.position, base.m_v3TargetPoint, base.m_fSpeed * Time.deltaTime);
			if(result.x > MapManager.s_v3PickMapCornerTopLeft.x &&
			   result.x < MapManager.s_v3PickMapCornerTopRight.x &&
			   result.y > MapManager.s_v3PickMapCornerBotLeft.y &&
			   result.y < MapManager.s_v3PickMapCornerTopLeft.y)
			{
				transform.position = result;
				if(tabLastPos.Count>=10)
					tabLastPos.RemoveAt(0);
				tabLastPos.Add(result);
			}
		}

		public void Steer()
		{
			Vector3 separation = Vector3.zero;

			for(int i =0;i<VirusesManager.s_LstViruses.Count;i++)
			{
				Virus v = (Virus)VirusesManager.s_LstViruses[i];
				CircleCollider2D collider2D= v.gameObject.GetComponent<CircleCollider2D>();
				float width = collider2D.radius;

				if(v != this &&
				   v.m_eVirusState == m_eVirusState&&
				   Vector3.Distance(v.transform.position,transform.position)<VirusesManager.m_fDynamicAvoidance+width)
				{
					Vector3 relativePos = transform.position - v.transform.position;
					separation += relativePos/(relativePos.sqrMagnitude);
				}
			}

			Vector3 result = Vector3.zero;

			result = separation * VirusesManager.m_fAvoidance + s_v3PointToFollow;

			result.z = 0;
			base.m_v3NewTargetPoint = result ;

		}

		public void PerformOnRelease()
		{
			OnRelease();
		}

		public void PerformOnTouch()
		{
			OnTouch();
		}

		public void PerformOnPress(Vector3 _screenPoint)
		{
			OnPress(_screenPoint);
		}

		private void Released()
		{
			m_eVirusState = VirusState.Idle;

			s_v3PointToFollow = transform.position;
			m_v3TargetPoint = transform.position;
			base.m_v3NewTargetPoint = transform.position;


			if(nearestCell != null && nearestVirus !=null)
			{
				float distCell = Vector3.Distance(this.transform.position,nearestCell.transform.position);
				float distVirus = Vector3.Distance(this.transform.position,nearestVirus.transform.position);

				if(distCell>distVirus)
				{
					if(InRangeForAction(nearestVirus.gameObject) &&
					   nearestVirus != null &&
					   _fusionManager.InfoTypeFusion(this,nearestVirus)!= VirusType.None &&
					   _fusionManager.InfoRankFusion(this,nearestVirus)!= VirusRank.None)
					{
						//fusion
						_fusionManager.Fusion(this,nearestVirus);
					}
				}
				else
				{
					if(InRangeForAction(nearestCell.gameObject))
						_duplicateManager.Duplicate(this,nearestCell);
				}
			}
			else if(nearestCell != null)
			{
				if(InRangeForAction(nearestCell.gameObject))
					_duplicateManager.Duplicate(this,nearestCell);
			}
			else
			{
				if(InRangeForAction(nearestVirus.gameObject) &&
				   nearestVirus != null &&
				   _fusionManager.InfoTypeFusion(this,nearestVirus)!= VirusType.None &&
				   _fusionManager.InfoRankFusion(this,nearestVirus)!= VirusRank.None)
				{
					//fusion
					_fusionManager.Fusion(this,nearestVirus);
				}
			}
		}

		private void Selected()
		{
			Debug.Log(m_sName + " selected");

			if(m_eVirusState != VirusState.Selected)
			{
				DetermineTargetPoint(this.transform.position);
				m_eVirusState = VirusState.Selected;
			}
			else
			{
				m_eVirusState = VirusState.Idle;
			}
		}

		private void Grabbed(Vector3 _screenPoint)
		{
			//drag for fusion
			Debug.Log(m_sName + " grabbed");
			m_eVirusState = VirusState.Grabbed;
		}

		public override void UpdateTargetPoint()
		{
			base.m_v3TargetPoint = Vector3.Lerp (base.m_v3TargetPoint, base.m_v3NewTargetPoint, base.m_fTargetTransitionSpeed * Time.deltaTime);
		}
		
		public override void DetermineSpawnPoint()
		{
			this.transform.position = MapManager.GetRandomPointInPickMap();

		}
		
		public void DetermineTargetPoint(Vector3 _pos)
		{

			s_v3PointToFollow = _pos;
		}

		public void GrabBehaviour()
		{
			#if UNITY_EDITOR
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0;
			transform.position = pos;
			#elif UNITY_STANDALONE
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0;
			transform.position = ImagePosition;
			#else
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0));
			pos.z = 0;
			transform.position = pos;
			#endif

			//check for performing a fusion
			SeekForFusion();

			if(nearestVirus != null)
				Debug.Log(nearestVirus.m_sName + " is the nearest virus");

			//if it's a basic virus then we can check for performing a duplicate
			SeekForDuplicate();

			if(nearestCell != null)
				Debug.Log(nearestCell.m_sName + " is the nearest cell");
		}

		public void MoveBehaviour()
		{
			if(m_eVirusState == VirusState.Selected || m_eVirusState == VirusState.Idle)
			{
				Steer ();
				if(transform.position != m_v3TargetPoint)
				{
					Move();
				}
			}
		}

		void SeekForFusion()
		{
			for(int i =0;i<VirusesManager.s_LstViruses.Count;i++)
			{
				Virus v = (Virus)VirusesManager.s_LstViruses[i];
				if(v!= this && v.m_eVirusRank == m_eVirusRank)
				{
					CircleCollider2D collider2D= v.gameObject.GetComponent<CircleCollider2D>();
					float width = collider2D.radius;
					
					CircleCollider2D thisCollider2D= gameObject.GetComponent<CircleCollider2D>();
					float thisWidth = thisCollider2D.radius;
					
					if(Vector3.Distance(v.transform.position,transform.position) < (width + thisWidth)/2)
					{
						if(nearestVirus != null)
						{
							float distFromV = Vector3.Distance(transform.position,v.transform.position);
							float distFromNearest = Vector3.Distance(transform.position, nearestVirus.transform.position);
							
							if(distFromV < distFromNearest)
							{
								nearestVirus = v;
							}
						}
						else
						{
							nearestVirus = v;
						}
					}
				}
			}
		}

		void SeekForDuplicate()
		{
			if(m_eVirusRank == VirusRank.D)
			{
				for(int i=0;i<CellsManager.s_LstCells.Count;i++)
				{
					Cell c = (Cell)CellsManager.s_LstCells[i];
					if(c.m_eCellType.ToString() == m_eVirusType.ToString())
					{
						if(InRangeForAction(c.gameObject))
						{
								nearestCell = c;
						}
					}
				}
			}
		}

		bool InRangeForAction(GameObject _g)
		{
			CircleCollider2D collider2D= _g.GetComponent<CircleCollider2D>();
			float width = collider2D.radius;
			
			CircleCollider2D thisCollider2D= gameObject.GetComponent<CircleCollider2D>();
			float thisWidth = thisCollider2D.radius;
			
			if(Vector3.Distance(_g.transform.position,transform.position) < (width + thisWidth)/2)
				return true;
			else return false;
		}

		public override void OnDrawGizmos()
		{
			if(EditorDebug.s_bDrawVirus)
			{
				switch(m_eVirusType)
				{
				case VirusType.Black:
					Gizmos.color = Color.black;
					break;
				case VirusType.Blue:
					Gizmos.color = Color.blue;
					break;
				case VirusType.Green:
					Gizmos.color = Color.green;
					break;
				case VirusType.Red:
					Gizmos.color = Color.red;
					break;
				default:break;
				}
				float size = 0f;
				switch(m_eVirusRank)
				{
				case VirusRank.D:
					size = 0.5f;
					break;
				case VirusRank.C:
					size = 0.6f;
					break;
				case VirusRank.B:
					size = 0.7f;
					break;
				case VirusRank.A:
					size = 0.8f;
					break;
				case VirusRank.S:
					size = 0.9f;
					break;
				default:break;
				}
				Gizmos.DrawWireSphere(this.transform.position,size);
				Gizmos.DrawLine(transform.position,m_v3NewTargetPoint);
			}
		}

		public void DestroyVirus()
		{
			VirusesManager.s_LstViruses.Remove(this);
			Destroy(this.gameObject);
		}
	}
}

