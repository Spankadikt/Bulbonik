using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class BGreenVirus : BVirus
	{
		public BGreenVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Green;
			base.m_eVirusRank = VirusRank.B;
			base.m_sName = "BGreenVirus";
		}

		public override void Attack()
		{
		}
	}
}
