using LibraryProject;
using LibraryProject.Extractor;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<LibraryExtractorWorker>();
builder.Services.AddSingleton<PdfExtractor>();

var host = builder.Build();
host.Run();
