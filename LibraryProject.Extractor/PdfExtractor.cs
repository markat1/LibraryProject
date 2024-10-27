using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace LibraryProject.Extractor
{
    public class PdfExtractor
    {
        public ICollection<string> ExtractDataWithStandardTextStrategy(byte[] contentBytes)
        {
            return ExtractData(contentBytes, ExtractionStrategy.SimpleTextExtractionStrategy);
        }

        public ICollection<string> ExtractDataWithLocationTextExtractionStrategy(byte[] contentBytes)
        {
            return ExtractData(contentBytes, ExtractionStrategy.LocationTextExtractionStrategy);
        }

        public ICollection<string> ExtractData(byte[] contentBytes, ExtractionStrategy extractionStrategy)
        {

            if (contentBytes is null) new ArgumentException($"{nameof(contentBytes)} parameter can't be empty");

            var memoryStream = new MemoryStream(contentBytes);
            var pdfReader = new PdfReader(memoryStream);
            ICollection<string> pdfContents = new List<string>();

            using var pdfDoc = new PdfDocument(pdfReader);

     
            for (Int32 i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                var p = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), GetExtractionStrategy(extractionStrategy));
                pdfContents.Add(p);

            }

            return pdfContents;
        }

        private ITextExtractionStrategy GetExtractionStrategy(ExtractionStrategy extractionStrategy)
        {
            return extractionStrategy switch
            {
                ExtractionStrategy.SimpleTextExtractionStrategy => new SimpleTextExtractionStrategy(),
                ExtractionStrategy.LocationTextExtractionStrategy => new LocationTextExtractionStrategy(),
                _ => new SimpleTextExtractionStrategy()
            };
        }

        public enum ExtractionStrategy
        {
            SimpleTextExtractionStrategy,
            LocationTextExtractionStrategy
        }
    }
}
