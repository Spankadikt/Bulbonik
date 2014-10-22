using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class DuplicateManager : MonoBehaviour
{
	VirusesManager _virusesManager;
	
	void Start ()
	{
		_virusesManager = GameObject.Find("Managers").GetComponent<VirusesManager>();
	}

	void Update ()
	{

	}

	public void Duplicate(Virus _virus, Cell _cell)
	{
		Vector3 posDuplicate= (_virus.transform.position + _cell.transform.position)/2;
		
		StartCoroutine(_virusesManager.SpawnVirusAt(2,_virus.m_eVirusType,_virus.m_eVirusRank,posDuplicate));
		_virus.DestroyVirus();
		_cell.DestroyCell();
	}
}

