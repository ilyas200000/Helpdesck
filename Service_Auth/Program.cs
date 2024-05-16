using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.EntityFrameworkCore;
using Service_Auth.Data;
using Service_Auth.Models;
using Service_Auth.Service;

namespace Service_Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("AuthConnection") ?? throw new InvalidOperationException("Connection string 'AuthConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            // Capture les exceptions de base de donn�es r�solvables par migration
            // et envoie une r�ponse HTML invitant � migrer la base (� utiliser en mode dev uniquement)
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Ajoute des services d'identit�s communs :
            // - une interface utilisateur par d�faut,
            // - des fournisseurs de jetons pour g�n�rer des jetons afin de r�initialiser les mots de passe,
            // modifier l'e-mail et modifier le N� de tel, et pour l'authentification � 2 facteurs.
            // - configure l'authentification pour utiliser les cookies d'identit�
            builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowedOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });

            builder.Services.AddRazorPages();

            // Ajoute et configure le service IdentityServer
            builder.Services.AddIdentityServer(options =>
                  options.Authentication.CoordinateClientLifetimesWithUserSession = true)

            // Cr�e des identit�s
            .AddInMemoryIdentityResources(new IdentityResource[] {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile(),
                
                
            })

            // Crée une étendue d'API "entreprise" et lui associe la revendication Fonction
            .AddInMemoryApiScopes(new ApiScope[] {
                 new ApiScope("entreprise", new[] { "Role" }),
                 new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            })
            
            // Configure une appli cliente
            .AddInMemoryClients(new Client[] {
                 new Client
                 {
                    ClientId = "Client1",
                    ClientSecrets = { new Secret("Secret1".Sha256()) },
                    AllowedGrantTypes =  GrantTypes.ResourceOwnerPassword ,

                    // Urls auxquelles envoyer les jetons
                    RedirectUris = { "https://localhost:8080/signin-oidc" },
                    // Urls de redirection apr�s d�connexion
                    PostLogoutRedirectUris = { "https://localhost:8080/signout-callback-oidc" },
                    // Url pour envoyer une demande de d�connexion au serveur d'identit�
                    FrontChannelLogoutUri = "https://localhost:8080/signout-oidc",

                    // Etendues d'API autoris�es
                    AllowedScopes = { "openid", "profile" , IdentityServerConstants.LocalApi.ScopeName},

                    // Autorise le client � utiliser un jeton d'actualisation
                    AllowOfflineAccess = true
                 },

            })
            // Indique d'utiliser ASP.Net Core Identity pour la gestion des profils et revendications
            .AddAspNetIdentity<ApplicationUser>();

            // Ajoute la journalisation au niveau debug des �v�nements �mis par Duende
            builder.Services.AddLogging(options =>
            {
                options.AddFilter("Duende", LogLevel.Debug);
            });

            builder.Services.AddLocalApiAuthentication();
            builder.Services.AddControllers();

            // Extend the userinfo endpoint to include roles
            builder.Services.AddTransient<IProfileService,CustomProfileService>();



            var app = builder.Build();
            app.UseCors("MyAllowedOrigins");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
             
                app.UseSwagger();
                app.UseSwaggerUI();
               
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();

         

            app.UseRouting();
            // Ajoute le middleware d'authentification avec IdentityServer dans le pipeline
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapDefaultControllerRoute()
                    .RequireAuthorization();
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}