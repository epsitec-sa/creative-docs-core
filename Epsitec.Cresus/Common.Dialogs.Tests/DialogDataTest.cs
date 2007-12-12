using NUnit.Framework;

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.UI;
using Epsitec.Common.Widgets;

using Demo.Demo5juin.Entities;

namespace Epsitec.Common.Dialogs
{
	[TestFixture]
	public class DialogDataTest
	{
		[Test]
		public void Check01DialogModeIsolated()
		{
			EntityContext context = EntityContext.Current;
			PrixEntity prix1 = context.CreateEmptyEntity<PrixEntity> ();
			prix1.Monnaie = context.CreateEmptyEntity<MonnaieEntity> ();
			prix1.Ht = 10.0M;
			prix1.Monnaie.Désignation = "CHF";

			DialogData data = new DialogData (prix1, DialogDataMode.Isolated);

			PrixEntity prix2 = data.Data as PrixEntity;

			Assert.AreEqual (10.0M, prix2.Ht);
			Assert.AreEqual ("CHF", prix2.Monnaie.Désignation);

			prix2.Ht = 15.0M;
			prix2.Monnaie.Désignation = "EUR";
			
			Assert.AreEqual (10.0M, prix1.Ht);
			Assert.AreEqual ("CHF", prix1.Monnaie.Désignation);
			Assert.AreEqual (15.0M, prix2.Ht);
			Assert.AreEqual ("EUR", prix2.Monnaie.Désignation);
		}
		
		[Test]
		public void Check02DialogModeRealTime()
		{
			EntityContext context = EntityContext.Current;
			AbstractEntity entity = context.CreateEmptyEntity<PrixEntity> ();
			DialogData data = new DialogData (entity, DialogDataMode.RealTime);

			PrixEntity prix = data.Data as PrixEntity;
		}
	}
}
