using UnityEngine;
using System;
namespace AssemblyCSharp
{
	public class RedCell : Cell
	{
		public RedCell ():base()
		{
			base.m_eCellState = CellState.Available;
			base.m_eCellType = CellType.Red;
			base.m_sName = "RedCell";
		}

	}
}

