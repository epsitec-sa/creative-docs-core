using NUnit.Framework;

namespace Epsitec.Common.Support
{
	[TestFixture] public class IOTest
	{
		[Test] public void CheckBZip2()
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes (IOTest.SampleText);
			
			System.IO.MemoryStream target = new System.IO.MemoryStream ();
			System.IO.Stream   compressor = IO.CompressionStream.CreateBZip2 (target);
			
			compressor.Write (buffer, 0, buffer.Length);
			compressor.Close ();
			
			byte[] data = target.ToArray ();
			long length = data.Length;
			
			System.Console.Out.WriteLine ("Source size : {0} bytes", buffer.Length);
			System.Console.Out.WriteLine ("Compressed size : {0} bytes, using BZIP2", length);
			
			System.IO.MemoryStream source = new System.IO.MemoryStream (data);
			System.IO.Stream decompressor = IO.DecompressionStream.CreateAuto (source);
			
			byte[] read = new byte[buffer.Length];
			
			decompressor.Read (read, 0, buffer.Length);
			decompressor.Close ();
			
			for (int i = 0; i < buffer.Length; i++)
			{
				Assertion.AssertEquals (string.Format ("offset {0}: {1} != {2}", i, (char) buffer[i], (char) read[i]), buffer[i], read[i]);
			}
		}
		
		[Test] public void CheckZip()
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes (IOTest.SampleText);
			
			System.IO.MemoryStream target = new System.IO.MemoryStream ();
			System.IO.Stream   compressor = IO.CompressionStream.CreateZip (target, "test");
			
			compressor.Write (buffer, 0, buffer.Length);
			compressor.Close ();
			
			byte[] data = target.ToArray ();
			long length = data.Length;
			
			System.Console.Out.WriteLine ("Compressed size : {0} bytes, using ZIP", length);
			
			System.IO.MemoryStream source = new System.IO.MemoryStream (data);
			System.IO.Stream decompressor = IO.DecompressionStream.CreateAuto (source);
			
			byte[] read = new byte[buffer.Length];
			
			decompressor.Read (read, 0, buffer.Length);
			decompressor.Close ();
			
