using System.Windows.Input;
using membraneQualityAnalysis.application.MVVM;

namespace membraneQualityAnalysis.application.View.Controls;

public partial class VideoPlayerViewModel
{
	public sealed class VideoPlayerViewModelCommands
	{
		public ICommand StartPlayer { get; set; }
		public ICommand StopPlayer { get; set; }
		public ICommand SettingsPlayer { get; set; }

		public VideoPlayerViewModelCommands(VideoPlayerViewModel vm)
		{
			StartPlayer = new DelegateCommand(vm.StartPlayer);
			StopPlayer = new DelegateCommand(vm.StopPlayer);
			SettingsPlayer = new DelegateCommand(vm.SettingsPlayer);
		}
	}

	private VideoPlayerViewModelCommands _commands;

	public VideoPlayerViewModelCommands Commands => _commands ??= new VideoPlayerViewModelCommands(this);

}
