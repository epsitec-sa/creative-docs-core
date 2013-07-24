﻿using Epsitec.Common.Support;

using Epsitec.Cresus.Core.Data;
using Epsitec.Cresus.Core.Metadata;

using System.IO;


namespace Epsitec.Cresus.WebCore.Server.Core.Extraction
{


	internal abstract class EntityWriter
	{


		public EntityWriter(DataSetMetadata metadata, DataSetAccessor accessor)
		{
			this.metadata = metadata;
			this.accessor = accessor;
		}


		protected DataSetMetadata Metadata
		{
			get
			{
				return this.metadata;
			}
		}


		protected DataSetAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}


		public string GetFilename()
		{
			var rootFileName = this.GetRootFilename ();
			var extension = this.GetExtension ();

			return string.Format ("{0}.{1}", rootFileName, extension);
		}


		private string GetRootFilename()
		{
			var entityName = this.Metadata.Command.Caption.DefaultLabel;

			return StringUtils.RemoveDiacritics (entityName.ToLowerInvariant ());
		}


		public Stream GetStream()
		{
			var stream = new MemoryStream ();

			this.WriteStream (stream);

			stream.Position = 0;

			return stream;
		}


		protected abstract string GetExtension();


		protected abstract void WriteStream(Stream stream);


		private readonly DataSetMetadata metadata;


		private readonly DataSetAccessor accessor;


	}


}
