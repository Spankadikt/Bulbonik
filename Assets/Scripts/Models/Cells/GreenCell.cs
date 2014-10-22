using UnityEngine;
using System;
namespace AssemblyCSharp
{
	public class GreenCell : Cell
	{
		public GreenCell ():base()
		{
			base.m_eCellState = CellState.Available;
			base.m_eCellType = CellType.Green;
			base.m_sName = "GreenCell";
		}

	}
}

