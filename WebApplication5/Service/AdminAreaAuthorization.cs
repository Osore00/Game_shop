using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace WebApplication5.Service
{
    public class AdminAreaAuthorization:IControllerModelConvention
    {
        private readonly string _adminArea;
        private readonly string _adminPolicy;
        public AdminAreaAuthorization(string adminArea, string adminPolicy)
        {
            _adminArea = adminArea;
            _adminPolicy = adminPolicy;
        }
        public void Apply(ControllerModel controller)
        {
            if (controller.Attributes.Any(a =>
                    a is AreaAttribute && (a as AreaAttribute).RouteValue.Equals(_adminArea, StringComparison.OrdinalIgnoreCase))
                || controller.RouteValues.Any(r =>
                    r.Key.Equals("area", StringComparison.OrdinalIgnoreCase) && r.Value.Equals(_adminArea, StringComparison.OrdinalIgnoreCase)))
            {
                controller.Filters.Add(new AuthorizeFilter(_adminPolicy));
            }
        }
    }
}
