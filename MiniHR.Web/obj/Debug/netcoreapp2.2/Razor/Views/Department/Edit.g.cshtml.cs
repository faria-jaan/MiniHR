#pragma checksum "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "de43614e286e071b7408ba2a977985ac03d996a0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Department_Edit), @"mvc.1.0.view", @"/Views/Department/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Department/Edit.cshtml", typeof(AspNetCore.Views_Department_Edit))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 3 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
using MiniHR.Application.DTOs;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de43614e286e071b7408ba2a977985ac03d996a0", @"/Views/Department/Edit.cshtml")]
    public class Views_Department_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MiniHR.Application.DTOs.DepartmentDto>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(46, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(80, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
  
    ViewBag.Title = "Edit Department";

#line default
#line hidden
            BeginContext(129, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(131, 220, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "de43614e286e071b7408ba2a977985ac03d996a03292", async() => {
                BeginContext(137, 207, true);
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>Edit Department</title>\r\n    <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" />\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(351, 60, true);
            WriteLiteral("\r\n\r\n<div class=\"card shadow-sm mb-4\">\r\n    <h2 class=\"mb-4\">");
            EndContext();
            BeginContext(412, 13, false);
#line 16 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
                Write(ViewBag.Title);

#line default
#line hidden
            EndContext();
            BeginContext(425, 74, true);
            WriteLiteral("</h2>\r\n    <div class=\"card shadow-sm\">\r\n        <div class=\"card-body\">\r\n");
            EndContext();
#line 19 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
             using (Html.BeginForm("Edit", "Department", FormMethod.Post, new { @class = "needs-validation", novalidate = "novalidate" }))
            {

#line default
#line hidden
            BeginContext(654, 91, true);
            WriteLiteral("                <div asp-validation-summary=\"ModelOnly\" class=\"alert alert-danger\"></div>\r\n");
            EndContext();
            BeginContext(762, 23, false);
#line 22 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
           Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(804, 35, false);
#line 23 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
           Write(Html.HiddenFor(m => m.DepartmentID));

#line default
#line hidden
            EndContext();
            BeginContext(843, 56, true);
            WriteLiteral("                <div class=\"mb-3\">\r\n                    ");
            EndContext();
            BeginContext(900, 67, false);
#line 26 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
               Write(Html.LabelFor(m => m.DepartmentName, new { @class = "form-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(967, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(990, 133, false);
#line 27 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
               Write(Html.TextBoxFor(m => m.DepartmentName, new { @class = "form-control", placeholder = "Enter department name", required = "required" }));

#line default
#line hidden
            EndContext();
            BeginContext(1123, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1146, 84, false);
#line 28 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
               Write(Html.ValidationMessageFor(m => m.DepartmentName, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(1230, 26, true);
            WriteLiteral("\r\n                </div>\r\n");
            EndContext();
            BeginContext(1258, 56, true);
            WriteLiteral("                <div class=\"mb-3\">\r\n                    ");
            EndContext();
            BeginContext(1315, 69, false);
#line 32 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
               Write(Html.LabelFor(m => m.HeadOfDepartment, new { @class = "form-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(1384, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1407, 138, false);
#line 33 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
               Write(Html.TextBoxFor(m => m.HeadOfDepartment, new { @class = "form-control", placeholder = "Enter head of department", required = "required" }));

#line default
#line hidden
            EndContext();
            BeginContext(1545, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1568, 86, false);
#line 34 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
               Write(Html.ValidationMessageFor(m => m.HeadOfDepartment, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(1654, 26, true);
            WriteLiteral("\r\n                </div>\r\n");
            EndContext();
            BeginContext(1682, 251, true);
            WriteLiteral("                <div class=\"d-flex justify-content-end mt-4\">\r\n                    <button type=\"submit\" class=\"btn btn-primary me-2\">\r\n                        <i class=\"bi bi-save me-1\"></i> Update\r\n                    </button>\r\n                    ");
            EndContext();
            BeginContext(1934, 86, false);
#line 41 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
               Write(Html.ActionLink("Cancel", "Index", null, new { @class = "btn btn-outline-secondary" }));

#line default
#line hidden
            EndContext();
            BeginContext(2020, 26, true);
            WriteLiteral("\r\n                </div>\r\n");
            EndContext();
#line 43 "C:\Users\Faria Siddiqui\MINIHRAPP\MiniHR.Web\Views\Department\Edit.cshtml"
            }

#line default
#line hidden
            BeginContext(2061, 38, true);
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2117, 255, true);
                WriteLiteral("\r\n    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.5/jquery.validate.min.js\"></script>\r\n    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/4.0.0/jquery.validate.unobtrusive.min.js\"></script>\r\n");
                EndContext();
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MiniHR.Application.DTOs.DepartmentDto> Html { get; private set; }
    }
}
#pragma warning restore 1591
