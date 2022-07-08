using qualityControl.application.MVVM;
using System.Windows.Input;

namespace qualityControl.application.View.Controls;

public partial class ClassifierViewModel
{
	public sealed class ClassifierViewModelCommands
	{
		public ICommand SaveClassifier { get; set; }

		public ClassifierViewModelCommands(ClassifierViewModel vm)
		{
			//SaveClassifier = new DelegateCommand(vm.ApplyClassifier);
		}
	}

	private ClassifierViewModelCommands _commands;

	public ClassifierViewModelCommands Commands => _commands ??= new ClassifierViewModelCommands(this);

}
