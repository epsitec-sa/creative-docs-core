//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Text.RegularExpressions;
using System.Collections.Generic;

using Epsitec.Common.Types;

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// The <c>CommandContextChain</c> represents a chain (some kind of read-only stack)
	/// with all the <c>CommandContext</c> objects found when walking up a visual tree.
	/// The first command context will be the one which is nearest to the visual where
	/// the start was initiated.
	/// </summary>
	public sealed class CommandContextChain
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CommandContextChain"/> class.
		/// </summary>
		private CommandContextChain()
		{
		}

		/// <summary>
		/// Gets the command contexts.
		/// </summary>
		/// <value>The context enumeration.</value>
		public IEnumerable<CommandContext> Contexts
		{
			get
			{
				if ((this.chain != null) &&
					(this.chain.Count > 0))
				{
					System.WeakReference[] chain = this.chain.ToArray ();

					for (int i = 0; i < chain.Length; i++)
					{
						CommandContext context = chain[i].Target as CommandContext;

						if (context == null)
						{
							this.chain.Remove (chain[i]);
						}
						else
						{
							yield return context;
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets the first context in the context chain.
		/// </summary>
		/// <value>The first context or <c>null</c> if the chain is empty.</value>
		public CommandContext FirstContext
		{
			get
			{
				foreach (CommandContext context in this.Contexts)
				{
					return context;
				}

				return null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this chain is empty.
		/// </summary>
		/// <value><c>true</c> if this chain is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty
		{
			get
			{
				for (int i = 0; i < this.chain.Count; i++)
				{
					if (this.chain[i].IsAlive)
					{
						return false;
					}
				}

				return true;
			}
		}

		/// <summary>
		/// Gets the local enable state of the command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns><c>false</c> if the command is disabled locally, <c>true</c> otherwise.</returns>
		public bool GetLocalEnable(Command command)
		{
			foreach (CommandContext context in this.Contexts)
			{
				if (context.GetLocalEnable (command) == false)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Builds the command context chain based on a visual.
		/// </summary>
		/// <param name="visual">The visual from where to search for the command contexts.</param>
		/// <returns>The command context chain.</returns>
		public static CommandContextChain BuildChain(Visual visual)
		{
			CommandContextChain that = null;

			CommandContextChain.BuildChain (visual, ref that);
			CommandContextChain.BuildChain (visual.Window, ref that);

			return that;
		}

		/// <summary>
		/// Builds the command context chain based on a window.
		/// </summary>
		/// <param name="window">The window from where to search for the command contexts.</param>
		/// <returns>The command context chain.</returns>
		public static CommandContextChain BuildChain(Window window)
		{
			CommandContextChain that = null;

			CommandContextChain.BuildChain (window, ref that);

			return that;
		}

		/// <summary>
		/// Builds the command context chain based on a dependency object. The
		/// resulting chain will either have zero or one element, unless the
		/// dependency object maps to a <c>Visual</c>, in which case it can
		/// have more elements.
		/// </summary>
		/// <param name="obj">The dependency object to consider.</param>
		/// <returns>The command context chain.</returns>
		public static CommandContextChain BuildChain(DependencyObject obj)
		{
			CommandContextChain that = null;

			CommandContextChain.BuildChain (obj, ref that);

			return that;
		}

		public static CommandContextChain BuildChain(DependencyObject obj, ref CommandContextChain chain)
		{
			Visual visual = obj as Visual;
			Window window = obj as Window;

			if (visual != null)
			{
				CommandContextChain.BuildChain (visual, ref chain);
				return chain;
			}
			if (window != null)
			{
				CommandContextChain.BuildChain (window, ref chain);
				return chain;
			}

			CommandContextChain that = null;

			if (obj != null)
			{
				CommandContext context = CommandContext.GetContext (obj);

				if (context != null)
				{
					if (that == null)
					{
						that = new CommandContextChain ();
					}

					that.chain.Add (new System.WeakReference (context));
				}
			}
			
			return that;
		}

		/// <summary>
		/// Gets the state of the command.
		/// </summary>
		/// <param name="commandName">Name of the command.</param>
		/// <returns>The command state.</returns>
		public CommandState GetCommandState(string commandName)
		{
			Command command = Command.Get (commandName);
			CommandContext context;
			
			return this.GetCommandState (command, out context);
		}

		/// <summary>
		/// Gets the state of the command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The command state.</returns>
		public CommandState GetCommandState(Command command)
		{
			CommandContext context;
			return this.GetCommandState (command, out context);
		}
		
		/// <summary>
		/// Gets the state of the command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="context">The context where the command state was found, or <c>null</c> is none was found.</param>
		/// <returns>The command state.</returns>
		public CommandState GetCommandState(Command command, out CommandContext context)
		{
			if ((this.chain != null) &&
				(this.chain.Count > 0))
			{
				CommandContext root = null;
				
				foreach (System.WeakReference item in this.chain)
				{
					context = item.Target as CommandContext;

					if (context != null)
					{
						CommandState state = context.FindCommandState (command);
						
						if (state != null)
						{
							return state;
						}

						root = context;
					}
				}

				if (root != null)
				{
					//	Create the command state in the root-level context.

					context = root;
					CommandState state = context.GetCommandState (command);
					
					return state;
				}
			}

			context = null;
			
			return null;
		}

		private static void BuildChain(Visual visual, ref CommandContextChain that)
		{
			while (visual != null)
			{
				CommandContext context = CommandContext.GetContext (visual);

				if (context != null)
				{
					if (that == null)
					{
						that = new CommandContextChain ();
					}

					that.chain.Add (new System.WeakReference (context));
				}

				AbstractMenu menu = visual as AbstractMenu;

				if (menu != null)
				{
					CommandContextChain.BuildChain (menu.Host, ref that);
				}

				visual = visual.Parent;
			}
		}

		private static void BuildChain(Window window, ref CommandContextChain that)
		{
			while (window != null)
			{
				CommandContext context = CommandContext.GetContext (window);

				if (context != null)
				{
					if (that == null)
					{
						that = new CommandContextChain ();
					}

					that.chain.Add (new System.WeakReference (context));
				}

				window = window.Owner;
			}

			//	TODO: ajouter ici la notion d'application/module/document
		}

		List<System.WeakReference> chain = new List<System.WeakReference> ();
	}
}
