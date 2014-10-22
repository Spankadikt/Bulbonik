using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class CGreenVirus : CVirus
	{
		public CGreenVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Green;
			base.m_eVirusRank = VirusRank.C;
			base.m_sName = "CGreenVirus";
		}

		public override void Attack()
		{
		}
	}
}
