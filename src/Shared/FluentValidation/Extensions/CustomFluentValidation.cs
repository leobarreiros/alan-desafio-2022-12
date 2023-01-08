using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.FluentValidation.Extensions
{
    public static class CustomFluentValidation
    {
        public static IRuleBuilderOptions<T, string> IsAlphaNumeric<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
              .Matches(x => RegexIsAlphaNumeric());

            return options;
        }

        private static string RegexIsAlphaNumeric() => @"^[a-zA-Z0-9 à-úÀ-Ú-_(&-)'.]*$";
    }
}
