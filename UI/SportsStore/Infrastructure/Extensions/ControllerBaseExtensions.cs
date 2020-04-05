using System;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Infrastructure.Extensions
{
    internal static class ControllerBaseExtensions
    {
        public static T With<T>(this T controller, Action<T> ControllerAction) where T : ControllerBase
        {
            ControllerAction(controller);
            return controller;
        }
    }
}
