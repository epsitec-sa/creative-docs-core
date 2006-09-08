//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Support
{
	/// <summary>
	/// The <c>FileOperationMode</c> class is used to customize the file operations
	/// supported by the <see cref="FileManager"/> class.
	/// </summary>
	public sealed class FileOperationMode
	{
		public FileOperationMode()
		{
		}

		public bool								Silent
		{
			get
			{
				return this.silent;
			}
			set
			{
				this.silent = value;
			}
		}

		public bool								AutoRenameOnCollision
		{
			get
			{
				return this.autoRenameOnCollision;
			}
			set
			{
				this.autoRenameOnCollision = value;
			}
		}

		public bool								AutoConfirmation
		{
			get
			{
				return this.autoConfirmation;
			}
			set
			{
				this.autoConfirmation = value;
			}
		}

		public bool								AutoCreateDirectory
		{
			get
			{
				return this.autoCreateDirectory;
			}
			set
			{
				this.autoCreateDirectory = value;
			}
		}

		private bool							silent;
		private bool							autoRenameOnCollision;
		private bool							autoConfirmation;
		private bool							autoCreateDirectory;
	}
}
