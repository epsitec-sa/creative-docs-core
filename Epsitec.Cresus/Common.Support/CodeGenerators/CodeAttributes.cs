//	Copyright � 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;

namespace Epsitec.Common.Support.CodeGenerators
{
	/// <summary>
	/// The <c>CodeAttributes</c> structure describes the accessibility, visibility
	/// and other method and property related attributes.
	/// </summary>
	public struct CodeAttributes
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAttributes"/> struct.
		/// </summary>
		/// <param name="attributes">The attributes.</param>
		public CodeAttributes(CodeAttributes attributes)
		{
			this.accessibility = attributes.accessibility;
			this.visibility = attributes.visibility;
			this.readOnly = attributes.readOnly;
			this.newDefinition = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAttributes"/> struct.
		/// </summary>
		/// <param name="accessibility">The code accessibility.</param>
		public CodeAttributes(CodeAccessibility accessibility)
		{
			this.accessibility = accessibility;
			this.visibility = CodeVisibility.Public;
			this.readOnly = false;
			this.newDefinition = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAttributes"/> struct.
		/// </summary>
		/// <param name="visibility">The code visibility.</param>
		public CodeAttributes(CodeVisibility visibility)
		{
			this.accessibility = CodeAccessibility.Final;
			this.visibility = visibility;
			this.readOnly = false;
			this.newDefinition = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAttributes"/> struct.
		/// </summary>
		/// <param name="accessibility">The code accessibility.</param>
		/// <param name="visibility">The code visibility.</param>
		public CodeAttributes(CodeVisibility visibility, CodeAccessibility accessibility)
		{
			this.accessibility = accessibility;
			this.visibility = visibility;
			this.readOnly = false;
			this.newDefinition = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAttributes"/> struct.
		/// </summary>
		/// <param name="accessibility">The code accessibility.</param>
		/// <param name="visibility">The code visibility.</param>
		/// <param name="attributes">One or more attributes (such as <see cref="CodeAttributes.ReadOnlyAttribute"/> or <see cref="CodeAttributes.NewAttribute"/>).</param>
		public CodeAttributes(CodeVisibility visibility, CodeAccessibility accessibility, params object[] attributes)
		{
			this.accessibility = accessibility;
			this.visibility = visibility;
			this.readOnly = false;
			this.newDefinition = false;

			foreach (object attribute in attributes)
			{
				if (attribute == CodeAttributes.ReadOnlyAttribute)
				{
					this.readOnly = true;
				}
				else if (attribute == CodeAttributes.NewAttribute)
				{
					this.newDefinition = false;
				}
			}
		}

		/// <summary>
		/// Gets the code accessibility.
		/// </summary>
		/// <value>The code accessibility.</value>
		public CodeAccessibility				Accessibility
		{
			get
			{
				return this.accessibility;
			}
		}

		/// <summary>
		/// Gets the code visibility.
		/// </summary>
		/// <value>The code visibility.</value>
		public CodeVisibility					Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the access is read only.
		/// </summary>
		/// <value><c>true</c> if the access is read only; otherwise, <c>false</c>.</value>
		public bool								ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the attribute should specify the new
		/// keyword.
		/// </summary>
		/// <value><c>true</c> if the attribute should specify the new keyword;
		/// otherwise, <c>false</c>.</value>
		public bool								New
		{
			get
			{
				return this.newDefinition;
			}
		}

		/// <summary>
		/// Gets the read only attribute constant which can be passed to the
		/// <see cref="CodeAttributes"/> constructor.
		/// </summary>
		public static readonly object			ReadOnlyAttribute = new object ();

		/// <summary>
		/// Gets the new attribute constant which can be passed to the
		/// <see cref="CodeAttributes"/> constructor.
		/// </summary>
		public static readonly object NewAttribute = new object ();

		/// <summary>
		/// Gets the default <see cref="CodeAttributes"/> instance.
		/// </summary>
		public static readonly CodeAttributes Default = new CodeAttributes ();

		/// <summary>
		/// Performs an implicit conversion from <see cref="CodeAttributes"/> to <see cref="CodeAccessibility"/>.
		/// </summary>
		/// <param name="attributes">The code attributes.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator CodeAccessibility(CodeAttributes attributes)
		{
			return attributes.Accessibility;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="CodeAttributes"/> to <see cref="CodeVisibility"/>.
		/// </summary>
		/// <param name="attributes">The code attributes.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator CodeVisibility(CodeAttributes attributes)
		{
			return attributes.Visibility;
		}

		/// <summary>
		/// Returns the C# representation of the code attributes defined by this
		/// instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string"/> containing the C# representation of the code
		/// attributes defined by this instance.
		/// </returns>
		public override string ToString()
		{
			List<string> tokens = new List<string> ();

			switch (this.visibility)
			{
				case CodeVisibility.Internal:
					tokens.Add ("internal");
					break;
				
				case CodeVisibility.Private:
					tokens.Add ("private");
					break;

				case CodeVisibility.Protected:
					tokens.Add ("protected");
					break;
				
				case CodeVisibility.Public:
					tokens.Add ("public");
					break;

				default:
					throw new System.NotSupportedException (string.Format ("CodeVisibility.{0} not supported here", this.visibility));
			}

			switch (this.accessibility)
			{
				case CodeAccessibility.Abstract:
					tokens.Add ("abstract");
					break;
				
				case CodeAccessibility.Constant:
					tokens.Add ("const");
					break;

				case CodeAccessibility.Final:
					break;
				
				case CodeAccessibility.Override:
					tokens.Add ("override");
					break;

				case CodeAccessibility.Static:
					tokens.Add ("static");
					break;
				
				case CodeAccessibility.Virtual:
					tokens.Add ("virtual");
					break;

				case CodeAccessibility.Sealed:
					tokens.Add ("sealed");
					break;

				default:
					throw new System.NotSupportedException (string.Format ("CodeAccess.{0} not supported here", this.accessibility));
			}

			if (this.readOnly)
			{
				tokens.Add ("readonly");
			}

			if (this.newDefinition)
			{
				tokens.Add ("new");
			}

			return string.Join (" ", tokens.ToArray ());
		}

		private CodeAccessibility				accessibility;
		private CodeVisibility					visibility;
		private bool							readOnly;
		private bool							newDefinition;
	}
}
