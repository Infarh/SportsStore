using System.Globalization;
using Microsoft.AspNetCore.Razor.TagHelpers;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace SportsStore.TagHelpers
{
    [HtmlTargetElement(Attributes = __TagHelperAttributeName)]
    public class DataAjax : TagHelper
    {
        //https://github.com/dhlavaty/winf.unobtrusiveAjax.js/blob/master/README.md
        private const string __TagHelperAttributeName = "asp-ajax";
        private const string __DataAjaxAttributeName = "data-ajax";
        private const string __DataAjaxUrlAttribute = "data-ajax-url";
        private const string __DataAjaxUpdateAttribute = "data-ajax-update";
        private const string __DataAjaxLoadingAttribute = "data-ajax-loading";
        private const string __DataAjaxConfirmAttribute = "data-ajax-confirm";
        private const string __DataAjaxMethodAttribute = "data-ajax-method";
        private const string __DataAjaxModeAttribute = "data-ajax-mode";
        private const string __DataAjaxDisableOnclickAttribute = "data-ajax-disable-onclick";
        private const string __DataAjaxUpdateClosestAttribute = "data-ajax-update-closest";
        private const string __DataAjaxLoadingDurationAttribute = "data-ajax-loading-duration";
        private const string __DataAjaxBeginAttribute = "data-ajax-begin";
        private const string __DataAjaxSuccessAttribute = "data-ajax-success";
        private const string __DataAjaxCompleteAttribute = "data-ajax-complete";
        private const string __DataAjaxErrorAttribute = "data-ajax-error";
        private const string __DataAjaxFailureAttribute = "data-ajax-failure";

        private const string __HrefAttributeEmptyValue = "#";
        private const string __HrefAttribute = "href";
        private const string __ActionAttribute = "action";

        private const string __AjaxUpdate = "ajax-update";
        private const string __AjaxLoading = "ajax-loading";
        private const string __AjaxConfirm = "ajax-confirm";
        private const string __AjaxDisableOnclick = "ajax-disable-onclick";
        private const string __AjaxMethod = "ajax-method";
        private const string __AjaxMode = "ajax-mode";
        private const string __AjaxUpdateClosest = "ajax-update-closest";
        private const string __AjaxLoadingDuration = "ajax-loading-duration";
        private const string __AjaxBegin = "ajax-begin";
        private const string __AjaxSuccess = "ajax-success";
        private const string __AjaxComplete = "ajax-complete";
        private const string __AjaxError = "ajax-error";
        private const string __AjaxFailure = "ajax-failure";

        [HtmlAttributeName(__TagHelperAttributeName)]
        public bool Enabled { get; set; }

        [HtmlAttributeName(__AjaxUpdate)]
        public string OutputDataElement { get; set; }

        [HtmlAttributeName(__AjaxLoading)]
        public string ProgressElement { get; set; }

        [HtmlAttributeName(__AjaxBegin)]
        public string OnBeginScriptName { get; set; }

        [HtmlAttributeName(__AjaxComplete)]
        public string OnCompleteScriptName { get; set; }

        [HtmlAttributeName(__AjaxSuccess)]
        public string OnSuccessScriptName { get; set; }

        [HtmlAttributeName(__AjaxError)]
        public string OnErrorScriptName { get; set; }

        [HtmlAttributeName(__AjaxFailure)]
        public string OnFailureScriptName { get; set; }

        [HtmlAttributeName(__AjaxConfirm)]
        public string ConfirmMessage { get; set; }

        [HtmlAttributeName(__AjaxDisableOnclick)]
        public bool DisableOnClick { get; set; }

        [HtmlAttributeName(__AjaxMethod)]
        public string Method { get; set; }

        [HtmlAttributeName(__AjaxMode)]
        public string Mode { get; set; }

        [HtmlAttributeName(__AjaxLoadingDuration)]
        public int LoadngDuration { get; set; }

        [HtmlAttributeName(__AjaxUpdateClosest)]
        public string ClosestTargetElementType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var attributes = output.Attributes;
            attributes.RemoveAll(__TagHelperAttributeName);
            attributes.SetAttribute(__DataAjaxAttributeName, Enabled.ToString().ToLower());

            switch (context.TagName.ToLower(CultureInfo.InvariantCulture))
            {
                case "a":
                    var href = attributes[__HrefAttribute].Value;
                    if (Enabled)
                        attributes.SetAttribute(__HrefAttribute, __HrefAttributeEmptyValue);
                    attributes.SetAttribute(__DataAjaxUrlAttribute, href);
                    break;
                case "form":
                    var action = attributes[__ActionAttribute].Value;
                    if (Enabled)
                        attributes.RemoveAll(__ActionAttribute);
                    attributes.SetAttribute(__DataAjaxUrlAttribute, action);

                    var method = attributes["method"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(method) && string.IsNullOrWhiteSpace(Method))
                        Method = method;
                    attributes.RemoveAll("method");
                    break;
            }
        

            if (!string.IsNullOrWhiteSpace(OutputDataElement))
                attributes.SetAttribute(__DataAjaxUpdateAttribute, OutputDataElement);

            if (!string.IsNullOrWhiteSpace(ProgressElement))
                attributes.SetAttribute(__DataAjaxLoadingAttribute, ProgressElement);

            if (!string.IsNullOrWhiteSpace(OnBeginScriptName))
                attributes.SetAttribute(__DataAjaxBeginAttribute, OnBeginScriptName);

            if (!string.IsNullOrWhiteSpace(OnCompleteScriptName))
                attributes.SetAttribute(__DataAjaxCompleteAttribute, OnCompleteScriptName);

            if (!string.IsNullOrWhiteSpace(OnSuccessScriptName))
                attributes.SetAttribute(__DataAjaxSuccessAttribute, OnSuccessScriptName);

            if (!string.IsNullOrWhiteSpace(OnErrorScriptName))
                attributes.SetAttribute(__DataAjaxErrorAttribute, OnErrorScriptName);   

            if (!string.IsNullOrWhiteSpace(OnFailureScriptName))
                attributes.SetAttribute(__DataAjaxFailureAttribute, OnFailureScriptName);

            if (!string.IsNullOrWhiteSpace(ConfirmMessage))
                attributes.SetAttribute(__DataAjaxConfirmAttribute, ConfirmMessage);

            if (DisableOnClick)
                attributes.SetAttribute(__DataAjaxDisableOnclickAttribute, "true");

            if (!string.IsNullOrWhiteSpace(Method))
                attributes.SetAttribute(__DataAjaxMethodAttribute, Method);

            if (!string.IsNullOrWhiteSpace(Mode))
                attributes.SetAttribute(__DataAjaxModeAttribute, Mode.ToUpper());

            if (!string.IsNullOrWhiteSpace(ClosestTargetElementType))
                attributes.SetAttribute(__DataAjaxUpdateClosestAttribute, ClosestTargetElementType);

            if (LoadngDuration > 0)
                attributes.SetAttribute(__DataAjaxLoadingDurationAttribute, LoadngDuration);
        }
    }

    public enum DataAjaxMode
    {
        /// <summary>Результат добавляется первым в контейнер</summary>
        BEFORE,
        /// <summary>Результат добавляется последним в контейнер</summary>
        AFTER,
        /// <summary>Результат заменяет содержимое контейнера</summary>
        REPLACE,
        /// <summary>Результат замещает контейнер</summary>
        REALPLACE,
        /// <summary>Результат добавляется до указанного элемента</summary>
        BEFOREELEMENT,
        /// <summary>Результат добавляется после указанного элемента</summary>
        AFTERELEMENT
    }
}
