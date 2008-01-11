using NUnit.Framework;

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.UI;
using Epsitec.Common.Widgets;

using System.Collections.Generic;

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
			prix1.Monnaie.TauxChangeVersChf = 1.00M;
			
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

			List<string> results = new List<string> ();

			data.ForEachChange (change =>
				{
					results.Add (string.Format ("Change {0} from {1} to {2}", change.Path, change.OldValue, change.NewValue));
					return true;
				});

			Collection.CompareEqual (results,
				new string[]
				{
					"Change [630G].[630A] from CHF to EUR",
					"Change [630H] from 10.0 to 15.0"
				});

			Assert.AreEqual (3, Collection.Count (data.Changes));
			data.RevertChanges ();
			Assert.AreEqual (3, Collection.Count (data.Changes));

			Assert.AreEqual (10.0M, prix1.Ht);
			Assert.AreEqual ("CHF", prix1.Monnaie.Désignation);
			Assert.AreEqual (1.00M, prix1.Monnaie.TauxChangeVersChf);
			Assert.AreEqual (10.0M, prix2.Ht);
			Assert.AreEqual ("CHF", prix2.Monnaie.Désignation);
			Assert.AreEqual (3, Collection.Count (data.Changes));
//-			Assert.AreEqual (1.00M, prix2.Monnaie.TauxChangeVersChf);	//	read access => snapshot
//-			Assert.AreEqual (4, Collection.Count (data.Changes));

			MonnaieEntity monnaie = context.CreateEmptyEntity<MonnaieEntity> ();
			monnaie.Désignation = "USD";
			monnaie.TauxChangeVersChf = 1.08M;

			prix2.Monnaie.TauxChangeVersChf = 2.00M;
			Assert.AreEqual (4, Collection.Count (data.Changes));
			
			prix2.Monnaie = monnaie;
			prix2.Monnaie.TauxChangeVersChf = 1.06M;

			Assert.AreEqual (4, Collection.Count (data.Changes));

			Assert.AreNotEqual (monnaie, prix1.Monnaie);
			Assert.AreEqual (monnaie, prix2.Monnaie);
			
			results.Clear ();
			data.ForEachChange (change =>
				{
					results.Add (string.Format ("Change {0} from {1} to {2}", change.Path, change.OldValue, change.NewValue));
					return true;
				});

			Assert.AreEqual (1, results.Count);
			Assert.IsTrue (results[0].StartsWith ("Change [630G] from "));
			
			data.RevertChanges ();

			Assert.AreEqual (10.0M, prix1.Ht);
			Assert.AreEqual ("CHF", prix1.Monnaie.Désignation);
			Assert.AreEqual (1.00M, prix1.Monnaie.TauxChangeVersChf);
			Assert.AreEqual (10.0M, prix2.Ht);
			Assert.AreEqual ("CHF", prix2.Monnaie.Désignation);
			Assert.AreEqual (1.00M, prix2.Monnaie.TauxChangeVersChf);

			Assert.AreEqual ("USD", monnaie.Désignation);
			Assert.AreEqual (1.06M, monnaie.TauxChangeVersChf);
			
			Assert.AreEqual (4, Collection.Count (data.Changes));

			prix2.Monnaie = null;

			Assert.IsNull (prix2.Monnaie);
			data.RevertChanges ();
			Assert.IsNotNull (prix2.Monnaie);
		}

		[Test]
		public void Check02DialogModeRealTime()
		{
			EntityContext context = EntityContext.Current;
			PrixEntity prix1 = context.CreateEmptyEntity<PrixEntity> ();
			prix1.Monnaie = context.CreateEmptyEntity<MonnaieEntity> ();
			prix1.Ht = 10.0M;
			prix1.Monnaie.Désignation = "CHF";
			prix1.Monnaie.TauxChangeVersChf = 1.00M;

			DialogData data = new DialogData (prix1, DialogDataMode.RealTime);

			PrixEntity prix2 = data.Data as PrixEntity;
			MonnaieEntity monnaiePrix1 = prix1.Monnaie;
			MonnaieEntity monnaiePrix2 = prix2.Monnaie;

			Assert.AreNotEqual (monnaiePrix1, monnaiePrix2);

			Assert.AreEqual (10.0M, prix2.Ht);
			Assert.AreEqual ("CHF", prix2.Monnaie.Désignation);

			prix2.Ht = 15.0M;
			prix2.Monnaie.Désignation = "EUR";

			Assert.AreEqual (15.0M, prix1.Ht);
			Assert.AreEqual ("EUR", prix1.Monnaie.Désignation);
			Assert.AreEqual (monnaiePrix1, prix1.Monnaie);
			Assert.AreEqual (15.0M, prix2.Ht);
			Assert.AreEqual ("EUR", prix2.Monnaie.Désignation);
			Assert.AreEqual (monnaiePrix2, prix2.Monnaie);

			List<string> results = new List<string> ();

			data.ForEachChange (change =>
				{
					results.Add (string.Format ("Change {0} from {1} to {2}", change.Path, change.OldValue, change.NewValue));
					return true;
				});

			Collection.CompareEqual (results,
				new string[]
				{
					"Change [630G].[630A] from CHF to EUR",
					"Change [630H] from 10.0 to 15.0"
				});

			Assert.AreEqual (3, Collection.Count (data.Changes));
			data.RevertChanges ();
			Assert.AreEqual (3, Collection.Count (data.Changes));

			Assert.AreEqual (10.0M, prix1.Ht);
			Assert.AreEqual ("CHF", prix1.Monnaie.Désignation);
			Assert.AreEqual (1.00M, prix1.Monnaie.TauxChangeVersChf);
			Assert.AreEqual (10.0M, prix2.Ht);
			Assert.AreEqual ("CHF", prix2.Monnaie.Désignation);
			Assert.AreEqual (3, Collection.Count (data.Changes));
//-			Assert.AreEqual (1.00M, prix2.Monnaie.TauxChangeVersChf);	//	read access => snapshot
//-			Assert.AreEqual (4, Collection.Count (data.Changes));

			MonnaieEntity monnaie = context.CreateEmptyEntity<MonnaieEntity> ();
			monnaie.Désignation = "USD";
			monnaie.TauxChangeVersChf = 1.08M;

			prix2.Monnaie.TauxChangeVersChf = 2.00M;
			Assert.AreEqual (4, Collection.Count (data.Changes));

			prix2.Monnaie = monnaie;
			prix2.Monnaie.TauxChangeVersChf = 1.06M;

			Assert.AreEqual (4, Collection.Count (data.Changes));

			Assert.AreEqual (monnaie, prix1.Monnaie);
			Assert.AreNotEqual (monnaie, prix2.Monnaie);				//	because of the proxy
			Assert.IsNotNull ((prix2.Monnaie as IEntityProxyProvider).GetEntityProxy ());

			results.Clear ();
			data.ForEachChange (change =>
				{
					results.Add (string.Format ("Change {0} from {1} to {2}", change.Path, change.OldValue, change.NewValue));
					return true;
				});

			Assert.AreEqual (1, results.Count);
			Assert.IsTrue (results[0].StartsWith ("Change [630G] from "));

			data.RevertChanges ();

			Assert.AreEqual (monnaiePrix1, prix1.Monnaie);
			Assert.AreEqual (monnaiePrix2, prix2.Monnaie);

			Assert.AreEqual (10.0M, prix1.Ht);
			Assert.AreEqual ("CHF", prix1.Monnaie.Désignation);
			Assert.AreEqual (1.00M, prix1.Monnaie.TauxChangeVersChf);
			Assert.AreEqual (10.0M, prix2.Ht);
			Assert.AreEqual ("CHF", prix2.Monnaie.Désignation);
			Assert.AreEqual (1.00M, prix2.Monnaie.TauxChangeVersChf);

			Assert.AreEqual ("USD", monnaie.Désignation);
			Assert.AreEqual (1.06M, monnaie.TauxChangeVersChf);

			Assert.AreEqual (4, Collection.Count (data.Changes));

			prix2.Monnaie = null;

			Assert.IsNull (prix1.Monnaie);
			Assert.IsNull (prix2.Monnaie);

			prix2.Monnaie = monnaie;

			Assert.IsNotNull (prix1.Monnaie);
			Assert.IsNotNull (prix2.Monnaie);
			Assert.AreEqual (1.06M, prix1.Monnaie.TauxChangeVersChf);
			Assert.AreEqual (1.06M, prix2.Monnaie.TauxChangeVersChf);
		}
		
		[Test]
		public void Check03DialogModeTransparent()
		{
			EntityContext context = EntityContext.Current;
			PrixEntity prix1 = context.CreateEmptyEntity<PrixEntity> ();
			prix1.Monnaie = context.CreateEmptyEntity<MonnaieEntity> ();
			prix1.Ht = 10.0M;
			prix1.Monnaie.Désignation = "CHF";

			DialogData data = new DialogData (prix1, DialogDataMode.Transparent);

			PrixEntity prix2 = data.Data as PrixEntity;

			Assert.AreEqual (10.0M, prix2.Ht);
			Assert.AreEqual ("CHF", prix2.Monnaie.Désignation);

			MonnaieEntity monnaie = prix2.Monnaie;

			prix2.Ht = 15.0M;
			prix2.Monnaie.Désignation = "EUR";

			Assert.AreEqual (15.0M, prix1.Ht);
			Assert.AreEqual ("EUR", prix1.Monnaie.Désignation);

			Assert.AreEqual (prix1, prix2);
			Assert.AreEqual (0, Collection.Count (data.Changes));
			
			prix2.Monnaie = null;

			Assert.IsNull (prix1.Monnaie);
			Assert.IsNull (prix2.Monnaie);

			prix2.Monnaie = monnaie;

			Assert.IsNotNull (prix1.Monnaie);
			Assert.IsNotNull (prix2.Monnaie);
			Assert.AreEqual ("EUR", prix1.Monnaie.Désignation);
			Assert.AreEqual ("EUR", prix2.Monnaie.Désignation);
		}
	}
}
