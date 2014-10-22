using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class DRedVirus : DVirus
	{
		public DRedVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Red;
			base.m_eVirusRank = VirusRank.D;
			base.m_sName = "DRedVirus";
		}

		public override void Attack()
		{
		}
	}
}

