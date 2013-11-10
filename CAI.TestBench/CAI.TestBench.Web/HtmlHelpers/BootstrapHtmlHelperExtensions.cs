using System;

namespace CAI.TestBench.Web.HtmlHelpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Nancy.ModelBinding;
    using Nancy.Validation;
    using Nancy.ViewEngines.Razor;

    public static class BootstrapHtmlHelperExtensions
    {
        public static IHtmlString LabelFor<TModel, TProperty>(
            this HtmlHelpers<TModel> helpers,
            Expression<Func<TModel, TProperty>> propertyExpression,
            object htmlAttributes,
            string labelName = null)
        {
            var memberInfo = propertyExpression.GetTargetMemberInfo();

            var label = new TagBuilder("label");
            label.MergeAttribute("for", memberInfo.Name);

            if (string.IsNullOrEmpty(labelName))
            {
                labelName = memberInfo.Name;
            }

            label.SetInnerText(labelName);

            if (htmlAttributes != null)
            {
                label.MergeAttributes(ConvertHtmlAttributes(htmlAttributes));
            }

            return new NonEncodedHtmlString(label.ToString());
        }

        public static IHtmlString TextBoxFor<TModel, TProperty>(
            this HtmlHelpers<TModel> helpers, 
            Expression<Func<TModel, TProperty>> propertyExpression, 
            object htmlAttributes = null)
        {
            var memberInfo = propertyExpression.GetTargetMemberInfo();
            var expressionValue = propertyExpression.Compile()(helpers.Model);

            var textInput = new TagBuilder("input");
            textInput.MergeAttribute("type", "text");
            textInput.MergeAttribute("name", memberInfo.Name);

            if (expressionValue != null)
            {
                textInput.MergeAttribute("value", expressionValue.ToString());
            }

            if (htmlAttributes != null)
            {
                textInput.MergeAttributes(ConvertHtmlAttributes(htmlAttributes));
            }

            if (!textInput.Attributes.ContainsKey("placeholder"))
            {
                textInput.MergeAttribute("placeholder", memberInfo.Name);
            }

            var builder = new TagBuilder("div");

            if (ModelHasErrorForProperty(helpers.RenderContext.Context.ModelValidationResult, memberInfo.Name))
            {
                builder.AddCssClass("has-error");
            }

            builder.AddCssClass("col-sm-4");
            builder.InnerHtml = textInput.ToString(TagRenderMode.SelfClosing);
            return new NonEncodedHtmlString(builder.ToString());
        }

        private static bool ModelHasErrorForProperty(ModelValidationResult modelValidationResult, string propertyName)
        {
            return modelValidationResult.Errors.Any(m => m.MemberNames.Contains(propertyName));
        }

        private static IDictionary<string, object> ConvertHtmlAttributes(object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return null;
            }

            var type = htmlAttributes.GetType();
            var properties = type.GetProperties();
            var attrDictionary = properties.ToDictionary(prop => prop.Name, prop => prop.GetValue(htmlAttributes, null));
            return attrDictionary;
        }
    }
}