using System;
using System.Windows.Input;

namespace qualityControl.application.MVVM;

public class DelegateCommand : ICommand
{
	public event EventHandler? CanExecuteChanged;

	public readonly Action _executeMethods;
	public readonly Func<bool> _canExecuteMethod;

	public DelegateCommand(
		Action executeMethods, 
		Func<bool>? canExecuteMethods = null, 
		bool isAutomaticRequeryDisable = false)
	{
		_executeMethods = executeMethods;
		_canExecuteMethod = canExecuteMethods;
	}

	public bool CanExecute()
	{
		if(_canExecuteMethod != null)
		{
			return _canExecuteMethod!();
		}
		return true;
	}

	public void Execute()
	{
		_executeMethods();
	}

	bool ICommand.CanExecute(object? parameter)
	{
		return CanExecute();
	}

	void ICommand.Execute(object? parameter)
	{
		Execute();
	}
}
