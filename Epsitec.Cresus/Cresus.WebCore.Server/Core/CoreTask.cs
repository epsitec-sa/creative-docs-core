﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epsitec.Cresus.WebCore.Server.Core
{
	public sealed class CoreTask
	{
		public string Id
		{
			get 
			{ 
				return this.taskId;
			}
		}

		public string HtmlView
		{
			get
			{
				var desc = this.Id;

				if((this.Status == CoreTaskStatus.Waiting) || (this.Status == CoreTaskStatus.Running))
				{
					desc = desc + "<br><a href=''>Annuler</a>";
				}
				if (this.Status == CoreTaskStatus.Ended)
				{
					desc = desc + string.Format("<br>Durée: {0}",this.GetRunningTime());
				}

				desc = desc + "<br>" + this.Metadata;
				return desc;
			}
		}

		public string Title
		{
			get
			{
				return this.title + " (" + this.Status.Value + ")";
			}
		}

		public CoreTaskStatus Status
		{
			get;
			set;
		}

		public string Metadata
		{
			get;
			set;
		}

		public string Username
		{
			get;
			set;
		}

		public string SessionId
		{
			get;
			set;
		}

		public DateTime QueuedAt
		{
			get
			{
				return this.queuedAt;
			}
		}

		public CoreTask(string taskId, string title)
		{
			var now = DateTime.Now;
			this.title = title;
			this.Status = CoreTaskStatus.Started;
			this.taskId = taskId;
			this.createdAt = now;
		}

		public void Enqueue()
		{
			this.queuedAt = DateTime.Now;
			this.Status = CoreTaskStatus.Waiting;
		}

		public void Start()
		{
			this.startedAt = DateTime.Now;
			this.Status = CoreTaskStatus.Running;
		}

		public void Finish()
		{
			this.finisedAt = DateTime.Now;
			this.Status = CoreTaskStatus.Ended;
		}

		public TimeSpan GetRunningTime ()
		{
			return this.finisedAt.Subtract (this.startedAt);
		}

		protected readonly string taskId;
		
		protected DateTime createdAt;
		protected DateTime queuedAt;
		protected DateTime startedAt;
		protected DateTime finisedAt;

		private string title;
		
	}

	public sealed class CoreTaskStatus
	{
		public static readonly CoreTaskStatus Started = new CoreTaskStatus ("En attente");
		public static readonly CoreTaskStatus Waiting = new CoreTaskStatus ("En attente");
		public static readonly CoreTaskStatus Running = new CoreTaskStatus ("En cours");
		public static readonly CoreTaskStatus Ended = new CoreTaskStatus ("Terminée");

		private CoreTaskStatus(string value)
		{
			Value = value;
		}

		public string Value
		{
			get;
			private set;
		}
	}
}
