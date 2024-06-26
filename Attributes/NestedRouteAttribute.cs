namespace StoreApi.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class NestedRouteAttribute(string template, Type t)
    : Attribute, IRouteTemplateProvider
{

    private readonly Type _controllerType = t;
    private readonly string _template = template;

    public string? Template
    {
        get
        {
            // Look up the route from the parent type. This only goes up one level, but if the parent class also has a `NestedRouteAttribute`, then it should work recursively.
            Type? bt = _controllerType.BaseType;

            IRouteTemplateProvider? routeAttr = bt?.GetCustomAttributes().Where(a => a is IRouteTemplateProvider).FirstOrDefault() as IRouteTemplateProvider;
            return $"{routeAttr?.Template}/{_template}";
        }
    }

    public int? Order => null;

    public string? Name => _template;
}
