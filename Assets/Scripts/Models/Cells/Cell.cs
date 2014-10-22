using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class Cell : Entity
	{
		public CellType m_eCellType;
		public CellState m_eCellState;
		public string m_sName;	

		private float m_fRefreshTargetTime = 3f;
		private float m_fRefreshTargetTimer = 0f;

		public Cell()
		{
			base.m_fSpeed = 0.2f;
			base.m_fTargetTransitionSpeed = 1f;
		}

		// Use this for initialization
		public override void Start ()
		{
			gameObject.AddComponent<Rigidbody2D>();
			gameObject.rigidbody2D.isKinematic = true;
			gameObject.AddComponent<CircleCollider2D>();
			gameObject.collider2D.isTrigger = true;

			CellsManager.s_LstCells.Add(this);

			if(!string.IsNullOrEmpty(m_sName))
				this.gameObject.name = m_sName;

			if(m_v3SpawnPoint != Vector3.zero)
				this.transform.position = base.m_v3SpawnPoint;
			else
				DetermineSpawnPoint();

			DetermineTargetPoint ();
		}

		// Update is called once per frame
		public override void Update ()
		{
			m_fRefreshTargetTimer+= Time.deltaTime;
			if(m_fRefreshTargetTimer>m_fRefreshTargetTime)
			{
				m_fRefreshTargetTimer = 0f;
				DetermineTargetPoint();
			}

			UpdateTargetPoint ();
			Move ();
		}

		public override void Move()
		{
			transform.position = Vector3.Lerp (transform.position, base.m_v3TargetPoint, base.m_fSpeed * Time.deltaTime);
		}

		public override void UpdateTargetPoint()
		{
			base.m_v3TargetPoint = Vector3.Lerp (base.m_v3TargetPoint, base.m_v3NewTargetPoint, base.m_fTargetTransitionSpeed * Time.deltaTime);
		}

		public override void DetermineSpawnPoint()
		{
			this.transform.position = MapManager.GetRandomPointInPickMap();
		}

		public override void DetermineTargetPoint()
		{
			base.m_v3NewTargetPoint = MapManager.GetRandomPointInPickMap();
		}

		public override void OnDrawGizmos()
		{
			if(EditorDebug.s_bDrawCells)
			{
				switch(m_eCellType)
				{
				case CellType.Blue:
					Gizmos.color = Color.blue;
					break;
				case CellType.Green:
					Gizmos.color = Color.green;
					break;
				case CellType.Red:
					Gizmos.color = Color.red;
					break;
				default:break;
				}
				Gizmos.DrawSphere(this.transform.position,0.5f);
			}
		}

		public void DestroyCell()
		{
			CellsManager.s_LstCells.Remove(this);
			Destroy(this.gameObject);
		}
	}
}
