using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
	public static Vector3 s_v3PickMapCenterPos;
	public static Vector3 s_v3PickMapCornerTopLeft;
	public static Vector3 s_v3PickMapCornerTopRight;
	public static Vector3 s_v3PickMapCornerBotLeft;
	public static Vector3 s_v3PickMapCornerBotRight;

	public static Vector3 s_v3FightMapCenterPos;
	public static Vector3 s_v3FightMapCornerTopLeft;
	public static Vector3 s_v3FightMapCornerTopRight;
	public static Vector3 s_v3FightMapCornerBotLeft;
	public static Vector3 s_v3FightMapCornerBotRight;

	public static Vector3 s_v3GuiMapCenterPos;
	public static Vector3 s_v3GuiMapCornerTopLeft;
	public static Vector3 s_v3GuiMapCornerTopRight;
	public static Vector3 s_v3GuiMapCornerBotLeft;
	public static Vector3 s_v3GuiMapCornerBotRight;
	
	public static float s_fCamFocalHeight;
	public static float s_fCamFocalWidth;

	public static float s_fSwitchDistValue = 0.9f;
	
	// Use this for initialization
	void Start () {
		CalculateCamFocal ();
		CalculatePickMapBorder ();
		CalculateFightMapBorder ();
		CalculateGuiMapBorder ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CalculateCamFocal()
	{
		Camera cam = Camera.main;
		s_fCamFocalHeight = 2f * cam.orthographicSize;
		s_fCamFocalWidth = s_fCamFocalHeight * cam.aspect;
	}
	
	void CalculatePickMapBorder()
	{
		s_v3PickMapCenterPos = new Vector3(this.transform.position.x,this.transform.position.y,0);

		s_v3PickMapCornerTopLeft = new Vector3(s_v3PickMapCenterPos.x - (s_fCamFocalWidth*s_fSwitchDistValue)/2,s_v3PickMapCenterPos.y + s_fCamFocalHeight/2,0);
		s_v3PickMapCornerTopRight = new Vector3 (s_v3PickMapCenterPos.x + (s_fCamFocalWidth*s_fSwitchDistValue)/2 , s_v3PickMapCenterPos.y + s_fCamFocalHeight / 2, 0);
		s_v3PickMapCornerBotLeft = new Vector3(s_v3PickMapCenterPos.x - (s_fCamFocalWidth*s_fSwitchDistValue)/2, s_v3PickMapCenterPos.y - s_fCamFocalHeight/2,0);
        s_v3PickMapCornerBotRight = new Vector3(s_v3PickMapCenterPos.x + (s_fCamFocalWidth*s_fSwitchDistValue)/2 , s_v3PickMapCenterPos.y - s_fCamFocalHeight/2,0);
	}

	void CalculateFightMapBorder()
	{
		s_v3FightMapCenterPos = new Vector3(s_v3PickMapCenterPos.x + s_fCamFocalWidth * s_fSwitchDistValue,s_v3PickMapCenterPos.y,0);

		s_v3FightMapCornerTopLeft = new Vector3(s_v3FightMapCenterPos.x - (s_fCamFocalWidth*s_fSwitchDistValue)/2 ,s_v3FightMapCenterPos.y + s_fCamFocalHeight/2,0);
		s_v3FightMapCornerTopRight = new Vector3 (s_v3FightMapCenterPos.x + (s_fCamFocalWidth*s_fSwitchDistValue)/2  , s_v3FightMapCenterPos.y + s_fCamFocalHeight / 2, 0);
		s_v3FightMapCornerBotLeft = new Vector3(s_v3FightMapCenterPos.x - (s_fCamFocalWidth*s_fSwitchDistValue)/2 , s_v3FightMapCenterPos.y - s_fCamFocalHeight/2,0);
		s_v3FightMapCornerBotRight = new Vector3(s_v3FightMapCenterPos.x + (s_fCamFocalWidth*s_fSwitchDistValue)/2 , s_v3FightMapCenterPos.y - s_fCamFocalHeight/2,0);
	}

	void CalculateGuiMapBorder()
	{
		s_v3GuiMapCenterPos = new Vector3(s_v3PickMapCenterPos.x - s_fCamFocalWidth / 2 + s_fCamFocalWidth*(1-s_fSwitchDistValue)/4,s_v3PickMapCenterPos.y,0);
		
		s_v3GuiMapCornerTopLeft = new Vector3(s_v3GuiMapCenterPos.x - s_fCamFocalWidth*(1-s_fSwitchDistValue)/4 ,s_v3GuiMapCenterPos.y + s_fCamFocalHeight/2,0);
		s_v3GuiMapCornerTopRight = new Vector3 (s_v3GuiMapCenterPos.x + s_fCamFocalWidth*(1-s_fSwitchDistValue)/4  , s_v3GuiMapCenterPos.y + s_fCamFocalHeight / 2, 0);
		s_v3GuiMapCornerBotLeft = new Vector3(s_v3GuiMapCenterPos.x - s_fCamFocalWidth*(1-s_fSwitchDistValue)/4 , s_v3GuiMapCenterPos.y - s_fCamFocalHeight/2,0);
		s_v3GuiMapCornerBotRight = new Vector3(s_v3GuiMapCenterPos.x + s_fCamFocalWidth*(1-s_fSwitchDistValue)/4 , s_v3GuiMapCenterPos.y - s_fCamFocalHeight/2,0);
	}


	private static Vector3 GetRandomPointInMap(Vector3 botLeft, Vector3 topLeft, Vector3 botRight, Vector3 topRight)
	{
		Vector3 leftLimitPoint = (botLeft + topLeft) / 2;
		Vector3 rightLimitPoint = (botRight + topRight) / 2;
		Vector3 topLimitPoint = (topLeft + topRight) / 2;
		Vector3 botLimitPoint = (botLeft + botRight) / 2;
		
		int leftLimit = (int)leftLimitPoint.x;
		int rightLimit = (int)rightLimitPoint.x;
		int topLimit = (int)topLimitPoint.y;
		int botLimit = (int)botLimitPoint.y;

		return new Vector3 (Random.Range (leftLimit, rightLimit), Random.Range (botLimit, topLimit), 0);
	}

	public static Vector3 GetRandomPointInPickMap()
	{
		return GetRandomPointInMap (MapManager.s_v3PickMapCornerBotLeft, MapManager.s_v3PickMapCornerTopLeft, MapManager.s_v3PickMapCornerBotRight, MapManager.s_v3PickMapCornerTopRight);
	}

	public static Vector3 GetRandomPointInFightMap()
	{
		return GetRandomPointInMap(MapManager.s_v3FightMapCornerBotLeft,MapManager.s_v3FightMapCornerTopLeft,MapManager.s_v3FightMapCornerBotRight,MapManager.s_v3FightMapCornerTopRight);
	}

	public static Vector3 GetRandomPointInLine(Vector3 corner1, Vector3 corner2)
	{
		//return new Vector3 (((corner1 + corner2) / 2).x, Random.Range (corner1.y, corner2.y), 0);
		Vector3 rdmPoint = Vector3.Lerp (corner1, corner2, Random.value);
		return rdmPoint;
	}

	public static void SideScroll()
	{
	}
}

