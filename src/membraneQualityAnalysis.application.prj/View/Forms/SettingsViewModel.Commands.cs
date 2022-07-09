using System.Windows.Input;
using membraneQualityAnalysis.application.MVVM;

namespace membraneQualityAnalysis.application.View.Forms;

public partial class SettingsViewModel
{
	public sealed class SettingsViewModelCommands
	{
		public ICommand FileSource { get; set; }
		public ICommand Apply { get; set; }
		public ICommand Cancel { get; set; }


		public SettingsViewModelCommands(SettingsViewModel vm)
		{
			FileSource = new DelegateCommand(vm.FileSource);
			Apply = new DelegateCommand(vm.Apply);
			Cancel = new DelegateCommand(vm.Cancel);
		}
	}

	private SettingsViewModelCommands _commands;

	public SettingsViewModelCommands Commands => _commands ??= new SettingsViewModelCommands(this);

}

