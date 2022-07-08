using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace qualityControl.application.MVVM;

public class NotificationObjects : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
	public event EventHandler ClosingRequest;

	/// <summary>Обновляет значение свойства.</summary>
	/// <param name="propertyName">Имя свойства.</param>
	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	protected bool UpdatePropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
	{
		if(EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
}
