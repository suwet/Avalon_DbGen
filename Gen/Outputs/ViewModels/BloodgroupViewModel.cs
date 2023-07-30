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
	public  class BloodgroupViewModel : ViewModelBase
	{
				private Int32  _id;  
    
		private String  _bloodgroupname;  
    
		private String  _bloodgroupdescription;  
    
		private String  _active;  
    
		private DateTime  _createddate;  
    
		private DateTime  _modifielddate;  

		////////////////////////////
				public Int32  Id  
 		{
		   get=> _id;
		   set => this.RaiseAndSetIfChanged(ref _id,value);
		} 
    
		public String  BloodGroupName  
 		{
		   get=> _bloodgroupname;
		   set => this.RaiseAndSetIfChanged(ref _bloodgroupname,value);
		} 
    
		public String  BloodGroupDescription  
 		{
		   get=> _bloodgroupdescription;
		   set => this.RaiseAndSetIfChanged(ref _bloodgroupdescription,value);
		} 
    
		public String  Active  
 		{
		   get=> _active;
		   set => this.RaiseAndSetIfChanged(ref _active,value);
		} 
    
		public DateTime  CreatedDate  
 		{
		   get=> _createddate;
		   set => this.RaiseAndSetIfChanged(ref _createddate,value);
		} 
    
		public DateTime  ModifieldDate  
 		{
		   get=> _modifielddate;
		   set => this.RaiseAndSetIfChanged(ref _modifielddate,value);
		} 

	}
}