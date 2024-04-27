// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using VUTIS2.App.ViewModels;

namespace VUTIS2.App.Views;

public partial class ContentPageBase
{
    protected IViewModel ViewModel { get; }

    public ContentPageBase(IViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ViewModel.OnAppearingAsync();
    }
}
