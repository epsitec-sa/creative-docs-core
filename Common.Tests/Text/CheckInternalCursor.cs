/*
This file is part of CreativeDocs.

Copyright © 2003-2024, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland

CreativeDocs is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

CreativeDocs is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


using Epsitec.Common.Text;
using Epsitec.Common.Text.Internal;
using NUnit.Framework;

namespace Epsitec.Common.Tests.Text
{
    /// <summary>
    /// Summary description for Checkursor.
    /// </summary>
    [TestFixture]
    public sealed class Checkursor
    {
        [Test]
        public static void RunTests()
        {
            Cursor c1 = new Cursor();
            Cursor c2 = new Cursor(c1);
            Cursor c3 = c1;

            Assert.IsTrue(c1.CursorState == CursorState.Copied);
            Assert.IsTrue(c2.CursorState == CursorState.Copied);
            Assert.IsTrue(c3.CursorState == CursorState.Copied);

            c3.DefineCursorState(CursorState.Allocated);

            Cursor c4 = c3;
            Cursor c5 = new Cursor(c3);

            Assert.IsTrue(c3.CursorState == CursorState.Allocated);
            Assert.IsTrue(c4.CursorState == CursorState.Allocated);
            Assert.IsTrue(c5.CursorState == CursorState.Copied);

            CursorInfo[] infos = new CursorInfo[4];

            infos[0] = new CursorInfo(0, 10, 0);
            infos[1] = new CursorInfo(5, 10, 1);
            infos[2] = new CursorInfo(3, 10, -1);
            infos[3] = new CursorInfo(8, 8, 1);

            System.Array.Sort(infos, CursorInfo.PositionComparer);

            Assert.IsTrue(infos[0].Position == 8);
            Assert.IsTrue(infos[1].Position == 10);
            Assert.IsTrue(infos[2].Position == 10);
            Assert.IsTrue(infos[3].Position == 10);
            Assert.IsTrue(infos[1].Direction == -1);
            Assert.IsTrue(infos[2].Direction == 0);
            Assert.IsTrue(infos[3].Direction == 1);

            System.Array.Sort(infos, CursorInfo.CursorIdComparer);

            Assert.IsTrue(infos[0].CursorId == 0);
            Assert.IsTrue(infos[1].CursorId == 3);
            Assert.IsTrue(infos[2].CursorId == 5);
            Assert.IsTrue(infos[3].CursorId == 8);
        }

        public static void OptimizerTest()
        {
            CursorId id1 = 10;
            CursorId id2 = id1;
            Checkursor.DumpCursorId(id2);
        }

        private static void DumpCursorId(int value)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Cursor ID is {0}.", value));
        }
    }
}
