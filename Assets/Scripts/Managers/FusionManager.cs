using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class FusionManager : MonoBehaviour
{
	VirusesManager _virusesManager;

	void Start ()
	{
		_virusesManager = GameObject.Find("Managers").GetComponent<VirusesManager>();
	}
	
	void Update ()
	{

	}

	public void Fusion(Virus _firstVirus, Virus _secondVirus)
  	{
		Vector3 posFusion = (_firstVirus.transform.position + _secondVirus.transform.position)/2;

		StartCoroutine(_virusesManager.SpawnVirusAt(1,InfoTypeFusion(_firstVirus,_secondVirus),InfoRankFusion(_firstVirus,_secondVirus),posFusion));
		_firstVirus.DestroyVirus();
		_secondVirus.DestroyVirus();
	}

	public VirusRank InfoRankFusion(Virus _firstVirus, Virus _secondVirus)
	{
		if(_firstVirus.m_eVirusRank == _secondVirus.m_eVirusRank)
		{
			if(_firstVirus.m_eVirusRank == VirusRank.D)
			{
				return VirusRank.C;
			}
			else if(_firstVirus.m_eVirusRank == VirusRank.C)
			{
				return VirusRank.B;
			}
			else
			{
				return VirusRank.A;
			}
		}
		else
		{
			return VirusRank.None;
		}
	}

	public VirusType InfoTypeFusion(Virus _firstVirus, Virus _secondVirus)
	{
		//same rank
		if(_firstVirus.m_eVirusRank == _secondVirus.m_eVirusRank)
		{
			//same type
			if(_firstVirus.m_eVirusType == _secondVirus.m_eVirusType)
			{
				if(_firstVirus.m_eVirusType == VirusType.Black)
				{
					return VirusType.Black;
				}
				else if(_firstVirus.m_eVirusType == VirusType.Blue)
				{
					return VirusType.Blue;
				}
				else if(_firstVirus.m_eVirusType == VirusType.Red)
				{
					return VirusType.Red;
				}
				else
				{
					return VirusType.Green;
				}
			}
			else
			{
				return VirusType.None;
			}
		}
		else//no special fusion
		{
			return VirusType.None;
		}
	}
}

