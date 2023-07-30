using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace Avalon.Models
{
	public  class Bloodgroup : ModelBase
	{
				public Int32  Id  {get;set;}
    
		public String  BloodGroupName  {get;set;}
    
		public String?  BloodGroupDescription  {get;set;}
    
		public String?  Active  {get;set;}
    
		public DateTime?  CreatedDate  {get;set;}
    
		public DateTime?  ModifieldDate  {get;set;}

	}
}