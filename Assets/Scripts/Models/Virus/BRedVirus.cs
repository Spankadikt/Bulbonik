using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class BRedVirus : BVirus
	{
		public BRedVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Red;
			base.m_eVirusRank = VirusRank.B;
			base.m_sName = "BRedVirus";
		}

		public override void Attack()
		{
		}
	}
}
