using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class CRedVirus : CVirus
	{
		public CRedVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Red;
			base.m_eVirusRank = VirusRank.C;
			base.m_sName = "CRedVirus";
		}

		public override void Attack()
		{
		}
	}
}
