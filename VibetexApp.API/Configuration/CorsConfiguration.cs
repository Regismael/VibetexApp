namespace AgendaApp.API.Configurations
{

    public class CorsConfiguration
    {
        public static void AddCorsConfiguration(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("VibetexPolicy", builder =>
                {
                    builder.WithOrigins(
                                "http://localhost:4200"
                            )
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        public static void UseCorsConfiguration(IApplicationBuilder app)
        {
            app.UseCors("VibetexPolicy");
        }
    }
}
