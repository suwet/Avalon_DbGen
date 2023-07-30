using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Avalon.Models
{
	public  class ListBloodgroupViewModel : ViewModelBase
	{
		public ListBloodgroupViewModel()
		{
		}
		public ListBloodgroupViewModel(IEnumerable<BloodgroupViewModel> list )
		{
			BloodgroupViewModels = new ObservableCollection<BloodgroupViewModel>(list.ToList());
		}
		public ObservableCollection<BloodgroupViewModel> BloodgroupViewModels {get;set;}
	}
}