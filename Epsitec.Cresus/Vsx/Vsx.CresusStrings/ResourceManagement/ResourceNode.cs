﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Epsitec.Cresus.ResourceManagement
{
	public abstract class ResourceNode
	{
		public abstract ResourceNode Accept(ResourceVisitor visitor);
	}
}
