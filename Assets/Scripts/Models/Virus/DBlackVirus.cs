using UnityEngine;
using System.Collections;
using AssemblyCSharp;
namespace AssemblyCSharp
{
	public class DBlackVirus : DVirus
	{
		public DBlackVirus():base()
		{
			base.m_eVirusState = VirusState.Idle;
			base.m_eVirusType = VirusType.Black;
			base.m_eVirusRank = VirusRank.D;
			base.m_sName = "DBlackVirus";
		}

		public override void Attack()
		{
		}
	}
}
