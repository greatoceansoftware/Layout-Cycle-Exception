using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared;

public abstract class ViewModelBase : INotifyPropertyChanged
{

  // properties
  public static bool SuspendNotifications { get; set; }

  // methods
  internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
  {
    if (SuspendNotifications == false) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  protected bool SetProperty<T>(ref T backingStore, T value, Action? onChanged = null, [CallerMemberName] string propertyName = "")
  {

    // no need to change if it is already set to inbound value
    if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;

    // set the backing value of the property
    backingStore = value;

    // fire the changed event
    onChanged?.Invoke();
    MainThread.BeginInvokeOnMainThread(() => NotifyPropertyChanged(propertyName));

    // return success
    return true;

  }

  // events
  public event PropertyChangedEventHandler? PropertyChanged;

}