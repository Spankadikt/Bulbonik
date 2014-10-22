using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CellsManager : MonoBehaviour
{
	//VAR
	public static int s_nMaxCells = 10;
	public static float s_fTimeSpawn = 2f;
	public static int s_nSpawnCell = 1;

	public static float s_fRedCellRateSpawn = 0.33f;
	public static float s_fGreenCellRateSpawn = 0.33f;
	public static float s_fBlueCellRateSpawn = 0.33f;

	public static ArrayList s_LstCells;

	private float m_fSpawnTimer = 0f;

	// Use this for initialization
	void Start ()
	{
		s_LstCells = new ArrayList();
	}

	// Update is called once per frame
	void Update ()
	{
		m_fSpawnTimer += Time.deltaTime;
		if(s_LstCells.Count < s_nMaxCells && m_fSpawnTimer > s_fTimeSpawn && VirusesManager.s_LstViruses.Count < VirusesManager.s_nMaxVirus)
		{
			m_fSpawnTimer = 0f;
			SpawnCells(s_nSpawnCell);
		}
	}

	public void CreateCell(CellType _cellType)
	{
		GameObject go = new GameObject();
		switch(_cellType)
		{
		case CellType.Blue:
			go.AddComponent<BlueCell>();
			break;
		case CellType.Green:
			go.AddComponent<GreenCell>();
			break;
		case CellType.Red:
			go.AddComponent<RedCell>();
			break;
		default:
			break;
		}

	}

	private void SpawnCells(int _nbCell)
	{
		int redProbability = (int)(100 * s_fRedCellRateSpawn);
		int greenProbability = (int)(100 * s_fGreenCellRateSpawn);
		int blueProbability = (int)(100 * s_fBlueCellRateSpawn);

		for(int i=0;i<_nbCell;i++)
		{
			if(redProbability + greenProbability + blueProbability <= 100 && redProbability + greenProbability + blueProbability >= 0)
			{
				int rdm = Random.Range(1,99);

				int redChance = 0 + redProbability;
				int greenChance = redChance + greenProbability;
				int blueChance = greenChance + blueProbability;

				if(rdm < redChance)
				{
					CreateCell(CellType.Red);
				}
				else if(rdm < greenChance)
				{
					CreateCell(CellType.Green);
				}
				else if(rdm < blueChance)
				{
					CreateCell(CellType.Blue);
				}
				else
				{
					Debug.LogError("Cell spawn probability is wrong.");
				}
			}
			else
			{
				Debug.LogError("Cell spawn probability is wrong.");
			}
		}
	}

	public static void CellsSpawnProbabilityManager(CellType _cellType, float _diffProbability)
	{
		switch(_cellType)
		{
		case CellType.Blue:
			float resultBlue = s_fBlueCellRateSpawn + _diffProbability;
			if(resultBlue >= 0 && resultBlue <= 1)
			{
				s_fBlueCellRateSpawn += _diffProbability;
				s_fGreenCellRateSpawn -= _diffProbability/2;
				s_fRedCellRateSpawn -= _diffProbability/2;
			}
			break;
		case CellType.Green:
			float resultGreen = s_fGreenCellRateSpawn + _diffProbability;
			if(resultGreen >= 0 && resultGreen <= 1)
			{
				s_fBlueCellRateSpawn -= _diffProbability/2;
				s_fGreenCellRateSpawn += _diffProbability;
				s_fRedCellRateSpawn -= _diffProbability/2;
			}
			break;
		case CellType.Red:
			float resultRed = s_fRedCellRateSpawn + _diffProbability;
			if(resultRed >= 0 && resultRed <= 1)
			{
				s_fBlueCellRateSpawn -= _diffProbability/2;
				s_fGreenCellRateSpawn -= _diffProbability/2;
				s_fRedCellRateSpawn += _diffProbability;
			}
			break;
		default:
			break;
		}
	}
}

