using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class CBlackVirus : CVirus
	{
		public CBlackVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Black;
			base.m_eVirusRank = VirusRank.C;
			base.m_sName = "CBlackVirus";
		}

		public override void Attack()
		{
		}
	}
}
