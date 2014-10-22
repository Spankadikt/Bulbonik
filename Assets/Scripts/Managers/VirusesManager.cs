using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class VirusesManager : MonoBehaviour
{

	//VAR
	public static int s_nMaxVirus = 50;
	public static float s_fTimeMorph = 2f;
	public static int s_nStartSpawnVirus = 12;
	
	public static float s_fBlackVirusRateSpawn = 0.25f;
	public static float s_fRedVirusRateSpawn = 0.25f;
	public static float s_fBlueVirusRateSpawn = 0.25f;
	public static float s_fGreenVirusRateSpawn = 0.25f;

	public static float m_fDynamicAvoidance = 0.5f;
	public static float m_fAvoidance = 0.5f;
		
	public static ArrayList s_LstViruses;
	
	private float m_fFusionTimer = 0f;
	
	void Start ()
	{
		s_LstViruses = new ArrayList();
		StartCoroutine(StartSpawnVirus(s_nStartSpawnVirus));
	}
	
	void Update ()
	{
		m_fDynamicAvoidance = Mathf.Clamp(Mathf.Exp(0.5f*s_LstViruses.Count*0.1f),1f,5f);
		Debug.Log(s_LstViruses.Count + " viruses, avoidance : " + m_fDynamicAvoidance);
	}

	public static void Release()
	{
		for(int i=0;i<s_LstViruses.Count;i++)
		{
			Virus v = (Virus)s_LstViruses[i];
			if(v.m_eVirusState == VirusState.Grabbed)
			{
				s_bVirusGrabbed = false;
				ReleaseVirus(v);
			}
		}
	}

	public static void CheckTouch(Vector3 _touchPoint)
	{
		Ray ray = Camera.main.ScreenPointToRay(_touchPoint);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

		if (hit) 
		{
			if(hit.transform.gameObject.GetComponent<Virus>() != null)
			{
				SelectVirus(hit.transform.gameObject.GetComponent<Virus>());
			}
		}
		else
		{
			SetPointToFollow(_touchPoint);
		}
	}

	public static bool s_bVirusGrabbed = false;
	public static void CheckPress(Vector3 _touchPoint, float duration)
	{
		Ray ray = Camera.main.ScreenPointToRay(_touchPoint);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
		
		if (hit) 
		{
			if(hit.transform.gameObject.GetComponent<Virus>() != null && !s_bVirusGrabbed)
			{
				s_bVirusGrabbed = true;
				GrabbVirus(hit.transform.gameObject.GetComponent<Virus>(),_touchPoint);
			}
			else
			{
			}
		}
		else
		{
		}
	}

	private static void ReleaseVirus(Virus _v)
	{
		_v.PerformOnRelease();
	}

	private static void SelectVirus(Virus _v)
	{
		_v.PerformOnTouch();
	}

	private static void GrabbVirus(Virus _v, Vector3 _screenPoint)
	{
		_v.PerformOnPress(_screenPoint);
	}

	public static void SetPointToFollow(Vector3 _pointToFollow)
	{
		for(int i=0;i<s_LstViruses.Count;i++)
		{
			Virus v = (Virus)s_LstViruses[i];
			if(v.m_eVirusState == VirusState.Selected)
				v.s_v3PointToFollow= Camera.main.ScreenToWorldPoint(_pointToFollow);
		}
	}

	public static GameObject CreateVirusAt(VirusType _virusType, VirusRank _virusRank,Vector3 _pos)
	{
		GameObject go = new GameObject();
		if(_virusType == VirusType.Black)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DBlackVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CBlackVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BBlackVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else if(_virusType == VirusType.Red)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DRedVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CRedVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BRedVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else if(_virusType == VirusType.Blue)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DBlueVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CBlueVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BBlueVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else if(_virusType == VirusType.Green)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DGreenVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CGreenVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BGreenVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else//Special
		{
			if(_virusRank == VirusRank.D)
			{
			}
			else if(_virusRank == VirusRank.C)
			{
			}
			else if(_virusRank == VirusRank.B)
			{
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}

		go.transform.position = _pos;
		go.GetComponent<Virus>().DetermineTargetPoint(_pos);
		return go;
	}

	public static GameObject CreateVirusAtRandom(VirusType _virusType, VirusRank _virusRank)
	{
		GameObject go = new GameObject();
		if(_virusType == VirusType.Black)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DBlackVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CBlackVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BBlackVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else if(_virusType == VirusType.Red)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DRedVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CRedVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BRedVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else if(_virusType == VirusType.Blue)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DBlueVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CBlueVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BBlueVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else if(_virusType == VirusType.Green)
		{
			if(_virusRank == VirusRank.D)
			{
				go.AddComponent<DGreenVirus>();
			}
			else if(_virusRank == VirusRank.C)
			{
				go.AddComponent<CGreenVirus>();
			}
			else if(_virusRank == VirusRank.B)
			{
				go.AddComponent<BGreenVirus>();
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}
		else//Special
		{
			if(_virusRank == VirusRank.D)
			{
			}
			else if(_virusRank == VirusRank.C)
			{
			}
			else if(_virusRank == VirusRank.B)
			{
			}
			else if(_virusRank == VirusRank.A)
			{
			}
			else//S
			{
			}
		}

		go.GetComponent<Virus>().DetermineSpawnPoint();
		go.GetComponent<Virus>().DetermineTargetPoint(Vector3.zero);
		return go;
	}

	public IEnumerator SpawnVirus(int _nbVirus, VirusType _virusType, VirusRank _virusRank)
	{
		for(int i=0;i<_nbVirus;i++)
		{
			yield return new WaitForSeconds(0.1f);
			CreateVirusAtRandom(_virusType,_virusRank);
		}
	}

	public IEnumerator SpawnVirusAt(int _nbVirus, VirusType _virusType, VirusRank _virusRank,Vector3 _pos)
	{
		for(int i=0;i<_nbVirus;i++)
		{
			yield return new WaitForSeconds(0.1f);
			CreateVirusAt(_virusType,_virusRank,_pos);
		}
	}
	
	IEnumerator StartSpawnVirus(int _nbVirus)
	{
		int blackProbability = (int)(100 * s_fBlackVirusRateSpawn);
		int redProbability = (int)(100 * s_fRedVirusRateSpawn);
		int greenProbability = (int)(100 * s_fGreenVirusRateSpawn);
		int blueProbability = (int)(100 * s_fBlueVirusRateSpawn);
		
		for(int i=0;i<_nbVirus;i++)
		{
			yield return new WaitForSeconds(0.1f);
			if(redProbability + greenProbability + blueProbability + blackProbability <= 100 && redProbability + greenProbability + blueProbability + blackProbability >= 0)
			{
				int rdm = Random.Range(1,99);

				int blackChance = 0 + blackProbability;
				int redChance = blackChance + redProbability;
				int greenChance = redChance + greenProbability;
				int blueChance = greenChance + blueProbability;

				if(rdm < blackChance)
				{
					CreateVirusAtRandom(VirusType.Black,VirusRank.D);
				}
				else if(rdm < redChance)
				{
					CreateVirusAtRandom(VirusType.Red,VirusRank.D);
				}
				else if(rdm < greenChance)
				{
					CreateVirusAtRandom(VirusType.Green,VirusRank.D);
				}
				else if(rdm < blueChance)
				{
					CreateVirusAtRandom(VirusType.Blue,VirusRank.D);
				}
				else
				{
					Debug.LogError("Virus spawn probability is wrong.");
				}
			}
			else
			{
				Debug.LogError("Virus spawn probability is wrong.");
			}
		}
	}
}
