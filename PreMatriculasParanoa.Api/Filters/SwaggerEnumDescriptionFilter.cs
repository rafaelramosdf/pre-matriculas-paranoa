using PreMatriculasParanoa.Domain.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace PreMatriculasParanoa.Api.Filters
{
    public class SwaggerEnumDescriptionFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                model.Enum.Clear();

                var enumItems = Enum.GetNames(context.Type).ToList();

                foreach (var enumName in enumItems) 
                {
                    var enumObject = (Enum)Enum.Parse(context.Type, enumName);
                    var enumDescription = enumObject.EnumDescription();
                    var swaggerDescription = string.IsNullOrEmpty(enumDescription)
                        ? $" {enumObject.GetHashCode()} = {enumDescription}"
                        : $" {enumObject.GetHashCode()} = {enumName}";

                    model.Enum.Add(new OpenApiString(swaggerDescription));
                }
            }
        }
    }
}
