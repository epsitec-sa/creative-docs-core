//	Copyright � 2003, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Statut : en chantier

namespace Epsitec.Common.Support
{
	/// <summary>
	/// La classe CommandDispatcher permet de g�rer la distribution des
	/// commandes de l'interface graphique vers les routines de traitement.
	/// </summary>
	public class CommandDispatcher
	{
		public CommandDispatcher()
		{
		}
		
		
		public void Dispatch(string command, object source)
		{
			//	Transmet la commande � ceux qui sont int�ress�s
			
			System.Diagnostics.Debug.WriteLine ("Command: " + command);
			System.Diagnostics.Debug.WriteLine ("Source:  " + source);
			
			if (this.event_handlers.Contains (command))
			{
				EventSlot slot = this.event_handlers[command] as EventSlot;
				slot.Fire (this, new CommandEventArgs (source, command));
			}
		}
		
		
		public void RegisterController(object controller)
		{
			if (controller != null)
			{
				System.Type type = controller.GetType ();
				System.Reflection.MemberInfo[] members = type.GetMembers (System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
				
				for (int i = 0; i < members.Length; i++)
				{
					if ((members[i].IsDefined (CommandDispatcher.command_attr_type, true)) &&
						(members[i].MemberType == System.Reflection.MemberTypes.Method))
					{
						System.Reflection.MethodInfo info = members[i] as System.Reflection.MethodInfo;
						this.RegisterMethod (controller, info);
					}
				}
			}
		}
		
		public void Register(string command, CommandEventHandler handler)
		{
			EventSlot slot;
			
			if (this.event_handlers.Contains (command))
			{
				slot = this.event_handlers[command] as EventSlot;
			}
			else
			{
				slot = new EventSlot (command);
				this.event_handlers[command] = slot;
			}
			
			slot.Register (handler);
		}
		
		public void Unregister(string command, CommandEventHandler handler)
		{
			if (this.event_handlers.Contains (command))
			{
				EventSlot slot = this.event_handlers[command] as EventSlot;
				
				slot.Unregister (handler);
				
				if (slot.Count == 0)
				{
					this.event_handlers.Remove (command);
				}
			}
		}
		
		
		protected void RegisterMethod(object controller, System.Reflection.MethodInfo info)
		{
			//	Ne parcourt que les attributs au niveau d'impl�mentation actuel (pas les classes d�riv�es,
			//	ni les classes parent). Le parcours des parent est assur� par l'appelant.
			
			object[] attributes = info.GetCustomAttributes (CommandDispatcher.command_attr_type, false);
			
			foreach (CommandAttribute attribute in attributes)
			{
				this.RegisterMethod (controller, info, attribute);
			}
		}
		
		protected void RegisterMethod(object controller, System.Reflection.MethodInfo method_info, CommandAttribute attribute)
		{
			System.Diagnostics.Debug.WriteLine ("Method: " + method_info.Name + " in " + method_info.DeclaringType.Name + " is a command called " + attribute.CommandName + ", " + method_info.ToString ());
			
			System.Reflection.ParameterInfo[] param_info = method_info.GetParameters ();
			
			CommandEventHandler handler = null;
			EventRelay          relay   = new EventRelay (controller, method_info);
			
			switch (param_info.Length)
			{
				case 0:
					//	La m�thode n'a aucun argument :
					
					handler = new CommandEventHandler (relay.InvokeWithoutArgument);
					break;
				
				case 1:
					//	La m�thode a un unique argument. Ce n'est acceptable que si cet argument est
					//	de type CommandDispatcher, soit :
					//
					//		void Method(CommandDispatcher)
					
					if (param_info[0].ParameterType == typeof (CommandDispatcher))
					{
						handler = new CommandEventHandler (relay.InvokeWithCommandDispatcher);
					}
					break;
				
				case 2:
					//	La m�thode a deux arguments. Ce n'est acceptable que si le premier est de type
					//	CommandDispatcher et le second de type CommandEventArgs, soit :
					//
					//		void Method(CommandDispatcher, CommandEventArgs)
					
					if ((param_info[0].ParameterType == typeof (CommandDispatcher)) &&
						(param_info[1].ParameterType == typeof (CommandEventArgs)))
					{
						handler = new CommandEventHandler (relay.InvokeWithCommandDispatcherAndEventArgs);
					}
					break;
			}
			
			if (handler == null)
			{
				throw new System.FormatException (string.Format ("{0}.{1} uses invalid signature: {2}.", controller.GetType ().Name, method_info.Name, method_info.ToString ()));
			}
			
			this.Register (attribute.CommandName, handler);
			
		}
		
		
		protected class EventRelay
		{
			public EventRelay(object controller, System.Reflection.MethodInfo method_info)
			{
				//	Cette classe r�alise un relais entre le delegate CommandEventHandler et les
				//	diverses impl�mentations possibles au niveau des gestionnaires de commandes.
				//	Ainsi, les m�thodes :
				//
				//		void Method()
				//		void Method(CommandDispatcher)
				//		void Method(CommandDispatcher, CommandEventArgs)
				//
				//	sont toutes appelables via CommandEventHandler.
				
				this.controller  = controller;
				this.method_info = method_info;
			}
			
			
			public void InvokeWithoutArgument(CommandDispatcher sender, CommandEventArgs e)
			{
				this.method_info.Invoke (this.controller, null);
			}
			
			public void InvokeWithCommandDispatcher(CommandDispatcher sender, CommandEventArgs e)
			{
				object[] p = new object[1];
				p[0] = sender;
				this.method_info.Invoke (this.controller, p);
			}
			
			public void InvokeWithCommandDispatcherAndEventArgs(CommandDispatcher sender, CommandEventArgs e)
			{
				object[] p = new object[2];
				p[0] = sender;
				p[1] = e;
				this.method_info.Invoke (this.controller, p);
			}
			
			
			protected object						controller;
			protected System.Reflection.MethodInfo	method_info;
		}
		
		protected class EventSlot
		{
			public EventSlot(string name)
			{
				this.name = name;
			}
			
			
			public void Register(CommandEventHandler handler)
			{
				this.command += handler;
				this.count++;
			}
			
			public void Unregister(CommandEventHandler handler)
			{
				this.command -= handler;
				this.count--;
			}
			
			
			public void Fire(CommandDispatcher sender, CommandEventArgs e)
			{
				if (this.command != null)
				{
					this.command (sender, e);
				}
			}
			
			
			public int							Count
			{
				get { return this.count; }
			}
			
			public string						Name
			{
				get { return this.name; }
			}
			
			
			protected string					name;
			protected event CommandEventHandler	command;
			protected int						count;
		}
		
		
		public static CommandDispatcher			Default
		{
			get { return CommandDispatcher.default_dispatcher; }
		}
		
		
		protected System.Collections.Hashtable	event_handlers = new System.Collections.Hashtable ();
		
		private static System.Type				command_attr_type  = typeof (CommandAttribute);
		private static CommandDispatcher		default_dispatcher = new CommandDispatcher ();
	}
}
