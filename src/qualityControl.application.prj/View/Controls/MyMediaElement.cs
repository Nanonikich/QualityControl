using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace qualityControl.application.View.Controls;

public class MyMediaElement : MediaElement
{
	public bool IsPause { get; set; }

	public MyMediaElement()
	{
		//LoadedBehavior = MediaState.Manual;
		UnloadedBehavior = MediaState.Manual;
		Pause();
		IsPause = true;

		MediaEnded += (s, e) =>
		{
			Position = TimeSpan.FromSeconds(0);
			Play();
		};

		MouseDown += (s, e) =>
		{
			if(IsPause)
			{
				Play();
			}
			else
			{
				Pause();
			}
			IsPause = !IsPause;
		};
	}
}
