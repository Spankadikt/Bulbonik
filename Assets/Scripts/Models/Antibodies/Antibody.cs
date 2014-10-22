using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class Antibody : Entity
	{
		public AntibodyType m_eAntibodyType;
		public AntibodyState m_eAntibodyState;
		public string m_sName;	

		public Antibody()
		{
			base.m_fSpeed = 1f;
			base.m_fTargetTransitionSpeed = 10f;
		}

		public Antibody(Vector3 _spawnPoint)
		{
			base.m_fSpeed = 1f;
			base.m_fTargetTransitionSpeed = 10f;

			base.m_v3SpawnPoint = _spawnPoint;
		}

		public override void Start ()
		{
			if(!string.IsNullOrEmpty(m_sName))
				this.gameObject.name = m_sName;
			
			if(m_v3SpawnPoint != Vector3.zero)
				this.transform.position = base.m_v3SpawnPoint;
			else
				DetermineSpawnPoint();
		}

		public override void Update ()
		{
			UpdateTargetPoint ();
			Move ();
		}

		public override void Move()
		{
			transform.position = Vector3.MoveTowards (transform.position, base.m_v3TargetPoint, base.m_fSpeed * Time.deltaTime);
		}
		
		public override void UpdateTargetPoint()
		{
			//Update target point
		}

		public override void DetermineSpawnPoint()
		{
			m_v3SpawnPoint = MapManager.GetRandomPointInLine (MapManager.s_v3FightMapCornerTopRight, MapManager.s_v3FightMapCornerBotRight);
			this.transform.position = m_v3SpawnPoint;
		}
		
		public override void DetermineTargetPoint()
		{
			//Determine target point
		}

		public override void OnDrawGizmos()
		{
			if(EditorDebug.s_bDrawAntibodies)
			{
				switch(m_eAntibodyType)
				{
				case AntibodyType.Normal:
					Gizmos.color = Color.white;
					break;
				default:break;
				}
				Gizmos.DrawSphere(this.transform.position,0.5f);
			}
		}
	}
}
