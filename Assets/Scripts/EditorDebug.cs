using UnityEngine;
using System.Collections;

public class EditorDebug : MonoBehaviour {

	public bool DrawPickMapBorder;
	public bool DrawFightMapBorder;
	public bool DrawGuiMapBorder;

	public bool DrawPickMapRectangle;
	public bool DrawFightMapRectangle;
	public bool DrawGuiMapRectangle;

	public bool DrawCells;
	public static bool s_bDrawCells;

	public bool DrawAntibodies;
	public static bool s_bDrawAntibodies;

	public bool DrawVirus;
	public static bool s_bDrawVirus;

	void OnDrawGizmos()
	{
		s_bDrawCells = DrawCells;
		s_bDrawAntibodies = DrawAntibodies;
		s_bDrawVirus = DrawVirus;

		if(DrawPickMapBorder)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (MapManager.s_v3PickMapCornerTopLeft, MapManager.s_v3PickMapCornerTopRight);
			Gizmos.DrawLine (MapManager.s_v3PickMapCornerTopRight, MapManager.s_v3PickMapCornerBotRight);
			Gizmos.DrawLine (MapManager.s_v3PickMapCornerBotRight, MapManager.s_v3PickMapCornerBotLeft);
			Gizmos.DrawLine (MapManager.s_v3PickMapCornerBotLeft, MapManager.s_v3PickMapCornerTopLeft);
		}
		if(DrawFightMapBorder)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine (MapManager.s_v3FightMapCornerTopLeft, MapManager.s_v3FightMapCornerTopRight);
			Gizmos.DrawLine (MapManager.s_v3FightMapCornerTopRight, MapManager.s_v3FightMapCornerBotRight);
			Gizmos.DrawLine (MapManager.s_v3FightMapCornerBotRight, MapManager.s_v3FightMapCornerBotLeft);
			Gizmos.DrawLine (MapManager.s_v3FightMapCornerBotLeft, MapManager.s_v3FightMapCornerTopLeft);
		}
		if(DrawGuiMapBorder)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine (MapManager.s_v3GuiMapCornerTopLeft, MapManager.s_v3GuiMapCornerTopRight);
			Gizmos.DrawLine (MapManager.s_v3GuiMapCornerTopRight, MapManager.s_v3GuiMapCornerBotRight);
			Gizmos.DrawLine (MapManager.s_v3GuiMapCornerBotRight, MapManager.s_v3GuiMapCornerBotLeft);
			Gizmos.DrawLine (MapManager.s_v3GuiMapCornerBotLeft, MapManager.s_v3GuiMapCornerTopLeft);
		}
		if(DrawPickMapRectangle)
		{
			Gizmos.color = new Color (1f, 0.6f, 0.8f, 0.5f);
			Gizmos.DrawCube (MapManager.s_v3PickMapCenterPos, new Vector3(MapManager.s_fCamFocalWidth*MapManager.s_fSwitchDistValue,MapManager.s_fCamFocalHeight,0));
		}
		if(DrawFightMapRectangle)
		{
			Gizmos.color = new Color (0.6f, 0.6f, 1f, 0.5f);
			Gizmos.DrawCube (MapManager.s_v3FightMapCenterPos, new Vector3(MapManager.s_fCamFocalWidth,MapManager.s_fCamFocalHeight,0));
		}
		if(DrawGuiMapRectangle)
		{
			Gizmos.color = new Color (0.3f, 1f, 1f, 0.5f);
			Gizmos.DrawCube (MapManager.s_v3GuiMapCenterPos, new Vector3(MapManager.s_fCamFocalWidth*(1-MapManager.s_fSwitchDistValue)/2,MapManager.s_fCamFocalHeight,0));
		}
	}
}
