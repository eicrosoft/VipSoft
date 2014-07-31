using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExpertPdf.HtmlToPdf;

namespace Demo
{
    public partial class HtmlToPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            //Test();
            Default();
        }

        private static void Default()
        {
            PdfConverter pdfConverter = new PdfConverter();

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdfConverter.PdfDocumentOptions.ShowHeader = true;
            pdfConverter.PdfDocumentOptions.ShowFooter = true;
            pdfConverter.PdfDocumentOptions.LeftMargin = 5;
            pdfConverter.PdfDocumentOptions.RightMargin = 5;
            pdfConverter.PdfDocumentOptions.TopMargin = 5;
            pdfConverter.PdfDocumentOptions.BottomMargin = 5;
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;

            pdfConverter.PdfDocumentOptions.ShowHeader = true;
            //pdfConverter.PdfHeaderOptions.HeaderText = "Sample header: " + "<br><font color='#ff0000'>ASDF</a><br/><B>DDD</B>";

            pdfConverter.PdfHeaderOptions.HtmlToPdfArea = new HtmlToPdfArea("http://localhost:24533/HtmlPage.html");
            pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.Blue;
            //pdfConverter.PdfHeaderOptions.HeaderDescriptionText = string.Empty;
            pdfConverter.PdfHeaderOptions.DrawHeaderLine = false;

            //pdfConverter.PdfFooterOptions.FooterText =
            //    "Sample footer: <B>Jimmy</B>. You can change color, font and other options";

            pdfConverter.PdfFooterOptions.HtmlToPdfArea = new HtmlToPdfArea("http://localhost:24533/HtmlPage.html");
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue;
            pdfConverter.PdfFooterOptions.DrawFooterLine = false;
            pdfConverter.PdfFooterOptions.PageNumberText = "Page";
            pdfConverter.PdfFooterOptions.ShowPageNumber = true;

            //pdfConverter.LicenseKey = "put your serial number here";
            byte[] downloadBytes = pdfConverter.GetPdfFromUrlBytes("www.vipsoft.com.cn/index.html");


            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "binary/octet-stream");
            response.AddHeader("Content-Disposition",
                               "attachment; filename=" + "Test.pdf" + "; size=" + downloadBytes.Length.ToString());
            response.Flush();
            response.BinaryWrite(downloadBytes);
            response.Flush();
            response.End();
        }

        private void Test()
        {
            PdfConverter pdfConverter = new PdfConverter();
            // show header and footer in the rendered PDF
            pdfConverter.PdfDocumentOptions.ShowHeader = true;
            pdfConverter.PdfDocumentOptions.ShowFooter = true;
            // set the header height in points
            pdfConverter.PdfHeaderOptions.HeaderHeight = 60;
            // set the header HTML area
            pdfConverter.PdfHeaderOptions.HtmlToPdfArea = new HtmlToPdfArea("http://localhost:24533/HtmlPage.html");
            // set the footer height in points
            pdfConverter.PdfFooterOptions.FooterHeight = 50;
            //write the page number
            pdfConverter.PdfFooterOptions.TextArea = new TextArea(0, 30, "This is page &p; of &P;  ",
                 new Font(new FontFamily("Times New Roman"), 10, GraphicsUnit.Point));
            pdfConverter.PdfFooterOptions.TextArea.TextAlign = HorizontalTextAlign.Right;
            // set the footer HTML area
            pdfConverter.PdfFooterOptions.HtmlToPdfArea = new HtmlToPdfArea("http://localhost:24533/HtmlPage.html");
            //pdfConverter.PdfFooterOptions.HtmlToPdfArea = new HtmlToPdfArea(http://www.google.com);
            string outFilePath = Path.Combine("D:\\", "AA.pdf");
            //pdfConverter.SavePdfFromUrlToFile("http://www.vipsoft.com.cn", outFilePath);

            byte[] downloadBytes = pdfConverter.GetPdfFromUrlBytes("http://www.vipsoft.com.cn");
             
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "binary/octet-stream");
            response.AddHeader("Content-Disposition",
                               "attachment; filename=" + "Test.pdf" + "; size=" + downloadBytes.Length.ToString());
            response.Flush();
            response.BinaryWrite(downloadBytes);
            response.Flush();
            response.End();
        }
    }
}