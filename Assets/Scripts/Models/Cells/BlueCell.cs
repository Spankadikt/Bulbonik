using UnityEngine;
using System;
namespace AssemblyCSharp
{
	public class BlueCell : Cell
	{
		public BlueCell ():base()
		{
			base.m_eCellState = CellState.Available;
			base.m_eCellType = CellType.Blue;
			base.m_sName = "BlueCell";
		}

	}
}

