﻿namespace Movies
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct|AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Enum,AllowMultiple =false)]
    public class SwaggerSchemaExampleAttribute:Attribute
    {
        public string Example { get; internal set; }
        public SwaggerSchemaExampleAttribute(string example)
        {
            Example = example;
        }
    }
}
