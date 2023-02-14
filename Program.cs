namespace AspRoutingParamsApp
{
    public class SecretCodeRouteConstraint : IRouteConstraint
    {
        string secretCode;
        public SecretCodeRouteConstraint(string secretCode)
        {
            this.secretCode = secretCode;
        }

        bool IRouteConstraint.Match(HttpContext? httpContext, 
                                    IRouter? route, 
                                    string routeKey, 
                                    RouteValueDictionary values, 
                                    RouteDirection routeDirection)
        {
            return values[routeKey]?.ToString() == secretCode;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.Configure<RouteOptions>(option =>
            //    option.ConstraintMap.Add("secretcode", typeof(SecretCodeRouteConstraint)));

            builder.Services.AddRouting(option =>
                option.ConstraintMap.Add("secretcode", typeof(SecretCodeRouteConstraint)));

            var app = builder.Build();

            //app.Map("/product/{id}", (string id) => $"Product id: {id}");
            //app.Map("/product/{id}", HandleId);
            //app.Map("/product", () => "Products page");

            //app.Map("/{controller=main}/{action=index}/{id?}",
            //    (string controller, string action, string? id) => 
            //       $"Controller: {controller}, Action: {action}, Id: {id??"none"}");
            //app.Map("/type/{**vars}", (string vars) => $"{vars}");

            app.Map("/product/{id:int:max(100)}", (int? id) => $"Product id: {id}");

            app.Map("/contact/{phone:regex(^7-\\d{{3}}-\\d{{3}}-\\d{{4}}$)}",
                (string phone) => $"Contact phone: {phone}");

            app.Map("/enter/{code:secretcode(123)}",
                (string code) => $"Enter with code {code}");

            app.Map("/", () => "Home page");


            app.Run();
        }

        public static string HandleId(string id)
        {
            return $"Product id: {id}";
        }

        /*
         
        :int -> IntRouteConstraint
        :bool
        :datetime
        :decimal
        :guid
        :long
        :float
        :double

        :length(n)
        :minlength(n)
        :maxlength(n)
        
        :min(value)
        :max(value)
        :range(min, max)

        :alpha

        :regex(expr)

        :required

        */
    }
}