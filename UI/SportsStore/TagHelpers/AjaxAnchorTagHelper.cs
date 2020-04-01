using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace SportsStore.TagHelpers
{
    //[HtmlTargetElement("a", Attributes = __ActionAttributeName)]
    //[HtmlTargetElement("a", Attributes = __ControllerAttributeName)]
    //[HtmlTargetElement("a", Attributes = __AreaAttributeName)]
    //[HtmlTargetElement("a", Attributes = __PageAttributeName)]
    //[HtmlTargetElement("a", Attributes = __PageHandlerAttributeName)]
    //[HtmlTargetElement("a", Attributes = __FragmentAttributeName)]
    //[HtmlTargetElement("a", Attributes = __HostAttributeName)]
    //[HtmlTargetElement("a", Attributes = __ProtocolAttributeName)]
    //[HtmlTargetElement("a", Attributes = __RouteAttributeName)]
    //[HtmlTargetElement("a", Attributes = __RouteValuesDictionaryName)]
    //[HtmlTargetElement("a", Attributes = __RouteValuesPrefix + "*")]
    //public class AjaxAnchorTagHelper : TagHelper
    //{
    //    private const string __ActionAttributeName = "asp-ajax-action";
    //    private const string __ControllerAttributeName = "asp-ajax-controller";
    //    private const string __AreaAttributeName = "asp-ajax-area";
    //    private const string __PageAttributeName = "asp-ajax-page";
    //    private const string __PageHandlerAttributeName = "asp-ajax-page-handler";
    //    private const string __FragmentAttributeName = "asp-ajax-fragment";
    //    private const string __HostAttributeName = "asp-ajax-host";
    //    private const string __ProtocolAttributeName = "asp-ajax-protocol";
    //    private const string __RouteAttributeName = "asp-ajax-route";
    //    private const string __RouteValuesDictionaryName = "asp-ajax-all-route-data";
    //    private const string __RouteValuesPrefix = "asp-ajax-route-";
    //    private const string __Href = "data-ajax-url";
    //    private IDictionary<string, string> _RouteValues;

    //    /// <summary>Creates a new <see cref="AjaxAnchorTagHelper"/>.</summary>
    //    /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
    //    public AjaxAnchorTagHelper(IHtmlGenerator generator) => Generator = generator;

    //    /// <inheritdoc />
    //    public override int Order => -1000;

    //    /// <summary>
    //    /// Gets the <see cref="IHtmlGenerator"/> used to generate the <see cref="Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper"/>'s output.
    //    /// </summary>
    //    protected IHtmlGenerator Generator { get; }

    //    /// <summary>
    //    /// The name of the action method.
    //    /// </summary>
    //    /// <remarks>
    //    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
    //    /// </remarks>
    //    [HtmlAttributeName(__ActionAttributeName)]
    //    public string Action { get; set; }

    //    /// <summary>
    //    /// The name of the controller.
    //    /// </summary>
    //    /// <remarks>
    //    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
    //    /// </remarks>
    //    [HtmlAttributeName(__ControllerAttributeName)]
    //    public string Controller { get; set; }

    //    /// <summary>
    //    /// The name of the area.
    //    /// </summary>
    //    /// <remarks>
    //    /// Must be <c>null</c> if <see cref="Route"/> is non-<c>null</c>.
    //    /// </remarks>
    //    [HtmlAttributeName(__AreaAttributeName)]
    //    public string Area { get; set; }

    //    /// <summary>
    //    /// The name of the page.
    //    /// </summary>
    //    /// <remarks>
    //    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Action"/>, <see cref="Controller"/>
    //    /// is non-<c>null</c>.
    //    /// </remarks>
    //    [HtmlAttributeName(__PageAttributeName)]
    //    public string Page { get; set; }

    //    /// <summary>
    //    /// The name of the page handler.
    //    /// </summary>
    //    /// <remarks>
    //    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Action"/>, or <see cref="Controller"/>
    //    /// is non-<c>null</c>.
    //    /// </remarks>
    //    [HtmlAttributeName(__PageHandlerAttributeName)]
    //    public string PageHandler { get; set; }

    //    /// <summary>
    //    /// The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.
    //    /// </summary>
    //    [HtmlAttributeName(__ProtocolAttributeName)]
    //    public string Protocol { get; set; }

    //    /// <summary>
    //    /// The host name.
    //    /// </summary>
    //    [HtmlAttributeName(__HostAttributeName)]
    //    public string Host { get; set; }

    //    /// <summary>
    //    /// The URL fragment name.
    //    /// </summary>
    //    [HtmlAttributeName(__FragmentAttributeName)]
    //    public string Fragment { get; set; }

    //    /// <summary>
    //    /// Name of the route.
    //    /// </summary>
    //    /// <remarks>
    //    /// Must be <c>null</c> if one of <see cref="Action"/>, <see cref="Controller"/>, <see cref="Area"/> 
    //    /// or <see cref="Page"/> is non-<c>null</c>.
    //    /// </remarks>
    //    [HtmlAttributeName(__RouteAttributeName)]
    //    public string Route { get; set; }

    //    /// <summary>
    //    /// Additional parameters for the route.
    //    /// </summary>
    //    [HtmlAttributeName(__RouteValuesDictionaryName, DictionaryAttributePrefix = __RouteValuesPrefix)]
    //    public IDictionary<string, string> RouteValues
    //    {
    //        get => _RouteValues ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    //        set => _RouteValues = value;
    //    }

    //    /// <summary>
    //    /// Gets or sets the <see cref="Rendering.ViewContext"/> for the current request.
    //    /// </summary>
    //    [HtmlAttributeNotBound]
    //    [ViewContext]
    //    public ViewContext ViewContext { get; set; }

    //    /// <inheritdoc />
    //    /// <remarks>Does nothing if user provides an <c>href</c> attribute.</remarks>
    //    public override void Process(TagHelperContext context, TagHelperOutput output)
    //    {
    //        if (context == null)
    //            throw new ArgumentNullException(nameof(context));

    //        if (output == null)
    //            throw new ArgumentNullException(nameof(output));

    //        // If "href" is already set, it means the user is attempting to use a normal anchor.
    //        if (output.Attributes.ContainsName(__Href))
    //        {
    //            if (Action != null ||
    //                Controller != null ||
    //                Area != null ||
    //                Page != null ||
    //                PageHandler != null ||
    //                Route != null ||
    //                Protocol != null ||
    //                Host != null ||
    //                Fragment != null ||
    //                (_RouteValues != null && _RouteValues.Count > 0))
    //            {
    //                // User specified an href and one of the bound attributes; can't determine the href attribute.
    //                throw new InvalidOperationException();
    //            }

    //            return;
    //        }

    //        var route_link = Route != null;
    //        var action_link = Controller != null || Action != null;
    //        var page_link = Page != null || PageHandler != null;

    //        if ((route_link && action_link) || (route_link && page_link) || (action_link && page_link))
    //            throw new InvalidOperationException();

    //        RouteValueDictionary route_values = null;
    //        if (_RouteValues != null && _RouteValues.Count > 0) 
    //            route_values = new RouteValueDictionary(_RouteValues);

    //        if (Area != null)
    //        {
    //            // Unconditionally replace any value from asp-ajax-route-area.
    //            route_values ??= new RouteValueDictionary();
    //            route_values["area"] = Area;
    //        }

    //        TagBuilder tag_builder;
    //        if (page_link)
    //            tag_builder = Generator.GeneratePageLink(
    //                ViewContext,
    //                linkText: string.Empty,
    //                pageName: Page,
    //                pageHandler: PageHandler,
    //                protocol: Protocol,
    //                hostname: Host,
    //                fragment: Fragment,
    //                routeValues: route_values,
    //                htmlAttributes: null);
    //        else
    //            tag_builder = route_link
    //                ? Generator.GenerateRouteLink(
    //                    ViewContext,
    //                    linkText: string.Empty,
    //                    routeName: Route,
    //                    protocol: Protocol,
    //                    hostName: Host,
    //                    fragment: Fragment,
    //                    routeValues: route_values,
    //                    htmlAttributes: null)
    //                : Generator.GenerateActionLink(
    //                    ViewContext,
    //                    linkText: string.Empty,
    //                    actionName: Action,
    //                    controllerName: Controller,
    //                    protocol: Protocol,
    //                    hostname: Host,
    //                    fragment: Fragment,
    //                    routeValues: route_values,
    //                    htmlAttributes: null);

    //        if(!output.Attributes.ContainsName("data-ajax"))
    //            output.Attributes.Add("data-ajax", true);

    //        output.MergeAttributes(tag_builder);
    //    }
    //}
}
