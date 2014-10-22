using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class AntibodiesManager : MonoBehaviour
{
	//VAR
	public static int s_nWave = 0;
	public static float s_fTimeSpawn = 60f;
	public static int s_nSpawnAntibody = 1;
	
	public static float s_fNormalAntibodyRateSpawn = 1f;
	
	public static ArrayList s_LstAntibodies;
	
	private static float m_fSpawnTimer = 0f;
	
	void Start ()
	{
		s_LstAntibodies = new ArrayList();
	}
	
	void Update ()
	{
		m_fSpawnTimer += Time.deltaTime;
		if(m_fSpawnTimer > s_fTimeSpawn)
		{
			m_fSpawnTimer = 0f;
			SpawnAntibodies();
		}
	}

	public static void CreateAntibodies(AntibodyType _antibodyType)
	{
		GameObject go = new GameObject();
		switch(_antibodyType)
		{
		case AntibodyType.Normal:
			go.AddComponent<NormalAntibody>();
			s_LstAntibodies.Add(go.GetComponent<NormalAntibody>());
			break;
		default:
			break;
		}
		
	}
	
	public static void SpawnAntibodies()
	{
		s_fTimeSpawn = Mathf.Clamp(s_fTimeSpawn-5,5,60);
		m_fSpawnTimer =0f;
		s_nWave++;
		int _nbAntibodies = (int)Mathf.Pow(2f,(float)s_nWave);

		int normalProbability = (int)(100 * s_fNormalAntibodyRateSpawn);
		
		for(int i=0;i<_nbAntibodies;i++)
		{
			if(normalProbability <= 100 && normalProbability >= 0)
			{
				int rdm = Random.Range(1,99);
				
				int normalChance = 0 + normalProbability;

				
				if(rdm < normalChance)
				{
					CreateAntibodies(AntibodyType.Normal);
				}
				else
				{
					Debug.LogError("Antibody spawn probability is wrong.");
				}
			}
			else
			{
				Debug.LogError("Antibody spawn probability is wrong.");
			}
		}
	}
	
	public static void AntibodiesSpawnProbabilityManager(AntibodyType _antibodyType, float _diffProbability)
	{
		switch(_antibodyType)
		{
		case AntibodyType.Normal:
			float resultNormal = s_fNormalAntibodyRateSpawn + _diffProbability;
			if(resultNormal >= 0 && resultNormal <= 1)
			{
				s_fNormalAntibodyRateSpawn += _diffProbability;
			}
			break;
		default:
			break;
		}
	}
}

