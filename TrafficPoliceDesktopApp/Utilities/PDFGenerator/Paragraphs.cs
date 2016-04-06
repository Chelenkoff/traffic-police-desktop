using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.Utilities.PDFGenerator
{
    static class Paragraphs
    {
        public static void DefineMyDataParagraph(Document document,User usr)
        {
            Paragraph paragraph = document.LastSection.AddParagraph("Paragraph Layout Overview", "Heading1");

            GenerateInfo(document,usr);
            //DemonstrateIndent(document);
            //DemonstrateFormattedText(document);
            //DemonstrateBordersAndShading(document);
        }

        static void GenerateInfo(Document document,User usr)
        {
            document.LastSection.AddParagraph("Лични данни на потребител: ", "Heading3");
            document.LastSection.AddParagraph(String.Format("{0} {1} {2}",usr.FirstName,usr.SecondName,usr.LastName), "Heading2");
            document.LastSection.AddParagraph("Left Aligned", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("Right Aligned", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("Centered", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("Justified", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Justify;
            paragraph.AddText("asasdfs");
        }

        static void DemonstrateIndent(Document document)
        {
            document.LastSection.AddParagraph("Indent", "Heading2");

            document.LastSection.AddParagraph("Left Indent", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.Format.LeftIndent = "2cm";
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("Right Indent", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.RightIndent = "1in";
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("First Line Indent", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.FirstLineIndent = "12mm";
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("First Line Negative Indent", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.LeftIndent = "1.5cm";
            paragraph.Format.FirstLineIndent = "-1.5cm";
            paragraph.AddText("asasdfs");
        }

        static void DemonstrateFormattedText(Document document)
        {
            document.LastSection.AddParagraph("Formatted Text", "Heading2");

            //document.LastSection.AddParagraph("Left Aligned", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.AddText("Text can be formatted ");
            paragraph.AddFormattedText("bold", TextFormat.Bold);
            paragraph.AddText(", ");
            paragraph.AddFormattedText("italic", TextFormat.Italic);
            paragraph.AddText(", or ");
            paragraph.AddFormattedText("bold & italic", TextFormat.Bold | TextFormat.Italic);
            paragraph.AddText(".");
            paragraph.AddLineBreak();
            paragraph.AddText("You can set the ");
            FormattedText formattedText = paragraph.AddFormattedText("size ");
            formattedText.Size = 15;
            paragraph.AddText("the ");
            formattedText = paragraph.AddFormattedText("color ");
            formattedText.Color = Colors.Firebrick;
            paragraph.AddText("the ");
            formattedText = paragraph.AddFormattedText("font", new Font("Verdana"));
            paragraph.AddText(".");
            paragraph.AddLineBreak();
            paragraph.AddText("You can set the ");
            formattedText = paragraph.AddFormattedText("subscript");
            formattedText.Subscript = true;
            paragraph.AddText(" or ");
            formattedText = paragraph.AddFormattedText("superscript");
            formattedText.Superscript = true;
            paragraph.AddText(".");
        }

        static void DemonstrateBordersAndShading(Document document)
        {
            document.LastSection.AddPageBreak();
            document.LastSection.AddParagraph("Borders and Shading", "Heading2");

            document.LastSection.AddParagraph("Border around Paragraph", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Borders.Width = 2.5;
            paragraph.Format.Borders.Color = Colors.Navy;
            paragraph.Format.Borders.Distance = 3;
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("Shading", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Shading.Color = Colors.LightCoral;
            paragraph.AddText("asasdfs");

            document.LastSection.AddParagraph("Borders & Shading", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Style = "TextBox";
            paragraph.AddText("asasdfs");
        }
    }
}
