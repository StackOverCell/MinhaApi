using MinhaApi.Repository;
using MinhaApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Registra o Reposiory
builder.Services.AddScoped<
    IProdutoRepository,
    ProdutoRepository>();

builder.Services.AddScoped<
    IClienteRepository,
    ClienteRepository>();

builder.Services.AddScoped<
    IVendaRepository,
    VendasRepository>();

builder.Services.AddScoped<
    IFornecedorRepository,
    FornecedorRepository>();

builder.Services.AddScoped<
    IDepartamentoRepository,
    DepartamentoRepository>();

// Registra a Service
builder.Services.AddScoped<
    IFornecedorService,
    FornecedorService>();

builder.Services.AddScoped<
    IProdutoService,
    ProdutoService>();
    
builder.Services.AddScoped<
    IClienteService,
    ClienteService>();

builder.Services.AddScoped<
    IVendaService,
    VendaService>();

builder.Services.AddScoped<
    IDepartamentoService,
    DepartamentoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();