using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class MainPage : ContentPage
{

  // properties
  public Project Project { get; set; } = new();

  // constructors
  public MainPage()
  {

    // initialize
    InitializeComponent();

    // build new project
    Project = new Project("Test Project");
    for (int p = 1; p <= 3; p++)
    {
      Phase phase = new Phase($"Phase {p}");
      Project.Phases.Add(phase);
      for (int g = 1; g <= 5; g++)
      {
        Goal goal = new Goal($"Goal {g}");
        phase.Add(goal);
        for (int a = 1; a <= 5; a++)
        {
          Activity activity = new Activity($"Activity {a}");
          goal.Activities.Add(activity);
        }
      }
    }

    // set context
    BindingContext = this;

  }

  // methods
  private void ButtonDoSomething_Clicked(object sender, EventArgs e)
  {
    Project.ActivitiesFilter = "1";
    //DisplayAlert("Do Something", "Done!", "OK");
  }

  private void ButtonDoSomethingElse_Clicked(object sender, EventArgs e)
  {
    Project.ActivitiesFilter = "";
    //DisplayAlert("Do Something Else", "Done!", "OK");
  }

  private void ButtonActivity_Clicked(object sender, EventArgs e)
  {
    Button button = sender as Button;
    Activity activity = button.BindingContext as Activity;
    DisplayAlert("Activity", $"Clicked {activity.Name}", "OK");
  }

  private void ButtonPhase_Clicked(object sender, EventArgs e)
  {
    Button button = sender as Button;
    Phase phase = button.BindingContext as Phase;
    DisplayAlert("Phase", $"Clicked {phase.Name}", "OK");
  }

  private void ButtonGoal_Clicked(object sender, EventArgs e)
  {
    Button button = sender as Button;
    Goal goal = button.BindingContext as Goal;
    DisplayAlert("Goal", $"Clicked {goal.Name}", "OK");
  }

}

public class Activity : ViewModelBase
{

  private bool isMatch = true;

  public string Name { get; set; } = "";
  public string Details { get; set; } = "";
  public bool IsMatch { get { return isMatch; } set { isMatch = value; NotifyPropertyChanged(); } }

  public Activity() { }
  public Activity(string name)
  {
    Name = name;
  }

}

public class Goal : ViewModelBase
{

  private bool isMatch = true;

  // properties
  public string Name { get; set; } = "";
  public ObservableCollection<Activity> Activities { get; set; } = new();
  public bool IsMatch { get { return isMatch; } set { isMatch = value; NotifyPropertyChanged(); } }

  // constructors
  public Goal() { }
  public Goal(string name)
  {
    Name = name;
  }

}

public class Phase : ObservableCollection<Goal>
{

  // properties
  public string Name { get; set; } = "";

  // constructors
  public Phase() { }
  public Phase(string name)
  {
    Name = name;
  }

}

public class Project : ViewModelBase
{

  // fields
  private string activitiesFilter = "";

  // properties
  public string Name { get; set; } = "";
  public ObservableCollection<Phase> Phases { get; set; } = new();
  public string ActivitiesFilter
  {
    get => activitiesFilter;
    set
    {

      activitiesFilter = value;
      if (string.IsNullOrWhiteSpace(activitiesFilter))
      {
        foreach (Phase phase in Phases)
        {
          foreach (Goal goal in phase)
          {
            //goal.IsMatch = goal.Name.Contains(activitiesFilter);
            foreach (Activity activity in goal.Activities)
              activity.IsMatch = true;
          }
        }
      }
      else
      {
        foreach (Phase phase in Phases)
        {
          foreach (Goal goal in phase)
          {
            //goal.IsMatch = goal.Name.Contains(activitiesFilter);
            foreach (Activity activity in goal.Activities)
              activity.IsMatch = activity.Name.Contains(activitiesFilter);
          }
        }
      }
    }
  }

  // constructors
  public Project() { }
  public Project(string name)
  {
    Name = name;
  }

}

