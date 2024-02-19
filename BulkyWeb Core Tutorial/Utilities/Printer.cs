using Aspose.Words;
using MGTConcerts.Models;
using MGTConcerts.Repository;
using Microsoft.VisualBasic;
using System.Data;
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

            document.Save(stream, SaveFormat.Pdf);
        }

        public bool PrintToPdf(string template, object obj, string filename, out string outputFile, Stream stream, IUnitOfWork unitOfWork)
        {
            template = template + ".docx";
            if (!File.Exists(template)) throw new Exception("Failed to find template " + template);
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