			for (int i = 0; i < buffer.Length; i++)
			{
				Assertion.AssertEquals (string.Format ("offset {0}: {1} != {2}", i, (char) buffer[i], (char) read[i]), buffer[i], read[i]);
			}
		}
		
		[Test] public void CheckDeflateLevel1()
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes (IOTest.SampleText);
			
			System.IO.MemoryStream target = new System.IO.MemoryStream ();
			System.IO.Stream   compressor = IO.CompressionStream.CreateDeflate (target, 1);
			
			compressor.Write (buffer, 0, buffer.Length);
			compressor.Close ();
			
			byte[] data = target.ToArray ();
			long length = data.Length;
			
			System.Console.Out.WriteLine ("Compressed size : {0} bytes, using DEFLATE-1", length);
			
			System.IO.MemoryStream source = new System.IO.MemoryStream (data);
			System.IO.Stream decompressor = IO.DecompressionStream.CreateAuto (source);
			
			byte[] read = new byte[buffer.Length];
			
			decompressor.Read (read, 0, buffer.Length);
			decompressor.Close ();
			
			for (int i = 0; i < buffer.Length; i++)
			{
				Assertion.AssertEquals (string.Format ("offset {0}: {1} != {2}", i, (char) buffer[i], (char) read[i]), buffer[i], read[i]);
			}
		}
		
		[Test] public void CheckDeflateLevel9()
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes (IOTest.SampleText);
			
			System.IO.MemoryStream target = new System.IO.MemoryStream ();
			System.IO.Stream   compressor = IO.CompressionStream.CreateDeflate (target, 9);
			
			compressor.Write (buffer, 0, buffer.Length);
			compressor.Close ();
			
			byte[] data = target.ToArray ();
			long length = data.Length;
			
			System.Console.Out.WriteLine ("Compressed size : {0} bytes, using DEFLATE-9", length);
			
			System.IO.MemoryStream source = new System.IO.MemoryStream (data);
			System.IO.Stream decompressor = IO.DecompressionStream.CreateAuto (source);
			
			byte[] read = new byte[buffer.Length];
			
			decompressor.Read (read, 0, buffer.Length);
			decompressor.Close ();
			
			for (int i = 0; i < buffer.Length; i++)
			{
				Assertion.AssertEquals (string.Format ("offset {0}: {1} != {2}", i, (char) buffer[i], (char) read[i]), buffer[i], read[i]);
			}
		}
		
		[Test] public void CheckGZip()
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes (IOTest.SampleText);
			
			System.IO.MemoryStream target = new System.IO.MemoryStream ();
			System.IO.Stream   compressor = IO.CompressionStream.CreateGZip (target);
			
			compressor.Write (buffer, 0, buffer.Length);
			compressor.Close ();
			
			byte[] data = target.ToArray ();
			long length = data.Length;
			
			System.Console.Out.WriteLine ("Compressed size : {0} bytes, using GZIP", length);
			
			System.IO.MemoryStream source = new System.IO.MemoryStream (data);
			System.IO.Stream decompressor = IO.DecompressionStream.CreateAuto (source);
			
			byte[] read = new byte[buffer.Length];
			
			decompressor.Read (read, 0, buffer.Length);
			decompressor.Close ();
			
			for (int i = 0; i < buffer.Length; i++)
			{
				Assertion.AssertEquals (string.Format ("offset {0}: {1} != {2}", i, (char) buffer[i], (char) read[i]), buffer[i], read[i]);
			}
		}
		
		[Test] public void CheckChecksumCRC()
		{
			IO.IChecksum checksum = IO.Checksum.CreateCrc32 ();
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes (IOTest.SampleText);
			
			Assertion.AssertEquals (0, checksum.Value);
			
			checksum.Reset ();
			Assertion.AssertEquals (0, checksum.Value);
			checksum.Update (buffer);
			
			long value_0 = checksum.Value;
			
			byte b10 = buffer[10];
			
			buffer[10] = buffer[20];
			buffer[20] = b10;
			
			checksum.Reset ();
			Assertion.AssertEquals (0, checksum.Value);
			checksum.Update (buffer);
			
			long value_1 = checksum.Value;
			
			System.Console.Out.WriteLine ();
			System.Console.Out.WriteLine ("CRC32 : {0:X}, after byte swap {1:X}", value_0, value_1);
			
			Assertion.Assert (value_0 != value_1);
		}
		
		[Test] public void CheckChecksumAdler()
		{
			IO.IChecksum checksum = IO.Checksum.CreateAdler32 ();
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes (IOTest.SampleText);
			
			Assertion.AssertEquals (1, checksum.Value);
			
			checksum.Reset ();
			Assertion.AssertEquals (1, checksum.Value);
			checksum.Update (buffer);
			
			long value_0 = checksum.Value;
			
			byte b10 = buffer[10];
			
			buffer[10] = buffer[20];
			buffer[20] = b10;
			
			checksum.Reset ();
			Assertion.AssertEquals (1, checksum.Value);
			checksum.Update (buffer);
			
			long value_1 = checksum.Value;
			
			System.Console.Out.WriteLine ("Adler : {0:X}, after byte swap {1:X}", value_0, value_1);
			
			Assertion.Assert (value_0 != value_1);
		}
		
		
		private static string					SampleText
		{
			get
			{
				return "Ceci est un petit essai qui devrait permettre de comparer la qualit� de la "
					 + "compression de divers algorithmes, appliqu�s � un morceau de texte relativement "
					 + "simple. L'utilisation de tags <italique>XML</italique> dans un tel <gras>texte</gras> "
					 + "est quelque peu <italique>artificielle</italique>, mais c'est un <gras>test</gras>, "
					 + "alors que veut-on de plus&#160;? Voici donc le XML alibi&#160;:\n"
					 + "<root>\n"
					+ "  <element name='abc'>\n"
					+ "    <node arg='1'/>\n"
					+ "    <node arg='2'/>\n"
					+ "    <node arg='3'/>\n"
					+ "    <node arg='4'/>\n"
					+ "    <node arg='5'/>\n"
					+ "    <node arg='6'/>\n"
					+ "  </element>\n"
					+ "  <element name='xyz'>\n"
					+ "    <node arg='3'/>\n"
					+ "    <node arg='6'/>\n"
					+ "    <node arg='8'/>\n"
					+ "    <node arg='10'/>\n"
					+ "  </element>\n"
					+ "</root>\n";
			}
		}
	}
}
