using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
	protected Vector3 m_v3SpawnPoint;
	public Vector3 m_v3NewTargetPoint;
	protected Vector3 m_v3TargetPoint;
	protected float m_fSpeed = 1f;
	protected float m_fTargetTransitionSpeed = 10f;

	public Entity ()
	{
	}

	// Use this for initialization
	public virtual void Start ()
	{

	}
	
	// Update is called once per frame
	public virtual void Update ()
	{

	}

	public virtual void Move()
	{

	}

	public virtual void Attack()
	{
	}
	
	public virtual void UpdateTargetPoint()
	{

	}
	
	public virtual void DetermineSpawnPoint()
	{

	}
	
	public virtual void DetermineTargetPoint()
	{

	}
	
	public virtual void OnDrawGizmos()
	{

	}
}

