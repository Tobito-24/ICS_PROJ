// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.ComponentModel;
using VUTIS2.App.Services;

namespace VUTIS2.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel
{
    private bool _isRefreshRequired = true;

    protected readonly IMessengerService MessengerService;

    protected ViewModelBase(IMessengerService messengerService)
        : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (_isRefreshRequired)
        {
            await LoadDataAsync();

            _isRefreshRequired = false;
        }
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;
}
