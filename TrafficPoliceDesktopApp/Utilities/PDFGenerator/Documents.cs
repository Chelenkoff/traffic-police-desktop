using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.Utilities.PDFGenerator
{
    static class Documents
    {
        public static Document CreateMyDataDocument(User usr)
        {
            // Create a new MigraDoc document
            Document document = new Document();
            document.Info.Title = "Hello, MigraDoc";
            document.Info.Subject = "Demonstrates an excerpt of the capabilities of MigraDoc.";
            document.Info.Author = "Stefan Lange";

            Styles.DefineMyDataStyles(document);


            DefineContentSection(document);
            Paragraphs.DefineMyDataParagraph(document,usr);


            return document;
        }

        /// <summary>
        /// Defines page setup, headers, and footers.
        /// </summary>
        static void DefineContentSection(Document document)
        {
            Section section = document.AddSection();
            section.PageSetup.OddAndEvenPagesHeaderFooter = true;
            section.PageSetup.StartingNumber = 1;

            // Create a paragraph with centered page number. See definition of style "Footer".
            Paragraph paragraph = new Paragraph();
            paragraph.AddTab();
            paragraph.AddPageField();


        }
    }
}
