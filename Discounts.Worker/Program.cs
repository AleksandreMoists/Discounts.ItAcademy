using Discounts.Persistence;
using Discounts.Worker;
using Discounts.Worker.Jobs;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddHostedService<ExpireOffersJob>();
builder.Services.AddHostedService<ExpireCouponsJob>();
builder.Services.AddHostedService<ExpireReservationsJob>();

var host = builder.Build();
host.Run();
