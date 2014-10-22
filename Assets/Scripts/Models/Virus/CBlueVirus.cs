using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class CBlueVirus : CVirus
	{
		public CBlueVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Blue;
			base.m_eVirusRank = VirusRank.C;
			base.m_sName = "CBlueVirus";
		}

		public override void Attack()
		{
		}
	}
}
