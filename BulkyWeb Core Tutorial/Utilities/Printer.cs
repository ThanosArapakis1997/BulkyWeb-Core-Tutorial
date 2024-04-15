using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Fields;
using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.VisualBasic;
using QRCoder;
using SkiaSharp;
using Stripe;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MGTConcerts.Utilities
{
    public class Printer
    {
        static Printer instance = new Printer();

        public static Printer Instance
        {
            get { return instance; }
        }

        public void RenderTemplate(string template, object obj, Stream stream, IUnitOfWork unitOfWork)
        {          

            Document document = new Document(template);
            string[] fields = document.MailMerge.GetFieldNames();
            DataSet set = ((IPrintingObject)obj).GetPrintingDataSet(fields, unitOfWork);
            document.MailMerge.ExecuteWithRegions(set);
            document.UpdateFields();

            if (obj is IPrintingObjectUpon)
            {
                document = ((IPrintingObjectUpon)obj).DoSpecialPostprocessing(document);
            }
            // Initialize a BarcodeGenerator class object and Set CodeText & Symbology Type
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "12345TEX");
            // Set ForceQR (default) for standard QR and Code text
            generator.Parameters.Barcode.QR.QrEncodeMode = QREncodeMode.Auto;
            generator.Parameters.Barcode.QR.QrEncodeType = QREncodeType.ForceQR;
            generator.Parameters.Barcode.QR.QrErrorLevel = QRErrorLevel.LevelL;
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.ImageWidth.Pixels = 500;
            generator.Parameters.ImageHeight.Pixels = 500;
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Parameters.Barcode.Padding.Left.Millimeters = 30;
            Aspose.Drawing.Bitmap lBmp = generator.GenerateBarCodeImage();

            MemoryStream memoryStream = new MemoryStream();

            lBmp.Save(memoryStream, Aspose.Drawing.Imaging.ImageFormat.Bmp);
            memoryStream.Position = 0;

            // Or Save to BMP File on Disk
            //lBmp.Save("image.bmp", ImageFormat.Bmp);

            // Load DOCX file you want to insert QR Code Image into
            DocumentBuilder builder = new DocumentBuilder(document);
            builder.MoveToDocumentEnd(); // or move cursor to any Node position
            // Insert QR Code Image from Memory Stream
            builder.InsertImage(memoryStream);            

            document.Save(stream, SaveFormat.Pdf);
        }       

        public bool PrintToPdf(string template, object obj, string filename, out string outputFile, Stream stream, IUnitOfWork unitOfWork)
        {
            template = template + ".docx";
            if (!System.IO.File.Exists(template)) throw new Exception("Failed to find template " + template);
            outputFile = filename + ".pdf";

            //if (File.Exists(outputFile)) File.Delete(outputFile);
            RenderTemplate(template, obj, stream, unitOfWork);
            return true;
        }

        public class ListtoDataTableConverter
        {

            public DataTable ToDataTable<T>(List<T> items)
            {
                return ToDataTable<T>(items, typeof(T).Name, new string[] { });
            }

            public DataTable ToDataTable<T>(List<T> items, string tableName)
            {
                return ToDataTable<T>(items, tableName, new string[] { });
            }
            public DataTable ToDataTable<T>(List<T> items, string tableName, string[] fields)
            {
                DataTable dataTable = new DataTable(tableName);
                //Get all the properties
                PropertyInfo[] Props = new PropertyInfo[] { };
                if (fields == null || fields.Length == 0)
                {
                    Props =
                        typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .ToList()
                            .Where(x => !x.PropertyType.IsSubclassOf(typeof(Order)))
                            .ToArray();
                }
                else
                {
                    Props =
                        typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .ToList()
                            .Where(x => Array.IndexOf(fields, x.Name) > 0)
                            .ToArray();
                }

                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
    }

    public interface IPrintingObject
    {
        DataSet GetPrintingDataSet(string[] fields, IUnitOfWork unitofWork);
    }

    public interface IPrintingObjectUpon
    {
        Aspose.Words.Document DoSpecialPostprocessing(Aspose.Words.Document document);
    }
}
