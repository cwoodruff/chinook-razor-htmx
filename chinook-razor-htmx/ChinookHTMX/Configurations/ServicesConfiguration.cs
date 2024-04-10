using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;

namespace ChinookHTMX.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureValidators(this IServiceCollection services)
    {
        // services.AddFluentValidationAutoValidation()
        //     .AddTransient<IValidator<AlbumApiModel>, AlbumValidator>()
        //     .AddTransient<IValidator<ArtistApiModel>, ArtistValidator>()
        //     .AddTransient<IValidator<CustomerApiModel>, CustomerValidator>()
        //     .AddTransient<IValidator<EmployeeApiModel>, EmployeeValidator>()
        //     .AddTransient<IValidator<GenreApiModel>, GenreValidator>()
        //     .AddTransient<IValidator<InvoiceApiModel>, InvoiceValidator>()
        //     .AddTransient<IValidator<InvoiceLineApiModel>, InvoiceLineValidator>()
        //     .AddTransient<IValidator<MediaTypeApiModel>, MediaTypeValidator>()
        //     .AddTransient<IValidator<PlaylistApiModel>, PlaylistValidator>()
        //     .AddTransient<IValidator<TrackApiModel>, TrackValidator>();
    }
}