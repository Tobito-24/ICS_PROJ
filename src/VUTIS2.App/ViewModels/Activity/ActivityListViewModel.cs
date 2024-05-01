using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;
[QueryProperty(nameof(subjectId), nameof(subjectId))]
public partial class ActivityListViewModel(IActivityFacade activityFacade, INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{

    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty with { StartTime = DateTime.Now, EndTime = DateTime.Now };

    public DateTime StartTimeDate
    {
        get => Activity.StartTime;
        set
        {
            if (Activity.StartTime.Date != value)
            {
                Activity.StartTime = new DateTime(value.Year, value.Month, value.Day,
                    StartTimeDate.Hour, StartTimeDate.Minute, StartTimeDate.Second);
                OnPropertyChanged(nameof(Activity.StartTime));
            }
        }
    }
    public TimeSpan StartTimeTimeSpan
    {
        get => Activity.StartTime.TimeOfDay;
        set
        {
            if (Activity.StartTime.TimeOfDay != value)
            {
                Activity.StartTime = new DateTime(StartTimeDate.Year, StartTimeDate.Month, StartTimeDate.Day,
                    value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged(nameof(Activity.StartTime));
            }
        }
    }

    public DateTime EndTimeDate
    {
        get => Activity.EndTime.Date;
        set
        {
            if (Activity.EndTime.Date != value.Date)
            {
                Activity.EndTime = new DateTime(value.Year, value.Month, value.Day,
                    EndTimeTimeSpan.Hours, EndTimeTimeSpan.Minutes, EndTimeTimeSpan.Seconds);
                OnPropertyChanged(nameof(Activity.EndTime));
            }
        }
    }
    public TimeSpan EndTimeTimeSpan
    {
        get => Activity.EndTime.TimeOfDay;
        set
        {
            if (Activity.EndTime.TimeOfDay != value)
            {
                Activity.EndTime = new DateTime(EndTimeDate.Year, EndTimeDate.Month, EndTimeDate.Day,
                    value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged(nameof(Activity.EndTime));
            }
        }
    }

    public Guid subjectId { get; set; }
    private bool sortedStartTimeAscending = true;
    private bool sortedEndTimeAscending = true;
    DateTime StartTime = DateTime.Now;
    DateTime EndTime = DateTime.Now;
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public DateTime StartTimeDate
    {
        get => StartTime;
        set
        {
            if (StartTime.Date != value)
            {
                StartTime = new DateTime(value.Year, value.Month, value.Day,
                    StartTimeDate.Hour, StartTimeDate.Minute, StartTimeDate.Second);
                OnPropertyChanged(nameof(StartTime));
            }
        }
    }
    public TimeSpan StartTimeTimeSpan
    {
        get => StartTime.TimeOfDay;
        set
        {
            if (StartTime.TimeOfDay != value)
            {
                StartTime = new DateTime(StartTimeDate.Year, StartTimeDate.Month, StartTimeDate.Day,
                    value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged(nameof(StartTime));
            }
        }
    }

    public DateTime EndTimeDate
    {
        get => EndTime.Date;
        set
        {
            if (EndTime.Date != value.Date)
            {
                EndTime = new DateTime(value.Year, value.Month, value.Day,
                    EndTimeTimeSpan.Hours, EndTimeTimeSpan.Minutes, EndTimeTimeSpan.Seconds);
                OnPropertyChanged(nameof(EndTime));
            }
        }
    }
    public TimeSpan EndTimeTimeSpan
    {
        get => EndTime.TimeOfDay;
        set
        {
            if (EndTime.TimeOfDay != value)
            {
                EndTime = new DateTime(EndTimeDate.Year, EndTimeDate.Month, EndTimeDate.Day,
                    value.Hours, value.Minutes, value.Seconds);
                OnPropertyChanged(nameof(EndTime));
            }
        }
    }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activities = await activityFacade.GetAsyncBySubject(subjectId);
    }

    [RelayCommand]
    public async Task GoToActivityDetailAsync(Guid id)
    {
        await navigationService.GoToAsync("/activitydetail",
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
    }

    [RelayCommand]
    public async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    public async Task DeleteAsync(Guid id)
    {
        await activityFacade.DeleteAsync(id);
        MessengerService.Send(new ActivityDeleteMessage());
    }
    [RelayCommand]
    public async Task SortByStartTimeAsync()
    {
        if (sortedStartTimeAscending)
        {
            Activities = activityFacade.GetOrderedByStartTimeAsc(Activities.ToList());
            sortedStartTimeAscending = false;
        }
        else
        {
            Activities = activityFacade.GetOrderedByStartTimeDesc(Activities.ToList());
            sortedStartTimeAscending = true;
        }

        await base.LoadDataAsync();
    }

    /*[RelayCommand]
    public async Task FilterByStartTimeAsync(DateTime startTime, bool from)
    {
        Activities = await activityFacade.GetActivitiesStartTime(startTime, from);
        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task FilterByEndTimeAsync(DateTime endTime, bool from)
    {
        Activities = await activityFacade.GetActivitiesEndTime(endTime, from);
        await base.LoadDataAsync();
    }*/

    [RelayCommand]
    public async Task SortByEndTimeAsync()
    {
        if (sortedEndTimeAscending)
        {
            Activities = activityFacade.GetOrderedByEndTimeAsc(Activities.ToList());
            sortedEndTimeAscending = false;
        }
        else
        {
            Activities = activityFacade.GetOrderedByEndTimeDesc(Activities.ToList());
            sortedEndTimeAscending = true;
        }

        await base.LoadDataAsync();
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
