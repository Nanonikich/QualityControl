using System.Windows.Input;
using membraneQualityAnalysis.application.MVVM;

namespace membraneQualityAnalysis.application.View.Controls;

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
