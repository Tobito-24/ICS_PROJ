using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VUTIS2.App.Messages;
using VUTIS2.App.Services;
using VUTIS2.BL.Facades;
using VUTIS2.BL.Models;

namespace VUTIS2.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class StudentDetailViewModel( IStudentFacade studentFacade, INavigationService navigationService, IMessengerService messengerService, IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>
{
    public Guid Id { get; set; }
    public StudentDetailModel? Student { get; private set; }
    protected override async Task LoadDataAsync()
    {
        Student = await studentFacade.GetAsync(Id);
    }
    [RelayCommand]
    public async Task DeleteAsync()
    {
        if (Student is not null)
        {
            try
            {
                await studentFacade.DeleteAsync(Student.Id);
                messengerService.Send(new StudentDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("filler", "filler");
            }
        }
    }

    public async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(StudentEditViewModel.Student)] = Student });
    }
    public async void Receive(StudentEditMessage message)
    {
        if (message.StudentId == Id)
        {
            await LoadDataAsync();
        }
    }
}
