//	Copyright © 2003-2004, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Statut : en chantier

namespace Epsitec.Common.Support
{
	/// <summary>
	/// La classe CommandEvent représente une commande, telle que transmise
	/// par le CommandDispatcher.
	/// </summary>
	public class CommandEventArgs : System.EventArgs
	{
		public CommandEventArgs(object source, string command_name, string[] args)
		{
			this.source  = source;
			this.command = command_name;
			this.args    = args;
		}
		
		public object					Source
		{
			get { return this.source; }
		}
		
		public string					CommandName
		{
			get { return this.command; }
		}
		
		public string[]					CommandArgs
		{
			get { return this.args; }
		}
		
		
		protected object				source;
		protected string				command;
		protected string[]				args;
	}
	
	public delegate void CommandEventHandler(CommandDispatcher sender, CommandEventArgs e);
}
