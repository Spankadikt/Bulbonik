using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class BBlackVirus : BVirus
	{
		public BBlackVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Black;
			base.m_eVirusRank = VirusRank.B;
			base.m_sName = "BBlackVirus";
		}

		public override void Attack()
		{
		}
	}
}
