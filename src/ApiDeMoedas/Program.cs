using ApiDeMoedas.Dtos.SolicitarCotacaoParaCliente;
using Commands.Clientes.CadastrarCliente;
using Commands.Clientes.CadastrarCliente.Dtos;
using Commands.Cotacoes.SolicitarCotacao;
using Commands.Cotacoes.SolicitarCotacaoParaCliente.Dtos;
using Domain.Models.Enums;
using Infra.DI;
using Queries.Cotacoes.ObterCotacao;
using Queries.Cotacoes.ObterCotacao.Dtos;
using System.Reflection;
using static Shared.ResponseHandlers.ResponseHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAwsServices(builder.Configuration.GetAWSOptions());

builder.Services.AddApiDeMoedasServices(builder.Configuration);

builder.Host.ConfigureAppConfiguration((hostContext, config) =>
{
    config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
    config.AddJsonFile("appsettings.json", optional: true, false);
    config.AddEnvironmentVariables();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapPost("/api/v1/cliente",
async (
    CadastrarClienteRequestDto? request,
    CadastrarClienteHandler handler,
    CancellationToken cancellationToken
) =>
{
    var result = await handler.Handle(request, cancellationToken);
    return ResponseResult(result);
});

app.MapGet("/api/v1/cliente/{id}/cotacao",
async (
    Guid id,
    ObterUltimaCotacaoHandler handler,
    CancellationToken cancellationToken
) =>
{
    var result = await handler.Handle(
        new ObterUltimaCotacaoRequestDto(id, Moeda.USD, Moeda.BRL), cancellationToken);
    return ResponseResult(result);
});

app.MapMethods("/api/v1/cliente/{id}/cotacao",
new[] { "PATCH" },
async (
    Guid id,
    ValorSolicitadoParaCotacaoEmReaisDto request,
    SolicitarCotacaoParaClienteHandler handler,
    CancellationToken cancellationToken
) =>
{
    var result = await handler.Handle(
        new SolicitarCotacaoParaClienteRequestDto(id, Moeda.USD, Moeda.BRL, request.ValorCotadoEmReais), cancellationToken);
    return ResponseResult(result);
});


app.Run();
