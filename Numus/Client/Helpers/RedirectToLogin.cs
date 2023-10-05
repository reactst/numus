﻿using Microsoft.AspNetCore.Components;

namespace Numus.Shared.Helpers
{
    public class RedirectToLogin:ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get;set; }

        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo("login");
        }
    }
}
