using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Settings
{
    public static class ViewExceptions
    {
        public static string FormatValidationErrorMessage(DbEntityValidationException exception)
        {
            var errorMessageBuilder = new StringBuilder();
            foreach (var validationErrors in exception.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    errorMessageBuilder.AppendLine($"Свойство: {validationError.PropertyName} Ошибка: {validationError.ErrorMessage}");
                }
            }

            return errorMessageBuilder.ToString();
        }
    }
}
