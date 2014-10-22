using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class DGreenVirus : DVirus
	{
		public DGreenVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Green;
			base.m_eVirusRank = VirusRank.D;
			base.m_sName = "DGreenVirus";
		}

		public override void Attack()
		{
		}
	}
}
