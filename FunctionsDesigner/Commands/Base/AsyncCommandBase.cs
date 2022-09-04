using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FunctionsDesigner.Commands.Base
{
	public abstract class AsyncCommandBase : ICommand
	{
		/// <summary>
		/// Occurs when changes occur that affect whether or not the command should execute.
		/// </summary>
		public event EventHandler? CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public abstract Task ExecuteAsync();

		public abstract bool CanExecuteTask();

		[DebuggerStepThrough]
		bool ICommand.CanExecute(object? parameter)
		{
			return CanExecute();
		}

		void ICommand.Execute(object? parameter)
		{
			Execute();
		}

		/// <summary>
		/// Triggers the CanExecuteChanged event.
		/// </summary>
		protected void RaiseCanExecuteChanged()
		{
			CommandManager.InvalidateRequerySuggested();
		}

		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <returns>True if this command can be executed; False otherwise.</returns>
		protected bool CanExecute()
		{
			return CanExecuteTask();
		}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		protected async void Execute()
		{
			if (CanExecuteTask() == false)
				return;

			RaiseCanExecuteChanged();

			try
			{
				await ExecuteAsync();
			}
			finally
			{
				RaiseCanExecuteChanged();
			}
		}
	}

	public abstract class AsyncCommandBase<T> : ICommand
	{
		/// <summary>
		/// Occurs when changes occur that affect whether or not the command should execute.
		/// </summary>
		public event EventHandler? CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public abstract Task ExecuteAsync(T parameter);

		public abstract bool CanExecuteTask(T parameter);

		bool ICommand.CanExecute(object? parameter)
		{
			var cannotExecute = typeof(T).IsValueType && parameter == null;
			if (cannotExecute)
				return false;

			return CanExecute((T)parameter!);
		}

		void ICommand.Execute(object? parameter)
		{
			var cannotExecute = typeof(T).IsValueType && parameter == null;
			if (cannotExecute)
				return;

			Execute((T)parameter!);
		}

		/// <summary>
		/// Gets a value indicating whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter">The parameter for the command.</param>
		/// <returns>A value indicating whether the command can execute in its current state.</returns>
		protected bool CanExecute(T parameter)
		{
			return CanExecuteTask(parameter);
		}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">The parameter for the command.</param>
		protected async void Execute(T parameter)
		{
			if (CanExecuteTask(parameter) == false)
				return;

			RaiseCanExecuteChanged();

			try
			{
				await ExecuteAsync(parameter);
			}
			finally
			{
				RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Triggers the CanExecuteChanged event.
		/// </summary>
		protected void RaiseCanExecuteChanged()
		{
			CommandManager.InvalidateRequerySuggested();
		}
	}
}
