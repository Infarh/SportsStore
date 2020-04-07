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

        public static T With<T, TParameter>(this T controller, TParameter Parameter, Action<T, TParameter> ControllerAction) where T : ControllerBase
        {
            ControllerAction(controller, Parameter);
            return controller;
        }

        public static T With<T, TParameter>(this T controller, TParameter Parameter, Action<TParameter> ParameterAction) where T : ControllerBase
        {
            ParameterAction(Parameter);
            return controller;
        }

        public static T Execute<T>(this T controller, Action action) where T : ControllerBase
        {
            action();
            return controller;
        }

        // ReSharper disable once Mvc.ActionNotResolved
        public static RedirectToActionResult RedirectToIndex(this Controller controller) => controller.RedirectToAction("Index");

        public static ViewResult ViewIndex(this Controller controller, object Model = null) => Model is null 
            ? controller.View("Index") 
            : controller.View("Index", Model);
    }
}
