using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class BBlueVirus : BVirus
	{
		public BBlueVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Blue;
			base.m_eVirusRank = VirusRank.B;
			base.m_sName = "BBlueVirus";
		}

		public override void Attack()
		{
		}
	}
}

