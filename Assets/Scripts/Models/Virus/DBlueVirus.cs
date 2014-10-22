using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class DBlueVirus : DVirus
	{
		public DBlueVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Blue;
			base.m_eVirusRank = VirusRank.D;
			base.m_sName = "DBlueVirus";
		}

		public override void Attack()
		{
		}
	}
}
