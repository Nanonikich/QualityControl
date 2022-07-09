using qualityControl.application.MVVM;
using System.Windows.Input;

namespace qualityControl.application.View.Controls;

public partial class DetectionViewModel
{
	public sealed class DetectionViewModelCommands
	{
		public ICommand SaveDetection { get; set; }

		public DetectionViewModelCommands(DetectionViewModel vm)
		{
			//SaveDetection = new DelegateCommand(vm.ApplyDetection);
		}
	}

	private DetectionViewModelCommands _commands;

	public DetectionViewModelCommands Commands => _commands ??= new DetectionViewModelCommands(this);

}
