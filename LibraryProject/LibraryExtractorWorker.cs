using LibraryProject.Extractor;
using static LibraryProject.Extractor.PdfExtractor;

namespace LibraryProject
{
    public class LibraryExtractorWorker : BackgroundService
    {
        private readonly ILogger<LibraryExtractorWorker> _logger;
        private readonly PdfExtractor _pdfExtractor;

        public LibraryExtractorWorker(ILogger<LibraryExtractorWorker> logger, PdfExtractor pdfExtractor)
        {
            _logger = logger;
            _pdfExtractor = pdfExtractor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Pdfs");
                var files = Directory.GetFiles(path);

                foreach (var file in files) {


                    using (var stream = File.ReadAllBytesAsync(path))
                    {
                        var extractByteData = await stream;

                        var extractedText = _pdfExtractor.ExtractData(extractByteData, ExtractionStrategy.LocationTextExtractionStrategy);

                        File.WriteAllText(Path.Combine(path, $"{file}_content.txt"), extractedText.ToString());

                    }


                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
